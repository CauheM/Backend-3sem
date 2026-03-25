using Azure.AI.ContentSafety;
using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

//1. configurar o contexto
builder.Services.AddDbContext<EventContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//2. registrar as repositores (injeção de dependência)
builder.Services.AddScoped<ITipoDeEventoRepository, TipoEventoRepository>();
builder.Services.AddScoped<ITipoUsuario, TipoUsuarioRepository>();
builder.Services.AddScoped<IInstituicao, InstituicaoRepository>();
builder.Services.AddScoped<IUsuarioRepository,  UsuarioRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<IPresencaRepository, PresencaRepository>();
builder.Services.AddScoped<IComentarioEventoRepository, ComentarioEventoRepository>();

//Configuração do Azure Content Safety (tira do comentario para funcionar)
var endpoint = "https://moderatorservise-cauhe.cognitiveservices.azure.com/";
var apikey = "";

var client = new ContentSafetyClient(new Uri(endpoint), new Azure.AzureKeyCredential(apikey));
builder.Services.AddSingleton(client);

//Adiciona o Swag
builder.Services.AddEndpointsApiExplorer();

//O cocô do token

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("JwtBearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Eventos-Usuario-chave-autenticacao-web-dev")),
             ClockSkew = TimeSpan.FromMinutes(5),
            ValidIssuer = "api_Eventos",
            ValidAudience = "api_Eventos",
        };
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Version = "v1",
        Title = "API de Eventos",
        Description = "Aplicacao para gerenciamento de eventos",
        TermsOfService = new Uri("https://youtu.be/Tk9mndIdsJo"),
        Contact = new Microsoft.OpenApi.OpenApiContact
        {
            Name = "Cauhê Matheus",
            Url = new Uri("https://github.com/CauheM")
        },
        License = new Microsoft.OpenApi.OpenApiLicense
        {
            Name = "Licensa",
            Url = new Uri("https://rickroll.it/rickroll.mp4")
        }
    });

    //Usando a autenticação no swag
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorizadion",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT:"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });
});




builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger(options =>
    {

    });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
