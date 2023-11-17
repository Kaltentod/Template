using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Commands;
using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities;
using FluentValidation;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Periodos.Commands;

public class UpdateCalificadoraPeriodoCommand : IRequest
{
    public int Id { get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly? FechaBaja { get; set; } = DateOnly.FromDateTime(Const.FechaMax);
    public DateOnly FechaAltaBCRA { get; set; }
    public DateOnly? FechaBajaBCRA { get; set; } = DateOnly.FromDateTime(Const.FechaMax);
    public Dictionary<int, string> Equivalencias { get; set; } = new();
}

public class UpdateCalificadoraPeriodosValidator : AbstractValidator<UpdateCalificadoraPeriodoCommand>
{
    public UpdateCalificadoraPeriodosValidator()
    {
        RuleFor(x => x.Id).NotNull()
            .WithMessage("Id no puede ser nulo.");
        
        RuleFor(x => x.FechaAlta).NotNull()
            .WithMessage("La fecha de alta no puede ser nula.");
        
        RuleFor(x => x.FechaAltaBCRA).NotNull()
            .WithMessage("La fecha de alta no puede ser nula.");
        
        RuleFor(x => x.FechaBaja).GreaterThanOrEqualTo(x => x.FechaAlta)
            .When(x => x.FechaBaja.HasValue)
            .WithMessage("La fecha de baja debe ser mayor o igual a la fecha de alta.");

        RuleFor(x => x.FechaBajaBCRA)
            .GreaterThanOrEqualTo(x => x.FechaAltaBCRA)
            .When(x => x.FechaBajaBCRA.HasValue)
            .WithMessage("La fecha de baja en BCRA debe ser mayor o igual a la fecha de alta en BCRA.");
    }
}

public class UpdateCalificadoraPeriodosCommandHandler : IRequestHandler<UpdateCalificadoraPeriodoCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCalificadoraPeriodosCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(UpdateCalificadoraPeriodoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.CalificadoraPeriodos.FindAsync(request.Id);

        if (entity is null) throw new NotFoundException();

        entity.FechaAlta = request.FechaAlta.ToDateTime(TimeOnly.MinValue);
        entity.FechaAltaBCRA = request.FechaAltaBCRA.ToDateTime(TimeOnly.MinValue);
        entity.FechaBaja = request.FechaBaja!.Value.ToDateTime(TimeOnly.MinValue);
        entity.FechaBajaBCRA = request.FechaBajaBCRA!.Value.ToDateTime(TimeOnly.MinValue);
        entity.Equivalencias = request.Equivalencias.Select(kv => new Equivalencia
            { BcraCalificacionId = kv.Key, CalificacionCalificadora = kv.Value }).ToList();

        await _context.SaveChangesAsync(cancellationToken);
    }
}