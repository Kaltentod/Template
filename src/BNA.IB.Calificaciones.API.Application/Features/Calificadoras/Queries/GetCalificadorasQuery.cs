using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Feature.Calificadoras.Queries;

public class GetCalificadorasQuery : IRequest<List<GetCalificadorasQueryResponse>>
{
}

public class GetProductsQueryHandler : IRequestHandler<GetCalificadorasQuery, List<GetCalificadorasQueryResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<GetCalificadorasQueryResponse>> Handle(
        GetCalificadorasQuery request, CancellationToken cancellationToken) =>
        _context.Calificadoras
            .AsNoTracking()
            .Select(s => new GetCalificadorasQueryResponse
            {
                Id = s.Id,
                Clave = s.Clave,
                Nombre = s.Nombre
            })
            .ToListAsync();
}

public class GetCalificadorasQueryResponse
{
    public int Id { get; set; }
    public int Clave { get; set; }
    public string Nombre { get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly FechaAltaBCRA { get; set; }
    public DateOnly FechaBaja { get; set; }
    public DateOnly FechaBajaBCRA { get; set; }
}