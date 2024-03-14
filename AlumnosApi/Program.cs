using System.Text;
using System.Text.Json.Serialization;
using AlumnosApi.DAL.DataContext;
using AlumnosApi.DAL.Implementaciones;
using AlumnosApi.DAL.Interfaces;
using AlumnosApi.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var key = builder.Configuration.GetValue<string>("ApiSettings:SECRET_KEY");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(
    //perfil de cache general
    option =>
{
    option.CacheProfiles.Add("PorDefecto", new CacheProfile() { Duration = 10 });
    option.CacheProfiles.Add("Autores", new CacheProfile() { Duration = 15 });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description =
                "Autenticacion JWT usando Bearer. \r\n\r\n "
                + "Ingresar la palabra 'Bearer' seguida de un  [espacio] y despues el token \r\n\r\n",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = "Bearer"
        }
    );

    // Se define un requisito de seguridad globalmente
    var securityRequirement = new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    };

    // Se aplica el requisito de seguridad a nivel global
    options.AddSecurityRequirement(securityRequirement);
});
builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"))
);

builder
    .Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ILibroRepository, LibroRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddAutoMapper(typeof(AutomapperProfile));

builder
    .Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });

builder.Services.AddCors(p =>
    p.AddPolicy(
        "PolicyCors",
        build =>
        {
            build.WithOrigins("").AllowAnyHeader().AllowAnyMethod();
        }
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
