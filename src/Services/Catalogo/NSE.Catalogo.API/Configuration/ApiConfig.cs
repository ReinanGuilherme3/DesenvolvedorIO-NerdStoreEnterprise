using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Data;
using NSE.WebApi.Core.Identidade;

namespace NSE.Catalogo.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogoContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Info = new()
                {
                    Title = "Scalar Project API",
                    Version = "1.0.0",
                    Description = "This is a sample API for Scalar Project using .NET 9.0 and C# 13.0."
                };
                document.Info.Contact = new()
                {
                    Name = "Scalar Project Team",
                    Email = "iago@treste",
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
