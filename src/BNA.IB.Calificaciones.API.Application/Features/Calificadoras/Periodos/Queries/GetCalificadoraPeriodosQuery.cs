using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Periodos.Queries;

public class GetCalificadoraPeriodosQuery : IRequest<List<GetCalificadorasPeriodosQueryResponse>>
{
    public int CalificadoraId { get; set; }
}

public class GetCalificadorasPeriodosQueryHandler : IRequestHandler<GetCalificadoraPeriodosQuery, List<GetCalificadorasPeriodosQueryResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetCalificadorasPeriodosQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<List<GetCalificadorasPeriodosQueryResponse>> Handle(
        GetCalificadoraPeriodosQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return _context.CalificadoraPeriodos
                .Include(x => x.Equivalencias)
                .Where(x => x.Calificadora.Id == request.CalificadoraId)
                .AsNoTracking()
                .Select(entity => new GetCalificadorasPeriodosQueryResponse
                {
                    Id = entity.Id,
                    FechaAlta = entity.FechaAlta,
                    FechaBaja = entity.FechaBaja,
                    FechaAltaBCRA = entity.FechaAltaBCRA,
                    FechaBajaBCRA = entity.FechaBajaBCRA,
                    //Equivalencias = entity.Equivalencias.ToDictionary(e => e.BcraCalificacionId, e => e.CalificacionCalificadora)
                })
                .ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception($"Error al obtener los periodos de la calificadora ({request.CalificadoraId}): {e.Message}", e);
        }
    }
}

public class GetCalificadorasPeriodosQueryResponse
{
    public int Id { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
    public DateTime FechaAltaBCRA { get; set; }
    public DateTime? FechaBajaBCRA { get; set; }
    //public Dictionary<int, string> Equivalencias { get; set; }
}