using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Configuration;
using NSE.WebApi.Core.Identidade;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddDependencyInjectionConfig();

var app = builder.Build();

app.UseScalarConfiguration();
app.UseApiConfiguration();