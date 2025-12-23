using NSE.Cliente.API.Data;

namespace NSE.Cliente.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjectionConfig(this IServiceCollection services)
    {
        // Repository
        //services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<ClientesContext>();
    }
}