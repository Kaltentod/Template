using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Queries;
using BNA.IB.Calificaciones.API.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Features.TituloPersonaCalificaciones.Queries;

public class GetTituloPersonaCalificacionQuery : IRequest<GetTituloPersonaCalificacionQueryResponse>
{
    public int Id { get; set; }
}

public class GetTituloPersonaCalificacionQueryHandler : IRequestHandler<GetTituloPersonaCalificacionQuery, GetTituloPersonaCalificacionQueryResponse>
{
    private readonly IApplicationDbContext _context;

    public GetTituloPersonaCalificacionQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetTituloPersonaCalificacionQueryResponse> Handle(
        GetTituloPersonaCalificacionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _context.TituloPersonaCalificaciones.FindAsync(request.Id);

            if (entity is null)
            {
                throw new NotFoundException(nameof(TituloPersonaCalificacion), request.Id);
            }
        
            return new GetTituloPersonaCalificacionQueryResponse
            {
                Id = entity.Id,
                Clave = entity.Clave,
                FechaAlta = entity.FechaAlta,
                FechaBaja = entity.FechaBaja
            };
        }
        catch (Exception e)
        {
            throw new Exception($"Error al obtener calificaciones de titulos o personas: {e.Message}", e);
        }
    }
}

public class GetTituloPersonaCalificacionQueryResponse
{
    public int Id { get; set; }
    public TituloPersonaCalificacionTipo Tipo { get; set; }
    public string Clave { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime FechaBaja { get; set; }
}