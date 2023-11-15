using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Domain.Entities;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.TituloPersonaCalificado.Commands;

public class UpdateTituloPersonaCalificadoCommand : IRequest
{
    public int Id { get; set; }
    public int CalificadoraId { get; set; }
    public int BcraCalificacionId { get; set; }
    public TituloPersonaCalificadaTipo TituloPersonaCalificadaTipo { get; set; }
    public string Clave { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
}

public class UpdateTituloPersonaCalificadoCommandHandler : IRequestHandler<UpdateTituloPersonaCalificadoCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTituloPersonaCalificadoCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(UpdateTituloPersonaCalificadoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TituloPersonaCalificadas.FindAsync(request.Id);

        if (entity is not null)
        {
            entity.Tipo = request.TituloPersonaCalificadaTipo;
            entity.Clave = request.Clave;
            entity.FechaAlta = request.FechaAlta;
            entity.FechaBaja = request.FechaBaja;
            entity.Calificadora.Id = request.CalificadoraId;
            entity.BcraCalificacion.Id = request.BcraCalificacionId;
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}