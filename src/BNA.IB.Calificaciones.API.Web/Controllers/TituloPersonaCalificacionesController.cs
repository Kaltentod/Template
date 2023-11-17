using BNA.IB.Calificaciones.API.Application.Features.TituloPersonaCalificaciones.Commands;
using BNA.IB.Calificaciones.API.Application.Features.TituloPersonaCalificaciones.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BNA.IB.Calificaciones.API.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[SwaggerTag("TituloPersonaCalificaciones")]
public class TituloPersonaCalificacionesController : ControllerBase
{
    private readonly ILogger<TituloPersonaCalificacionesController> _logger;
    private readonly IMediator _mediator;

    public TituloPersonaCalificacionesController(ILogger<TituloPersonaCalificacionesController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    // GetTituloPersonaCalificaciones
    [HttpGet(Name = "GetTituloPersonaCalificaciones")]
    [SwaggerOperation(
        Summary = "Obtener todas las calificaciones de titulos o personas.",
        Description = "Obtiene la lista completa de TituloPersonaCalificaciones.",
        OperationId = "ObtenerTituloPersonaCalificaciones"
    )]
    [Produces("application/json")]
    [SwaggerResponse(200, "Operación exitosa", typeof(IEnumerable<GetTituloPersonaCalificacionesQueryResponse>))]
    [SwaggerResponse(400, "Solicitud inválida", typeof(void))]
    public async Task<IEnumerable<GetTituloPersonaCalificacionesQueryResponse>> GetTituloPersonaCalificaciones()
    {
        return await _mediator.Send(new GetTituloPersonaCalificacionesQuery());
    }

    // GetTituloPersonaCalificacion
    [HttpGet("{Id:int}", Name = "GetTituloPersonaCalificacion")]
    [SwaggerOperation(
        Summary = "Obtener una TituloPersonaCalificacion por ID",
        Description = "Obtiene los detalles de una TituloPersonaCalificacion específica según su ID.",
        OperationId = "ObtenerTituloPersonaCalificacionPorID"
    )]
    [Produces("application/json")]
    [SwaggerResponse(200, "Operación exitosa", typeof(GetTituloPersonaCalificacionQueryResponse))]
    [SwaggerResponse(400, "Solicitud inválida", typeof(void))]
    [SwaggerResponse(404, "No encontrado", typeof(void))]
    public Task<GetTituloPersonaCalificacionQueryResponse> GetTituloPersonaCalificacion([FromRoute] GetTituloPersonaCalificacionQuery query)
    {
        return _mediator.Send(query);
    }

    // CreateCalificadora
    [HttpPost(Name = "CreateTituloPersonaCalificacion")]
    [SwaggerOperation(
        Summary = "Crear una nueva TituloPersonaCalificacion",
        Description = "Crea una nueva TituloPersonaCalificacion con la información proporcionada.",
        OperationId = "CrearCalificadora"
    )]
    [Consumes("application/json")]
    [SwaggerResponse(201, "Operación exitosa", typeof(void))]
    [SwaggerResponse(400, "Solicitud inválida", typeof(void))]
    public async Task<IActionResult> CreateTituloPersonaCalificacion([FromBody] CreateTituloPersonaCalificacionCommand command)
    {
        var response = await _mediator.Send(command);
        return CreatedAtRoute( "GetCalificadora",new { response.Id }, response);
    }

    // UpdateCalificadora
    [HttpPut("{Id:int}", Name = "UpdateTituloPersonaCalificacion")]
    [SwaggerOperation(
        Summary = "Actualizar una TituloPersonaCalificacion por ID",
        Description = "Actualiza los detalles de una TituloPersonaCalificacion específica según su ID.",
        OperationId = "ActualizarTituloPersonaCalificacionPorID"
    )]
    [Consumes("application/json")]
    [SwaggerResponse(200, "Operación exitosa", typeof(void))]
    [SwaggerResponse(400, "Solicitud inválida", typeof(void))]
    [SwaggerResponse(404, "No encontrado", typeof(void))]
    public async Task<IActionResult> UpdateCalificadora([FromBody] UpdateTituloPersonaCalificacionCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    // DeleteCalificadora
    [HttpDelete("{Id:int}", Name = "DeleteTituloPersonaCalificacion")]
    [SwaggerOperation(
        Summary = "Eliminar una TituloPersonaCalificacion por ID",
        Description = "Elimina una TituloPersonaCalificacion específica según su ID.",
        OperationId = "EliminarTituloPersonaCalificacionPorID"
    )]
    [SwaggerResponse(200, "Operación exitosa", typeof(void))]
    [SwaggerResponse(400, "Solicitud inválida", typeof(void))]
    [SwaggerResponse(404, "No encontrado", typeof(void))]
    public async Task<IActionResult> DeleteTituloPersonaCalificacion([FromBody] DeleteTituloPersonaCalificacionCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
}