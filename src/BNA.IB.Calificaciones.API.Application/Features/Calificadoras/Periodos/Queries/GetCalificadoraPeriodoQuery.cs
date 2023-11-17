using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Periodos.Queries;

public class GetCalificadoraPeriodoQuery : IRequest<GetCalificadoraPeriodoQueryResponse>
{
    public int Id { get; set; }
}

public class GetCalificadoraPeriodoQueryHandler : IRequestHandler<GetCalificadoraPeriodoQuery, GetCalificadoraPeriodoQueryResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCalificadoraPeriodoQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetCalificadoraPeriodoQueryResponse> Handle(
        GetCalificadoraPeriodoQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.CalificadoraPeriodos
            .Include(x => x.Equivalencias)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (entity is null) throw new NotFoundException(nameof(CalificadoraPeriodo), request.Id);

        return new GetCalificadoraPeriodoQueryResponse
        {
            Id = entity.Id,
            FechaAlta = entity.FechaAlta,
            FechaBaja = entity.FechaBaja,
            FechaAltaBCRA = entity.FechaAltaBCRA,
            FechaBajaBCRA = entity.FechaBajaBCRA,
            Equivalencias = entity.Equivalencias.ToDictionary(e => e.BcraCalificacionId, e => e.CalificacionCalificadora)
        };
    }
}

public class GetCalificadoraPeriodoQueryResponse
{
    public int Id { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
    public DateTime FechaAltaBCRA { get; set; }
    public DateTime? FechaBajaBCRA { get; set; }
    public Dictionary<int, string> Equivalencias { get; set; }
}