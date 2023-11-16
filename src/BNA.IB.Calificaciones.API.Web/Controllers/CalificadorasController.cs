using BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Commands;
using BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BNA.IB.Calificaciones.API.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[SwaggerTag("Calificadoras de riesgo")]
public class CalificadorasController : ControllerBase
{
    private readonly ILogger<CalificadorasController> _logger;
    private readonly IMediator _mediator;

    public CalificadorasController(ILogger<CalificadorasController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    // GetCalificadoras
    [HttpGet(Name = "GetCalificadoras")]
    [SwaggerOperation(
        Summary = "Obtener todas las calificadoras",
        Description = "Obtiene la lista completa de calificadoras.",
        OperationId = "ObtenerCalificadoras"
    )]
    [Produces("application/json")]
    [SwaggerResponse(200, "Operación exitosa", typeof(IEnumerable<GetCalificadorasQueryResponse>))]
    [SwaggerResponse(400, "Solicitud inválida", typeof(void))]
    public async Task<IEnumerable<GetCalificadorasQueryResponse>> GetCalificadoras()
    {
        return await _mediator.Send(new GetCalificadorasQuery());
    }

    // GetCalificadora
    [HttpGet("{Id:int}", Name = "GetCalificadora")]
    [SwaggerOperation(
        Summary = "Obtener una calificadora por ID",
        Description = "Obtiene los detalles de una calificadora específica según su ID.",
        OperationId = "ObtenerCalificadoraPorID"
    )]
    [Produces("application/json")]
    [SwaggerResponse(200, "Operación exitosa", typeof(GetCalificadoraQueryResponse))]
    [SwaggerResponse(400, "Solicitud inválida", typeof(void))]
    [SwaggerResponse(404, "No encontrado", typeof(void))]
    public Task<GetCalificadoraQueryResponse> GetCalificadora([FromRoute] GetCalificadoraQuery query)
    {
        return _mediator.Send(query);
    }

    // CreateCalificadora
    [HttpPost(Name = "CreateCalificadora")]
    [SwaggerOperation(
        Summary = "Crear una nueva calificadora",
        Description = "Crea una nueva calificadora con la información proporcionada.",
        OperationId = "CrearCalificadora"
    )]
    [Consumes("application/json")]
    [SwaggerResponse(201, "Operación exitosa", typeof(void))]
    [SwaggerResponse(400, "Solicitud inválida", typeof(void))]
    public async Task<IActionResult> CreateCalificadora([FromBody] CreateCalificadoraCommand command)
    {
        var response = await _mediator.Send(command);
        return CreatedAtRoute(new { response.Id }, response);
    }

    // UpdateCalificadora
    [HttpPut("{Id:int}", Name = "UpdateCalificadora")]
    [SwaggerOperation(
        Summary = "Actualizar una calificadora por ID",
        Description = "Actualiza los detalles de una calificadora específica según su ID.",
        OperationId = "ActualizarCalificadoraPorID"
    )]
    [Consumes("application/json")]
    [SwaggerResponse(200, "Operación exitosa", typeof(void))]
    [SwaggerResponse(400, "Solicitud inválida", typeof(void))]
    [SwaggerResponse(404, "No encontrado", typeof(void))]
    public async Task<IActionResult> UpdateCalificadora([FromBody] UpdateCalificadoraCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    // DeleteCalificadora
    [HttpDelete("{Id:int}", Name = "DeleteCalificadora")]
    [SwaggerOperation(
        Summary = "Eliminar una calificadora por ID",
        Description = "Elimina una calificadora específica según su ID.",
        OperationId = "EliminarCalificadoraPorID"
    )]
    [SwaggerResponse(200, "Operación exitosa", typeof(void))]
    [SwaggerResponse(400, "Solicitud inválida", typeof(void))]
    [SwaggerResponse(404, "No encontrado", typeof(void))]
    public async Task<IActionResult> DeleteCalificadora([FromBody] DeleteCalificadoraCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
}