using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PeluqueriaApi.Config;
using PeluqueriaApi.Repositories;
using PeluqueriaApi.Services;
using System.Text;
using System.Data.Common;
using Swashbuckle.AspNetCore.SwaggerGen;
using PeluqueriaApi.Utils.Filters;

var builder = WebApplication.CreateBuilder(args);

// Agregar cadena de conexi�n
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"));
});

// Agregar servicios al contenedor
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Peluqueria API",
        Description = "Una API para gestionar una peluquer�a",
    });
    options.AddSecurityDefinition("Token", new OpenApiSecurityScheme()
    {
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Name = "Authorization",
        Scheme = "bearer"
    });
    options.OperationFilter<AuthOperationsFilter>();
});

// Agregamos los servicios
builder.Services.AddScoped<ProductoServices>();
builder.Services.AddScoped<ServicioServices>();
builder.Services.AddScoped<TurnoServices>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<AuthServices>();
builder.Services.AddScoped<RolServices>();
builder.Services.AddScoped<IEncoderServices, EncoderServices>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Mapping));

// Agregamos los Repositorios
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IServicioRepository, ServicioRepository>();
builder.Services.AddScoped<ITurnoRepository, TurnoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();

// Configuraci�n de la clave secreta y autenticaci�n JWT
var secretKey = builder.Configuration.GetSection("jwtSettings").GetSection("secretKey").ToString();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS 
app.UseCors(opts =>
{
    opts.AllowAnyMethod();
    opts.AllowAnyHeader();
    opts.AllowAnyOrigin();
});

app.UseHttpsRedirection();
app.UseAuthentication(); // Aseg�rate de que esta l�nea est� antes de UseAuthorization
app.UseAuthorization();
app.MapControllers();
app.Run();
