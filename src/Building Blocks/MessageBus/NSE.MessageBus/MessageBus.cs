using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using NSE.Core.Messages.Integration;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace NSE.MessageBus;

public sealed class MessageBus : IMessageBus
{
    private readonly string _connectionString;

    private ServiceProvider? _provider;
    private IBus? _bus;

    public MessageBus(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IPubSub PubSub =>
        _bus?.PubSub ?? throw new InvalidOperationException("Bus não inicializado.");

    private IRpc Rpc =>
        _bus?.Rpc ?? throw new InvalidOperationException("Bus não inicializado.");

    private void EnsureStarted()
    {
        if (_bus is not null) return;

        var policy = Policy
            .Handle<EasyNetQException>()
            .Or<BrokerUnreachableException>()
            .WaitAndRetry(3, retry =>
                TimeSpan.FromSeconds(Math.Pow(2, retry)));

        policy.Execute(() =>
        {
            var services = new ServiceCollection();

            // EasyNetQ 8.x → registro via DI
            services.AddEasyNetQ(_connectionString);

            _provider = services.BuildServiceProvider();
            _bus = _provider.GetRequiredService<IBus>();
        });
    }

    // ---------------- Publish ----------------

    public async Task PublishAsync<T>(T message)
        where T : IntegrationEvent
    {
        EnsureStarted();
        await PubSub.PublishAsync(message);
    }

    // ---------------- Subscribe ----------------

    public async Task SubscribeAsync<T>(
        string subscriptionId,
        Func<T, CancellationToken, Task> onMessage,
        CancellationToken cancellationToken = default)
        where T : class
    {
        EnsureStarted();

        await PubSub.SubscribeAsync(
            subscriptionId,
            onMessage,
            cfg =>
            {
                cfg.WithAutoDelete(false);
                cfg.WithPrefetchCount(1);
            },
            cancellationToken
        );
    }

    // ---------------- RPC ----------------

    public async Task<TResponse> RequestAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default)
        where TRequest : IntegrationEvent
        where TResponse : ResponseMessage
    {
        EnsureStarted();
        return await Rpc.RequestAsync<TRequest, TResponse>(request, cancellationToken);
    }

    public async Task RespondAsync<TRequest, TResponse>(
        Func<TRequest, CancellationToken, Task<TResponse>> responder,
        CancellationToken cancellationToken = default)
        where TRequest : IntegrationEvent
        where TResponse : ResponseMessage
    {
        EnsureStarted();

        await Rpc.RespondAsync(
            responder,
            cfg =>
            {
                cfg.WithPrefetchCount(1);
            },
            cancellationToken
        );
    }

    // ---------------- Dispose ----------------

    public void Dispose()
    {
        _provider?.Dispose();
        _provider = null;
        _bus = null;
    }
}