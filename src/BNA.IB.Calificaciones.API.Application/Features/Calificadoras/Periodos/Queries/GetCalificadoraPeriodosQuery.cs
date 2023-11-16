using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Periodos.Queries;

public class GetCalificadoraPeriodosQuery : IRequest<GetCalificadoraPeriodosQueryResponse>
{
    public int Id { get; set; }
}

public class GetCalificadoraPeriodosQueryHandler : IRequestHandler<GetCalificadoraPeriodosQuery, GetCalificadoraPeriodosQueryResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCalificadoraPeriodosQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetCalificadoraPeriodosQueryResponse> Handle(
        GetCalificadoraPeriodosQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.CalificadoraPeriodos.FindAsync(request.Id);

        return new GetCalificadoraPeriodosQueryResponse
        {
            Id = entity.Id,
            FechaAlta = entity.FechaAlta,
            FechaBaja = entity.FechaBaja,
        };
    }
}

public class GetCalificadoraPeriodosQueryResponse
{
    public int Id { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
}