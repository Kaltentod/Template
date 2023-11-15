using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.TituloPersonaCalificado.Commands;

public class DeleteTituloPersonaCalificadoCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteTituloPersonaCalificadoCommandHandler : IRequestHandler<DeleteTituloPersonaCalificadoCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTituloPersonaCalificadoCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(DeleteTituloPersonaCalificadoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TituloPersonaCalificadas.FindAsync(request.Id);
        if (entity is not null) _context.TituloPersonaCalificadas.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}