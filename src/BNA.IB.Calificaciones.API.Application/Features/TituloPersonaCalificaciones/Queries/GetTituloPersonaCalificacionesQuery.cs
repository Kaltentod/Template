using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Features.TituloPersonaCalificaciones.Queries;

public class GetTituloPersonaCalificacionesQuery : IRequest<List<GetTituloPersonaCalificacionesQueryResponse>>
{
}

public class GetTituloPersonaCalificacionesQueryHandler : IRequestHandler<GetTituloPersonaCalificacionesQuery,
    List<GetTituloPersonaCalificacionesQueryResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetTituloPersonaCalificacionesQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<GetTituloPersonaCalificacionesQueryResponse>> Handle(
        GetTituloPersonaCalificacionesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.TituloPersonaCalificaciones
                .AsNoTracking()
                .Select(entity => new GetTituloPersonaCalificacionesQueryResponse
                {
                    Id = entity.Id,
                    Tipo = entity.Tipo,
                    Clave = entity.Clave,
                    FechaAlta = entity.FechaAlta,
                    FechaBaja = entity.FechaBaja,
                })
                .ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception($"Error al obtener las calificadoras: {e.Message}", e);
        }
    }
}

public class GetTituloPersonaCalificacionesQueryResponse
{
    public int Id { get; set; }
    public TituloPersonaCalificacionTipo Tipo { get; set; }
    public string Clave { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime FechaBaja { get; set; }
}