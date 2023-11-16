using System.Reflection;
using BNA.IB.Calificaciones.API.Application.Behaviours;
using BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Queries;
using BNA.IB.Calificaciones.API.Web.Filters;
using BNA.IB.Calificaciones.API.Web.Modules;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog();

var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version!.ToString();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("AssemblyVersion", assemblyVersion) 
    .WriteTo.Console()
    .WriteTo.File($"{Assembly.GetExecutingAssembly().GetName().FullName}-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy => { policy.WithOrigins("*"); });
});

// Persistence
builder.Services.AddPersistence(builder.Configuration, builder.Environment);

// mediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(GetCalificadorasQueryHandler))!));

builder.Services.Configure<ApiBehaviorOptions>(options =>
    options.SuppressModelStateInvalidFilter = true);

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));


// FluentValidation
builder.Services.AddControllers(options =>
        options.Filters.Add<ApiExceptionFilterAttribute>())
    .AddFluentValidation();

builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "IB.Calificaciones API",
        Version = "v1",
        Description = "IB Calificaciones API - Banco de la Nación Argentina"
    });
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IB.Calificaciones API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Iniciando BNA.Calificaciones.API - Versión {version}", assemblyVersion);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}