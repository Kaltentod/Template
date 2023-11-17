using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Domain.Entities;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.TituloPersonaCalificaciones.Commands;

public class UpdateTituloPersonaCalificacionCommand : IRequest
{
    public int Id { get; set; }
    public int CalificadoraPeriodoId { get; set; }
    public int BCRACalificacionId { get; set; }
    public TituloPersonaCalificacionTipo TituloPersonaCalificacionTipo { get; set; }
    public string Clave { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime FechaBaja { get; set; }
}

public class UpdateTituloPersonaCalificacionCommandHandler : IRequestHandler<UpdateTituloPersonaCalificacionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTituloPersonaCalificacionCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(UpdateTituloPersonaCalificacionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TituloPersonaCalificaciones.FindAsync(request.Id);

        if (entity is not null)
        {
            entity.Tipo = request.TituloPersonaCalificacionTipo;
            entity.Clave = request.Clave;
            entity.FechaAlta = request.FechaAlta;
            entity.FechaBaja = request.FechaBaja;
            entity.CalificadoraPeriodoId = request.CalificadoraPeriodoId;
            entity.BCRACalificacionId = request.BCRACalificacionId;
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}