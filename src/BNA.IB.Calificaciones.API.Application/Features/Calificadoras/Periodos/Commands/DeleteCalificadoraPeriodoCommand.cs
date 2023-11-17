using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Periodos.Commands;

public class DeleteCalificadoraPeriodoCommand : IRequest
{
    public int Id { get; set; }
    public int CalificadoraId { get; set; }
}

public class DeleteCalificadoraPeriodosValidator : AbstractValidator<DeleteCalificadoraPeriodoCommand>
{
    public DeleteCalificadoraPeriodosValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.CalificadoraId).NotNull().GreaterThan(0);
    }
}

public class DeleteCalificadoraPeriodosCommandHandler : IRequestHandler<DeleteCalificadoraPeriodoCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCalificadoraPeriodosCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(DeleteCalificadoraPeriodoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CalificadoraPeriodos.FindAsync(request.Id);

        if (entity is null) throw new NotFoundException();

        var tituloPersonaCalificadaValidation = await _context.TituloPersonaCalificaciones
            .AnyAsync(x => x.CalificadoraPeriodo.Id == request.CalificadoraId &&
                           (entity.FechaAlta <= x.FechaAlta || x.FechaAlta <= entity.FechaBaja) &&
                           (entity.FechaAlta <= x.FechaBaja || x.FechaBaja <= entity.FechaBaja));

        if (tituloPersonaCalificadaValidation) throw new ForbiddenException("Esta entidad tiene un Título/Persona calificado asociados.");

        _context.CalificadoraPeriodos.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}