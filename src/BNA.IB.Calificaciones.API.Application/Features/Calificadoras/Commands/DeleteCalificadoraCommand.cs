using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Domain.Entities;
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
        
        if (entity is null)
        {
            throw new NotFoundException(nameof(Calificadora), request.Id);
        }

        _context.Calificadoras.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}