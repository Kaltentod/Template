using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Features.TitulosPersonasCalificados.Commands;

public class CreateTituloPersonaCalificadoCommand : IRequest<CreateTituloPersonaCalificadoCommandResponse>
{
    public int CalificadoraPeriodoId { get; set; }
    public int BcraCalificacionId { get; set; }
    public TituloPersonaCalificadaTipo TituloPersonaCalificadaTipo {  get; set; }
    public string Clave {  get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly? FechaBaja { get; set; } = DateOnly.FromDateTime(Const.FechaMax);
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

        var tituloSuperpuesto = _context.TituloPersonaCalificadas
            .Any(x => x.Clave == request.Clave && x.Tipo == request.TituloPersonaCalificadaTipo 
                && (
                    (DateOnly.FromDateTime(x.FechaAlta) <= request.FechaAlta && request.FechaAlta <= DateOnly.FromDateTime(x.FechaBaja))
                    || (DateOnly.FromDateTime(x.FechaAlta) <= request.FechaBaja && request.FechaBaja <= DateOnly.FromDateTime(x.FechaBaja))
                    || 
                    ((request.FechaAlta <= DateOnly.FromDateTime(x.FechaAlta) && DateOnly.FromDateTime(x.FechaAlta) <= request.FechaBaja)
                    || request.FechaAlta <= DateOnly.FromDateTime(x.FechaBaja) && request.FechaBaja <= request.FechaBaja))
                );



        var entity = new TituloPersonaCalificada
        {
            Clave = request.Clave,
            FechaAlta = request.FechaAlta.ToDateTime(TimeOnly.MinValue),
            FechaBaja = request.FechaBaja!.Value.ToDateTime(TimeOnly.MinValue),
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