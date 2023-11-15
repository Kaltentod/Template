using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Commands;

public class DeleteCalificadoraCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteCalificadoraCommandHandler : IRequestHandler<DeleteCalificadoraCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCalificadoraCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(DeleteCalificadoraCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Calificadoras.FindAsync(request.Id);
        if (entity is not null) _context.Calificadoras.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}