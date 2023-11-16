using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Domain;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Periodos.Commands;

public class UpdateCalificadoraPeriodosCommand : IRequest
{
    public int Id { get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly? FechaBaja { get; set; } = DateOnly.FromDateTime(Const.FechaMax);
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
            entity.FechaAlta = request.FechaAlta.ToDateTime(TimeOnly.MinValue);
            entity.FechaBaja = request.FechaBaja!.Value.ToDateTime(TimeOnly.MinValue);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}