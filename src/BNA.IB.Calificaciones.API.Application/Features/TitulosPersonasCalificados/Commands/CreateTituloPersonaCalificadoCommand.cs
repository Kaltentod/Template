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
    /*
    static TDestino CopiarPropiedades<TOrigen, TDestino>(TOrigen origen, TDestino destino)
    {
        var propiedadesOrigen = typeof(TOrigen).GetProperties();
        var propiedadesDestino = typeof(TDestino).GetProperties();

        foreach (var propiedadOrigen in propiedadesOrigen)
        {
            var propiedadDestino = propiedadesDestino.FirstOrDefault(p => p.Name == propiedadOrigen.Name);

            if (propiedadDestino != null && propiedadDestino.PropertyType == propiedadOrigen.PropertyType)
            {
                var valor = propiedadOrigen.GetValue(origen);
                propiedadDestino.SetValue(destino, valor);
            }
        }
        return destino;
    }

    static TDestino CopiarPropiedades<TOrigen, TDestino>(TOrigen origen) where TDestino : new()
    {
        var destino = new TDestino();
        var propiedadesOrigen = typeof(TOrigen).GetProperties();
        var propiedadesDestino = typeof(TDestino).GetProperties();

        foreach (var propiedadOrigen in propiedadesOrigen)
        {
            var propiedadDestino = propiedadesDestino.FirstOrDefault(p => p.Name == propiedadOrigen.Name);

            if (propiedadDestino != null && propiedadDestino.PropertyType == propiedadOrigen.PropertyType)
            {
                var valor = propiedadOrigen.GetValue(origen);
                propiedadDestino.SetValue(destino, valor);
            }
        }
        return destino;
    }*/
}

public class CreateTituloPersonaCalificadoCommandResponse
{
    public int Id { get; set; }
}