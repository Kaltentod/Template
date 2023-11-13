using MediatR;

namespace BNA.IB.Calificaciones.API.Web.Controllers;

public class PeriodosController
{
    private readonly ILogger<PeriodosController> _logger;
    private readonly IMediator _mediator;

    public PeriodosController(ILogger<PeriodosController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    
}