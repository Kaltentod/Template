using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.CalificadorasPeriodos.Commands;

public class CreateCalificadoraPeriodosCommand : IRequest<CreateCalificadoraPeriodosCommandResponse>
{
    public int CalificadoraId { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
}

public class
    CreateCalificadoraPeriodosCommandHandler : IRequestHandler<CreateCalificadoraPeriodosCommand, CreateCalificadoraPeriodosCommandResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateCalificadoraPeriodosCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreateCalificadoraPeriodosCommandResponse> Handle(CreateCalificadoraPeriodosCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new CalificadoraPeriodo
        {
            FechaAlta = request.FechaAlta,
            FechaBaja = request.FechaBaja
        };

        _context.CalificadoraPeriodos.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateCalificadoraPeriodosCommandResponse { Id = entity.Id };
    }
}

public class CreateCalificadoraPeriodosCommandResponse
{
    public int Id { get; set; }
}