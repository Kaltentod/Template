using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ValidationException = BNA.IB.Calificaciones.API.Application.Exceptions.ValidationException;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Periodos.Commands;

public class DeleteCalificadoraPeriodosCommand : IRequest
{
    public int Id { get; set; }
    public int CalificadoraId { get; set; }
}

public class DeleteCalificadoraPeriodosValidator : AbstractValidator<DeleteCalificadoraPeriodosCommand>
{
    public DeleteCalificadoraPeriodosValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.CalificadoraId).NotNull().GreaterThan(0);
    }
}

public class DeleteCalificadoraPeriodosCommandHandler : IRequestHandler<DeleteCalificadoraPeriodosCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCalificadoraPeriodosCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(DeleteCalificadoraPeriodosCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CalificadoraPeriodos.FindAsync(request.Id);

        if (entity is null) throw new NotFoundException();

        var tituloPersonaCalificadaValidation = await _context.TituloPersonaCalificadas
            .AnyAsync(x => x.Calificadora.Id == request.CalificadoraId && 
                           (x.FechaAlta <= entity.FechaAlta || entity.FechaAlta <= x.FechaBaja) && 
                           (x.FechaAlta <= entity.FechaBaja || entity.FechaBaja <= x.FechaBaja));

        if (tituloPersonaCalificadaValidation) throw new ForbiddenException("Esta entidad tiene un Título/Persona calificado asociados.");

        _context.CalificadoraPeriodos.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}