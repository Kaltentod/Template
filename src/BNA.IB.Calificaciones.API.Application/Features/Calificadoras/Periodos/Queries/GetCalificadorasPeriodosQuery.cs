using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Periodos.Queries;

public class GetCalificadorasPeriodosQuery : IRequest<List<GetCalificadorasPeriodosQueryResponse>>
{
}

public class GetCalificadorasPeriodosQueryHandler : IRequestHandler<GetCalificadorasPeriodosQuery, List<GetCalificadorasPeriodosQueryResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetCalificadorasPeriodosQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<List<GetCalificadorasPeriodosQueryResponse>> Handle(
        GetCalificadorasPeriodosQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return _context.CalificadoraPeriodos
                .AsNoTracking()
                .Select(e => new GetCalificadorasPeriodosQueryResponse
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

public class GetCalificadorasPeriodosQueryResponse
{
    public int Id { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
}