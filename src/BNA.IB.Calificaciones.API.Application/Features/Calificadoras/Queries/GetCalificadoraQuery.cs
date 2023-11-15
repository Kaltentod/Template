using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Queries;

public class GetCalificadoraQuery : IRequest<GetCalificadoraQueryResponse>
{
    public int Id { get; set; }
}

public class GetCalificadoraQueryHandler : IRequestHandler<GetCalificadoraQuery, GetCalificadoraQueryResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCalificadoraQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetCalificadoraQueryResponse> Handle(
        GetCalificadoraQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Calificadoras.FindAsync(request.Id);

        return new GetCalificadoraQueryResponse
        {
            Id = entity.Id,
            Clave = entity.Clave,
            Nombre = entity.Nombre,
            FechaAlta = entity.FechaAlta,
            FechaAltaBCRA = entity.FechaAltaBCRA,
            FechaBaja = entity.FechaBaja,
            FechaBajaBCRA = entity.FechaBajaBCRA
        };
    }
}

public class GetCalificadoraQueryResponse
{
    public int Id { get; set; }
    public int Clave { get; set; }
    public string Nombre { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime FechaAltaBCRA { get; set; }
    public DateTime FechaBaja { get; set; }
    public DateTime FechaBajaBCRA { get; set; }
}