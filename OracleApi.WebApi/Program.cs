using Microsoft.EntityFrameworkCore;
using Oracle.DataAccess.Modelos;
using Oracle.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// String de conexion hacia la base de datos
var cadenaConexion = builder.Configuration.GetConnectionString("defaultConnection");

builder.Services.AddDbContext<ModelContext>(x =>
    x.UseOracle(
        cadenaConexion,
        options => options.UseOracleSQLCompatibility("11") // compatibilidad con Oracle 11
        )
    );


//Implementacion de los servicios para cada modelo

builder.Services.AddTransient<IUsuarioService, UsuarioService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
