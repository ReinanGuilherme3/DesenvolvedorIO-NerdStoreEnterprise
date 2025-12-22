using Scalar.AspNetCore;

namespace NSE.Catalogo.API.Configuration;

public static class ScalarConfig
{
    public static void UseScalarConfiguration(this WebApplication app)
    {
        app.MapScalarApiReference(options =>
        {
            options.WithTitle("Catalogo API")
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });
    }
}