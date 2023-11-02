using ari_ib_calificaciones_api_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ari_ib_calificaciones_api_web.Controllers;

[ApiController]
[Route("[controller]")]
public class CalificadorasController : ControllerBase
{
    private readonly ILogger<CalificadorasController> _logger;

    public CalificadorasController(ILogger<CalificadorasController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetCalificadoras")]
    public IEnumerable<Calificadora> Get()
    {
        string[] nombres =
        {
            "EVALUADORA LATINOAMERICANA S.A.", 
            "FIX SCR S.A. AGENTE DE CALIFICACIÓN DE RIESGO", 
            "MOODY'S LOCAL AR ACR S.A.", 
            "S&P GLOBAL RATINGS ARGENTINA SRL", 
            "PROFESSIONAL RATING SERVICES AG. CAL. RIESGO S.A."
        };
        
        return Enumerable.Range(1, 5).Select(index => new Calificadora
            {
                Id = Random.Shared.Next(100, 1000),
                Clave = Random.Shared.Next(1, 100),
                Nombre = nombres[Random.Shared.Next(nombres.Length)],
                FechaAlta = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                FechaAltaBCRA = DateOnly.FromDateTime(DateTime.Now.AddDays(index+1)),
                FechaBaja = DateOnly.FromDateTime(DateTime.Now.AddDays(index+2)),
                FechaBajaBCRA = DateOnly.FromDateTime(DateTime.Now.AddDays(index+3)),
            })
            .ToArray();
    }
    
    [HttpGet("{id:int}", Name = "GetCalificadora")]
    public async Task<ActionResult<Calificadora>> Get(int id)
    {
        return new Calificadora
            {
                Id = id,
                Clave = Random.Shared.Next(1, 100),
                Nombre = "S&P GLOBAL RATINGS ARGENTINA SRL",
                FechaAlta = DateOnly.FromDateTime(DateTime.Now),
                FechaAltaBCRA = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                FechaBaja = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                FechaBajaBCRA = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
            };
    }
}