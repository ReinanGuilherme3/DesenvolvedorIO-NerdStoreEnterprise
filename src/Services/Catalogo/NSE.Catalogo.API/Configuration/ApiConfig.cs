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
        services.AddOpenApi();

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
        app.UseAuthorization();
        app.UseCors("Total");
        app.UseJwtConfiguration();
        app.MapControllers();
        app.Run();
    }
}
