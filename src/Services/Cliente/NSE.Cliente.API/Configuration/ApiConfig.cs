using Microsoft.EntityFrameworkCore;
using NSE.Cliente.API.Data;
using NSE.WebApi.Core.Identidade;

namespace NSE.Cliente.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ClientesContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new()
                {
                    Title = "Cliente API",
                    Version = "1.0.0",
                    Description = "Cliente API"
                };
                document.Info.Contact = new()
                {
                    Name = "Scalar Project Team",
                    Email = "reinan@treste",
                    Url = new Uri("https://scalarproject.com")
                };

                return Task.CompletedTask;
            });
        });

        services.AddCors(options =>
            options.AddPolicy("Total", builder =>
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

    }

    public static void UseApiConfiguration(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        app.UseHttpsRedirection();
        app.UseCors("Total");
        app.UseJwtConfiguration();
        app.MapControllers();
        app.Run();
    }
}
