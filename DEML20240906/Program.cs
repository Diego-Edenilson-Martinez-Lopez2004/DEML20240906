using DEML20240906.Endpoints;
using DEML20240906.Models.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agrega servicios para habilitar la generación de documentación de API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura y agrega un contexto de base de datos para Entity Framework Core.
builder.Services.AddDbContext<DEMLContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));

// Agrega una instancia de la clase ProductsDEMLDAL como un servicio para la inyección de dependencias.
builder.Services.AddScoped<ProductsDEMLDAL>();

// Construye la aplicación web.
var app = builder.Build();

// Agrega los puntos finales relacionados con los productos a la aplicación.
app.AddProductEndpoints();

// Verifica si la aplicación se está ejecutando en un entorno de desarrollo.
if (app.Environment.IsDevelopment())
{
    // Habilita el uso de Swagger para la documentación de la API.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Agrega middleware para redirigir las solicitudes HTTP a HTTPS.
app.UseHttpsRedirection();

// Ejecuta la aplicación.
app.Run();