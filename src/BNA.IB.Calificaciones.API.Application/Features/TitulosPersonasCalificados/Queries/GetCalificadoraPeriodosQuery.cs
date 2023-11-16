using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.TitulosPersonasCalificados.Queries;

public class GetTituloPersonaCalificadoQuery : IRequest<GetTituloPersonaCalificadoQueryResponse>
{
    public int Id { get; set; }
}

public class GetTituloPersonaCalificadoQueryHandler : IRequestHandler<GetTituloPersonaCalificadoQuery, GetTituloPersonaCalificadoQueryResponse>
{
    private readonly IApplicationDbContext _context;

    public GetTituloPersonaCalificadoQueryHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<GetTituloPersonaCalificadoQueryResponse> Handle(
        GetTituloPersonaCalificadoQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.TituloPersonaCalificadas.FindAsync(request.Id);

        return new GetTituloPersonaCalificadoQueryResponse
        {
            Id = entity.Id,
            FechaAlta = entity.FechaAlta,
            FechaBaja = entity.FechaBaja,
        };
    }
}

public class GetTituloPersonaCalificadoQueryResponse
{
    public int Id { get; set; }
    public DateTime FechaAlta { get; set; }
    public DateTime? FechaBaja { get; set; }
}