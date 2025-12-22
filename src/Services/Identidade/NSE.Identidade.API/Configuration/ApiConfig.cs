using NSE.WebApi.Core.Identidade;
using Scalar.AspNetCore;

namespace NSE.Identidade.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi();
    }

    public static void UseApiConfiguration(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseJwtConfiguration();

        app.MapControllers();

        app.Run();
    }
}
