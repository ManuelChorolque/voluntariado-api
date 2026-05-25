using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Aplicacion.Mapping;
using DOMINIO.Interfaces;
using INFRAESTRUCTURA.Data;
using INFRAESTRUCTURA.Repositorios;
using INFRAESTRUCTURA.ServiciosExternos;
using Infraestructura.Data;
using Scalar.AspNetCore;
using APLICACION.CasosUso.Voluntarios;
using APLICACION.CasosUso.Organizaciones;
using APLICACION.CasosUso.Actividades;
using APLICACION.CasosUso.Certificados;
using APLICACION.CasosUso.Horas;
using APLICACION.CasosUso.Auth;
using APLICACION.Interfaces;
using API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext
builder.Services.AddDbContext<VoluntariadoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});
builder.Services.AddSingleton(mapperConfig.CreateMapper());

// Configurar JWT
var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Registrar repositorios
builder.Services.AddScoped<IRepositorioVoluntarios, RepositorioVoluntarios>();
builder.Services.AddScoped<IRepositorioHoras, RepositorioHoras>();
builder.Services.AddScoped<IRepositorioCertificados, RepositorioCertificados>();
builder.Services.AddScoped<IRepositorioOrganizaciones, RepositorioOrganizaciones>();
builder.Services.AddScoped<IRepositorioActividades, RepositorioActividades>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

// Registrar servicios
builder.Services.AddScoped<ITokenServicio, TokenServicio>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CurrentUser>();

// Registrar casos de uso (handlers)
builder.Services.AddScoped<CrearVoluntarioHandler>();
builder.Services.AddScoped<ObtenerVoluntariosHandler>();
builder.Services.AddScoped<ObtenerVoluntarioPorIdHandler>();
builder.Services.AddScoped<ActualizarVoluntarioHandler>();
builder.Services.AddScoped<EliminarVoluntarioHandler>();

builder.Services.AddScoped<CrearOrganizacionHandler>();
builder.Services.AddScoped<ObtenerOrganizacionesHandler>();
builder.Services.AddScoped<ObtenerOrganizacionPorIdHandler>();
builder.Services.AddScoped<ActualizarOrganizacionHandler>();
builder.Services.AddScoped<EliminarOrganizacionHandler>();

builder.Services.AddScoped<CrearActividadHandler>();
builder.Services.AddScoped<ObtenerActividadesHandler>();
builder.Services.AddScoped<ObtenerActividadPorIdHandler>();
builder.Services.AddScoped<ObtenerActividadesAbiertasHandler>();
builder.Services.AddScoped<AsignarVoluntarioHandler>();
builder.Services.AddScoped<EliminarActividadHandler>();
builder.Services.AddScoped<ObtenerActividadesPorVoluntarioHandler>();
builder.Services.AddScoped<ActualizarActividadHandler>();
builder.Services.AddScoped<IniciarActividadHandler>();
builder.Services.AddScoped<CancelarActividadHandler>();
builder.Services.AddScoped<CompletarActividadHandler>();

builder.Services.AddScoped<GenerarCertificadoHandler>();
builder.Services.AddScoped<ObtenerCertificadosHandler>();
builder.Services.AddScoped<ObtenerCertificadoPorIdHandler>();
builder.Services.AddScoped<ObtenerCertificadosPorVoluntarioHandler>();
builder.Services.AddScoped<ObtenerCertificadosPorOrganizacionHandler>();
builder.Services.AddScoped<DescargarCertificadoHandler>();
builder.Services.AddScoped<EliminarCertificadoHandler>();

builder.Services.AddScoped<RegistrarHorasHandler>();
builder.Services.AddScoped<ObtenerHorasHandler>();
builder.Services.AddScoped<ObtenerHoraPorIdHandler>();
builder.Services.AddScoped<CalcularHorasTotalesHandler>();
builder.Services.AddScoped<EliminarHorasHandler>();

// Registrar handlers de autenticacion
builder.Services.AddScoped<RegisterHandler>();
builder.Services.AddScoped<LoginHandler>();

// Registrar servicios externos
builder.Services.AddScoped<ServicioCorreo>();
builder.Services.AddScoped<ServicioPdf>();
builder.Services.AddScoped<IGeneradorPdf, GeneradorPdf>();

// Agregar controladores
builder.Services.AddControllers();

// Configurar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Aplicar migraciones y seed de datos
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<VoluntariadoDbContext>();
    dbContext.Database.Migrate();
    await DataSeeder.SeedAsync(dbContext);
}

// Configurar pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

