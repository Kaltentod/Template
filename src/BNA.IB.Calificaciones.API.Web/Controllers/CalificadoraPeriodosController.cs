using BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Periodos.Commands;
using BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Periodos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BNA.IB.Calificaciones.API.Web.Controllers;

[ApiController]
[Route("api/calificadoras/[controller]")]
[SwaggerTag("Periodos de la Calificadora")]
public class CalificadoraPeriodosController : ControllerBase
{
    private readonly ILogger<CalificadoraPeriodosController> _logger;
    private readonly IMediator _mediator;

    public CalificadoraPeriodosController(ILogger<CalificadoraPeriodosController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    // GetCalificadoraPeriodos
    [HttpGet(Name = "GetCalificadoraPeriodos")]
    [SwaggerOperation(
        Summary = "Obtener todas los Periodos de la Calificadora",
        Description = "Obtiene la lista completa de Periodos de la Calificadora.",
        OperationId = "ObtenerCalificadoraPeriodos"
    )]
    [Produces("application/json")]
    [SwaggerResponse(200, "Operación exitosa", typeof(IEnumerable<GetCalificadoraPeriodosQueryResponse>))]
    public async Task<IEnumerable<GetCalificadoraPeriodosQueryResponse>> GetCalificadoraPeriodos()
    {
        return await _mediator.Send(new GetCalificadoraPeriodosQuery());
    }

    // GetCalificadoraPeriodo
    [HttpGet("{Id:int}", Name = "GetCalificadoraPeriodo")]
    [SwaggerOperation(
        Summary = "Obtener una calificadora por ID",
        Description = "Obtiene los detalles de una calificadora específica según su ID.",
        OperationId = "ObtenerCalificadoraPorID"
    )]
    [Produces("application/json")]
    [SwaggerResponse(200, "Operación exitosa", typeof(GetCalificadoraPeriodoQueryResponse))]
    [SwaggerResponse(404, "No encontrado", typeof(void))]
    public Task<GetCalificadoraPeriodoQueryResponse> GetCalificadora([FromRoute] GetCalificadoraPeriodoQuery query)
    {
        return _mediator.Send(query);
    }

    // CreateCalificadora
    [HttpPost(Name = "CreateCalificadoraPeriodo")]
    [SwaggerOperation(
        Summary = "Crear una nueva calificadoraPeriodo",
        Description = "Crea una nueva calificadoraPeriodo con la información proporcionada.",
        OperationId = "CrearCalificadoraPeriodo"
    )]
    [Consumes("application/json")]
    [SwaggerResponse(201, "Operación exitosa", typeof(void))]
    [SwaggerResponse(400, "Solicitud inválida", typeof(void))]
    public async Task<IActionResult> CreateCalificadoraPeriodo([FromBody] CreateCalificadoraPeriodoCommand command)
    {
        var response = await _mediator.Send(command);
        return CreatedAtRoute(new { response.Id }, response);
    }

    // UpdateCalificadoraPeriodo
    [HttpPut("{Id:int}", Name = "UpdateCalificadoraPeriodo")]
    [SwaggerOperation(
        Summary = "Actualizar una calificadora por ID",
        Description = "Actualiza los detalles de una calificadora específica según su ID.",
        OperationId = "ActualizarCalificadoraPorID"
    )]
    [Consumes("application/json")]
    [SwaggerResponse(200, "Operación exitosa", typeof(void))]
    [SwaggerResponse(400, "Solicitud inválida", typeof(void))]
    [SwaggerResponse(404, "No encontrado", typeof(void))]
    public async Task<IActionResult> UpdateCalificadoraPeriodo([FromBody] UpdateCalificadoraPeriodoCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    // DeleteCalificadoraPeriodo
    [HttpDelete("{Id:int}", Name = "DeleteCalificadoraPeriodo")]
    [SwaggerOperation(
        Summary = "Eliminar una calificadoraPeriodo por ID",
        Description = "Elimina una calificadoraPeriodo específica según su ID.",
        OperationId = "EliminarCalificadoraPeriodoPorID"
    )]
    [SwaggerResponse(200, "Operación exitosa", typeof(void))]
    [SwaggerResponse(404, "No encontrado", typeof(void))]
    public async Task<IActionResult> DeleteCalificadoraPeriodo([FromBody] DeleteCalificadoraPeriodoCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
}