using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddDependencyInjectionConfig();

var app = builder.Build();

app.UseScalarConfiguration();
app.UseApiConfiguration();