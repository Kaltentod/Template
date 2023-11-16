using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.CalificadorasPeriodos.Queries;

public class GetCalificadoraPeriodoEquivalenciaEquivalenciaQuery : IRequest<GetCalificadoraPeriodoEquivalenciaQueryResponse>
{
    public int Id { get; set; }
}

public class GetCalificadoraPeriodoEquivalenciaQueryHandler : IRequestHandler<GetCalificadoraPeriodoEquivalenciaEquivalenciaQuery, GetCalificadoraPeriodoEquivalenciaQueryResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCalificadoraPeriodoEquivalenciaQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetCalificadoraPeriodoEquivalenciaQueryResponse> Handle(
        GetCalificadoraPeriodoEquivalenciaEquivalenciaQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.CalificadoraPeriodoEquivalencia.FindAsync(request.Id);

        return new GetCalificadoraPeriodoEquivalenciaQueryResponse
        {
            Id = entity.Id,
            FechaAlta = entity.FechaAlta,
            FechaBaja = entity.FechaBaja,
        };
    }
}

public class GetCalificadoraPeriodoEquivalenciaQueryResponse
{
    public int Id { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
}