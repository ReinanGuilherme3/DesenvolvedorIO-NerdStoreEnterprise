using NSE.Core.Messages.Integration;

namespace NSE.MessageBus;

public interface IMessageBus : IDisposable
{
    // Publish
    Task PublishAsync<T>(T message)
        where T : IntegrationEvent;

    // Subscribe
    Task SubscribeAsync<T>(
        string subscriptionId,
        Func<T, CancellationToken, Task> onMessage,
        CancellationToken cancellationToken = default)
        where T : class;

    // RPC
    Task<TResponse> RequestAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken = default)
        where TRequest : IntegrationEvent
        where TResponse : ResponseMessage;

    Task RespondAsync<TRequest, TResponse>(
        Func<TRequest, CancellationToken, Task<TResponse>> responder,
        CancellationToken cancellationToken = default)
        where TRequest : IntegrationEvent
        where TResponse : ResponseMessage;
}