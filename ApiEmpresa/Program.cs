using Microsoft.EntityFrameworkCore;
using Apiempresa.Models;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var proveedor = builder.Configuration["ProveedorBaseDatos"];

var cadenaConexion = proveedor == "AzureSQL"
    ? builder.Configuration.GetConnectionString("ConexionSQLAzure")
    : builder.Configuration.GetConnectionString("ConexionMySQL");

if (string.IsNullOrWhiteSpace(cadenaConexion))
{
    throw new InvalidOperationException(
        "No se encontró la cadena de conexión.");
}

builder.Services.AddDbContext<Conexiones>(options =>
{
    if (proveedor == "AzureSQL")
        options.UseSqlServer(cadenaConexion);
    else
        options.UseMySQL(cadenaConexion);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
 app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();





