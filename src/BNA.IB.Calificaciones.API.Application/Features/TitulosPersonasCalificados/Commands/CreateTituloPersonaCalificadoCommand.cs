using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Features.TituloPersonaCalificado.Commands;

public class CreateTituloPersonaCalificadoCommand : IRequest<CreateTituloPersonaCalificadoCommandResponse>
{
    public int CalificadoraId { get; set; }
    public int BcraCalificacionId { get; set; }
    public TituloPersonaCalificadaTipo TituloPersonaCalificadaTipo {  get; set; }
    public string Clave {  get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
}


public class
    CreateTituloPersonaCalificadoCommandHandler : IRequestHandler<CreateTituloPersonaCalificadoCommand, CreateTituloPersonaCalificadoCommandResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateTituloPersonaCalificadoCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreateTituloPersonaCalificadoCommandResponse> Handle(CreateTituloPersonaCalificadoCommand request,
        CancellationToken cancellationToken)
    {

        //var newEntity = CopiarPropiedades<CreateTituloPersonaCalificadoCommand, TituloPersonaCalificada>(request);

        var tituloSuperpuesto = _context.TituloPersonaCalificadas.Any(x => x.Clave != request.Clave && (x.FechaAlta <= request.FechaAlta || request.FechaAlta <= request.FechaBaja) && (x.FechaAlta <= request.FechaBaja || request.FechaBaja <= request.FechaBaja))

        var entity = new TituloPersonaCalificada
        {
            Clave = request.Clave,
            FechaAlta = request.FechaAlta,
            FechaBaja = request.FechaBaja,
            Calificadora = new Calificadora
            {
                Id = request.CalificadoraId
            },
            BcraCalificacion = new BCRACalificacion
            {
                Id = request.BcraCalificacionId
            }
        };

        _context.TituloPersonaCalificadas.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateTituloPersonaCalificadoCommandResponse { Id = entity.Id };
    }
}

public class CreateTituloPersonaCalificadoCommandResponse
{
    public int Id { get; set; }
}