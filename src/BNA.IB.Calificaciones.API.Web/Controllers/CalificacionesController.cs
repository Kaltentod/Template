using BNA.IB.Calificaciones.API.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BNA.IB.Calificaciones.API.Web.Controllers;

[ApiController]
[Route("[controller]")]
[SwaggerTag("Calificaciones")]
public class CalificacionesController : ControllerBase
{
    private readonly ILogger<CalificacionesController> _logger;
    private readonly IMediator _mediator;

    public CalificacionesController(ILogger<CalificacionesController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetCalificaciones")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = ""
    )]
    [SwaggerResponse(200, "")]
    public async Task<IEnumerable<GetCalificacionesQueryResponse>> GetCalificaciones()
    {
        return Enumerable.Range(1, 5).Select(index => new GetCalificacionesQueryResponse
            {
                Id = Random.Shared.Next(100, 1000),
                Tipo = Random.Shared.Next(1, 3),
                Clave = Random.Shared.NextInt64(10000000000, 99999999999).ToString(),
                CalificadoraId = Random.Shared.Next(100, 1000),
                FechaAlta = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                FechaBaja = DateOnly.FromDateTime(DateTime.Now.AddDays(index + 1)),
            })
            .ToArray();
    }
}