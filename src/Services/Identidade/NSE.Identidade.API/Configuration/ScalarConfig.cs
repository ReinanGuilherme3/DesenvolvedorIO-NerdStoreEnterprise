using Scalar.AspNetCore;

namespace NSE.Identidade.API.Configuration;

public static class ScalarConfig
{
    public static void UseScalarConfiguration(this WebApplication app)
    {
        app.MapScalarApiReference(options =>
        {
            options.WithTitle("Identidade API")
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });
    }
}
