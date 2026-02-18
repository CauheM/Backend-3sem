using FilmesMoura.WebAPI.BdContextFilme;
using FilmesMoura.WebAPI.interfaces;
using FilmesMoura.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Adiciona o contexto do banco de dados (exemplo SQL server)
builder.Services.AddDbContext<FilmeContext>(
options => options.UseSqlServer
(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
