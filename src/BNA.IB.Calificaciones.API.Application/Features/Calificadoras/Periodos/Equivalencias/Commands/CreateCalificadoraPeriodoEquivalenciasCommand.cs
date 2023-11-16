using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Domain.Entities;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Periodos.Equivalencias.Commands;

public class CreateCalificadoraPeriodoEquivalenciaCommand : IRequest<CreateCalificadoraPeriodoEquivalenciasCommandResponse>
{
    public int PeriodoId { get; set; }
    public List<Equivalencia> Equivalencias { get; set; }
}

public class
    CreateCalificadoraPeriodoEquivalenciasCommandHandler : IRequestHandler<CreateCalificadoraPeriodoEquivalenciaCommand, CreateCalificadoraPeriodoEquivalenciasCommandResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateCalificadoraPeriodoEquivalenciasCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreateCalificadoraPeriodoEquivalenciasCommandResponse> Handle(CreateCalificadoraPeriodoEquivalenciaCommand request,
        CancellationToken cancellationToken)
    {
        var periodo = await _context.Calificadoras.FindAsync(request.PeriodoId);

        if (periodo == null) throw new NotFoundException();

        if (request.Equivalencias.Count == 0)
        {
            throw new ForbiddenException("No hay equivalencias.");
        }

        var entity = new CalificadoraPeriodoEquivalencia
        {
            Equivalencias = request.Equivalencias
        };

        _context.CalificadoraPeriodoEquivalencias.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateCalificadoraPeriodoEquivalenciasCommandResponse { Id = entity.Id };
    }
}

public class CreateCalificadoraPeriodoEquivalenciasCommandResponse
{
    public int Id { get; set; }
}