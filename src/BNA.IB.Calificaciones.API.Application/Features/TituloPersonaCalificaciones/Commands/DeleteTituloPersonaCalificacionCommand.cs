using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.TituloPersonaCalificaciones.Commands;

public class DeleteTituloPersonaCalificacionCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteTituloPersonaCalificacionCommandHandler : IRequestHandler<DeleteTituloPersonaCalificacionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTituloPersonaCalificacionCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(DeleteTituloPersonaCalificacionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TituloPersonaCalificaciones.FindAsync(request.Id);
        if (entity is not null) _context.TituloPersonaCalificaciones.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}