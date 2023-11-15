using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Queries;

public class GetCalificadorasQuery : IRequest<List<GetCalificadorasQueryResponse>>
{
}

public class GetCalificadorasQueryHandler : IRequestHandler<GetCalificadorasQuery, List<GetCalificadorasQueryResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetCalificadorasQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<List<GetCalificadorasQueryResponse>> Handle(
        GetCalificadorasQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return _context.Calificadoras
                .AsNoTracking()
                .Select(e => new GetCalificadorasQueryResponse
                {
                    Id = e.Id,
                    Clave = e.Clave,
                    Nombre = e.Nombre,
                    FechaAlta = e.FechaAlta,
                    FechaAltaBCRA = e.FechaAltaBCRA,
                    FechaBaja = e.FechaBajaBCRA,
                    FechaBajaBCRA = e.FechaBajaBCRA
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

public class GetCalificadorasQueryResponse
{
    public int Id { get; set; }
    public int Clave { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaAltaBCRA { get; set; }
    public DateTime? FechaBaja { get; set; }
    public DateTime? FechaBajaBCRA { get; set; }
}