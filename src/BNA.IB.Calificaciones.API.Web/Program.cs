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

// Logging
var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version!.ToString();
var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("AssemblyVersion", assemblyVersion) 
    .WriteTo.Console()
    .WriteTo.File($"{assemblyName}-.log", rollingInterval: RollingInterval.Day) 
    //.WriteTo.ApplicationInsights(app.Services.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces)
    .CreateLogger();

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
    Log.Information($"Iniciando {assemblyName} - Versión {assemblyVersion}");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "El host terminó inesperadamente.");
}
finally
{
    Log.CloseAndFlush();
}