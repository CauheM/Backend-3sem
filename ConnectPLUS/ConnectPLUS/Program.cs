using ConnectPLUS.BdContextConnectPLUS;
using ConnectPLUS.Interfaces;
using ConnectPLUS.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ConnectPLUSContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITipoContratoRepository, TipoContatoRepository>();
builder.Services.AddScoped<IContratoRepository, ContatoRepository>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Version = "v1",
        Title = "API de Contatos",
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
        Name = "Authorization",
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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();