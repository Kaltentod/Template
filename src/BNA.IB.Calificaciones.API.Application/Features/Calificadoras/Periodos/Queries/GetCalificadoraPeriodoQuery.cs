using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Domain.Entities;
using MediatR;

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
        var entity = await _context.CalificadoraPeriodos.FindAsync(request.Id);

        if (entity is null)
        {
            throw new NotFoundException(nameof(CalificadoraPeriodo), request.Id);
        }

        return new GetCalificadoraPeriodoQueryResponse
        {
            Id = entity.Id,
            FechaAlta = entity.FechaAlta,
            FechaBaja = entity.FechaBaja,
            Equivalencias = entity.PeriodoCalificadoraEquivalencias
        };
    }
}

public class GetCalificadoraPeriodoQueryResponse
{
    public int Id { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
    public ICollection<CalificadoraPeriodoEquivalencia> Equivalencias { get; set; }
}