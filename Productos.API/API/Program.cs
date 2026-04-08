using System.Net.Http.Headers;
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using DA;
using DA.Repositorios;
using Flujo;
using Reglas;
using Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var urlBaseBccr = builder.Configuration["BancoCentralCR:UrlBase"];
var tokenBccr = builder.Configuration["BancoCentralCR:BearerToken"];
if (string.IsNullOrWhiteSpace(urlBaseBccr))
    throw new InvalidOperationException("Configure BancoCentralCR:UrlBase en appsettings.json.");

builder.Services.AddHttpClient(TipoCambioServicio.HttpClientName, client =>
{
    client.BaseAddress = new Uri(urlBaseBccr.TrimEnd('/') + "/");
    var tokenValido = !string.IsNullOrWhiteSpace(tokenBccr)
        && !string.Equals(tokenBccr, "TU_TOKEN_AQUI_DESDE_BCCR", StringComparison.Ordinal);
    if (tokenValido)
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenBccr);
});

builder.Services.AddScoped<ITipoCambioServicio, TipoCambioServicio>();
builder.Services.AddScoped<IProductoReglas, ProductoReglas>();

builder.Services.AddScoped<IProductoFlujo, ProductoFlujo>();
builder.Services.AddScoped<IProductoDA, ProductoDA>();
builder.Services.AddScoped<ICategoriaFlujo, CategoriaFlujo>();
builder.Services.AddScoped<ISubCategoriaFlujo, SubCategoriaFlujo>();
builder.Services.AddScoped<ICategoriaDA, CategoriaDA>();
builder.Services.AddScoped<ISubCategoriaDA, SubCategoriaDA>();
builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
