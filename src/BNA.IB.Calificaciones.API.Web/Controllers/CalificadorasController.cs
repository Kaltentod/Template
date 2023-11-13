using BNA.IB.Calificaciones.API.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace BNA.IB.Calificaciones.API.Web.Controllers;

[ApiController]
[Route("[controller]")]
[SwaggerTag("Calificadoras")]
public class CalificadorasController : ControllerBase
{
    private readonly ILogger<CalificadorasController> _logger;
    private readonly IMediator _mediator;

    public CalificadorasController(ILogger<CalificadorasController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetCalificadoras")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = ""
    )]
    [SwaggerResponse(200, "")]
    public async Task<IEnumerable<GetCalificadorasQueryResponse>> GetCalificadoras()
    {
        _mediator.Send(new GetCalificacionesQuery());
    }

    [HttpGet("{id:int}", Name = "GetCalificadora")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = ""
    )]
    [SwaggerResponse(200, "")]
    public async Task<GetCalificadoraQueryResponse> GetCalificadora([FromRoute] GetCalificadoraQuery query)
    {
        return _mediator.Send(query);
    }
    
    [HttpPost(Name = "CreateCalificadora")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = ""
    )]
    [SwaggerResponse(200, "")]
    public async Task<IActionResult> CreateCalificadora([FromBody] CreateCalificadoraCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
    
    [HttpPut("{id:int}", Name = "UpdateCalificadora")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = ""
    )]
    [SwaggerResponse(200, "")]
    public async Task<IActionResult> UpdateCalificadora([FromBody] UpdateCalificadoraCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
    
    [HttpDelete("{id:int}", Name = "DeleteCalificadora")]
    [SwaggerOperation(
        Summary = "",
        Description = "",
        OperationId = ""
    )]
    [SwaggerResponse(200, "")]
    public async Task<IActionResult> DeleteCalificadora([FromBody] DeleteCalificadoraCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
}