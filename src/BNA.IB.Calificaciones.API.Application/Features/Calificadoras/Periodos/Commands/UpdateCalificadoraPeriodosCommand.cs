using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.CalificadorasPeriodos.Commands;

public class UpdateCalificadoraPeriodosCommand : IRequest
{
    public int Id { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
}

public class UpdateCalificadoraPeriodosCommandHandler : IRequestHandler<UpdateCalificadoraPeriodosCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCalificadoraPeriodosCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(UpdateCalificadoraPeriodosCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CalificadoraPeriodos.FindAsync(request.Id);

        if (entity is not null)
        {
            entity.FechaAlta = request.FechaAlta;
            entity.FechaBaja = request.FechaBaja;
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}