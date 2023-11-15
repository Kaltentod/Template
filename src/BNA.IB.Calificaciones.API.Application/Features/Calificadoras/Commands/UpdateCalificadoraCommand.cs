using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Commands;

public class UpdateCalificadoraCommand : IRequest
{
    public int Id { get; set; }
    public int Clave { get; set; }
    public string Nombre { get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly FechaAltaBCRA { get; set; }
    public DateOnly FechaBaja { get; set; }
    public DateOnly FechaBajaBCRA { get; set; }
}

public class UpdateCalificadoraCommandHandler : IRequestHandler<UpdateCalificadoraCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCalificadoraCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(UpdateCalificadoraCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Calificadoras.FindAsync(request.Id);

        if (entity is not null)
        {
            entity.Clave = request.Clave;
            entity.Nombre = request.Nombre;
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}