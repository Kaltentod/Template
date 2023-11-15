using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.CalificadorasPeriodos.Commands;

public class DeleteCalificadoraPeriodosCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteCalificadoraPeriodosCommandHandler : IRequestHandler<DeleteCalificadoraPeriodosCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCalificadoraPeriodosCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(DeleteCalificadoraPeriodosCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CalificadoraPeriodos.FindAsync(request.Id);
        if (entity is not null) _context.CalificadoraPeriodos.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}