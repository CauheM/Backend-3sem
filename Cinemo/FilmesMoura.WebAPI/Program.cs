using FilmesMoura.WebAPI.BdContextFilme;
using FilmesMoura.WebAPI.interfaces;
using FilmesMoura.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

//Adiciona o contexto do banco de dados (exemplo SQL server)
builder.Services.AddDbContext<FilmeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})
    .AddJwtBearer("JwtBearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev")),
            ClockSkew = TimeSpan.FromMinutes(5),
            ValidIssuer = "api_filmes",
            ValidAudience = "api_filmes",
        };
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
       Version = "v1",
       Title = "Filmes API"
    });
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { });

    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
            