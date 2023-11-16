using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Features.TitulosPersonasCalificados.Queries;

public class GetTitulosPersonasCalificadosQuery : IRequest<List<GetTitulosPersonasCalificadosQueryResponse>>
{
}

public class GetTitulosPersonasCalificadosQueryHandler : IRequestHandler<GetTitulosPersonasCalificadosQuery, List<GetTitulosPersonasCalificadosQueryResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetTitulosPersonasCalificadosQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<List<GetTitulosPersonasCalificadosQueryResponse>> Handle(
        GetTitulosPersonasCalificadosQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return _context.CalificadoraPeriodos
                .AsNoTracking()
                .Select(e => new GetTitulosPersonasCalificadosQueryResponse
                {
                    Id = e.Id,
                    FechaAlta = e.FechaAlta,
                    FechaBaja = e.FechaBaja
                })
                .ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al obtener las calificadoras: {e.Message}");
            throw;
        }
    }
}

public class GetTitulosPersonasCalificadosQueryResponse
{
    public int Id { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
}