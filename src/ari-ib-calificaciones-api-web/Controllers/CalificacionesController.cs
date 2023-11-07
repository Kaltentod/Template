using ari_ib_calificaciones_api_web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ari_ib_calificaciones_api_web.Controllers;

[ApiController]
[Route("[controller]")]
public class CalificacionesController : ControllerBase
{
    private readonly ILogger<CalificacionesController> _logger;

    public CalificacionesController(ILogger<CalificacionesController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetCalificaciones")]
    public IEnumerable<Calificacion> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Calificacion
            {
                Id = Random.Shared.Next(100, 1000),
                Tipo = Random.Shared.Next(1, 3),
                Clave = Random.Shared.NextInt64(10000000000, 99999999999).ToString(),
                CalificadoraId = Random.Shared.Next(100, 1000),
                CalificacionCrediticia = (CalificacionCrediticia)Random.Shared.Next(0, Enum.GetValues(typeof(CalificacionCrediticia)).Length),
                FechaAlta = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                FechaBaja = DateOnly.FromDateTime(DateTime.Now.AddDays(index+1)),
            })
            .ToArray();
    }

    [HttpGet(Name = "GetCalificacionBcra")]
    public IEnumerable<string> GetCalificacionesBcra()
    {
        var calificaciones = Enum.GetValues(typeof(CalificacionCrediticia))
                            .Cast<CalificacionCrediticia>()
                            .Select(cc => cc.ToString())
                            .ToList();

        return calificaciones;
    }
}