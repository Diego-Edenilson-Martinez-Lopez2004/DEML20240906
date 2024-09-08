using DEML20240906.Endpoints;
using DEML20240906.Models.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agrega servicios para habilitar la generaci�n de documentaci�n de API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura y agrega un contexto de base de datos para Entity Framework Core.
builder.Services.AddDbContext<DEMLContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));

// Agrega una instancia de la clase ProductsDEMLDAL como un servicio para la inyecci�n de dependencias.
builder.Services.AddScoped<ProductsDEMLDAL>();

// Construye la aplicaci�n web.
var app = builder.Build();

// Agrega los puntos finales relacionados con los productos a la aplicaci�n.
app.AddProductEndpoints();

// Verifica si la aplicaci�n se est� ejecutando en un entorno de desarrollo.
if (app.Environment.IsDevelopment())
{
    // Habilita el uso de Swagger para la documentaci�n de la API.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Agrega middleware para redirigir las solicitudes HTTP a HTTPS.
app.UseHttpsRedirection();

// Ejecuta la aplicaci�n.
app.Run();