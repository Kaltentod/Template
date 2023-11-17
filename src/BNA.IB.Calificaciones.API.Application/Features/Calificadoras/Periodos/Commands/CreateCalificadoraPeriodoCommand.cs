using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities;
using FluentValidation;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Periodos.Commands;

public class CreateCalificadoraPeriodoCommand : IRequest<CreateCalificadoraPeriodosCommandResponse>
{
    public int CalificadoraId { get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly? FechaBaja { get; set; } = DateOnly.FromDateTime(Const.FechaMax);
    public DateOnly FechaAltaBCRA { get; set; }
    public DateOnly? FechaBajaBCRA { get; set; } = DateOnly.FromDateTime(Const.FechaMax);
    public Dictionary<int, string> Equivalencias { get; set; } = new();
}

public class CreateCalificadoraPeriodoValidator : AbstractValidator<CreateCalificadoraPeriodoCommand>
{
    public CreateCalificadoraPeriodoValidator()
    {
        RuleFor(x => x.CalificadoraId).NotNull()
            .WithMessage("Calificadora no puede ser nulo.");
        
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

public class
    CreateCalificadoraPeriodosCommandHandler : IRequestHandler<CreateCalificadoraPeriodoCommand, CreateCalificadoraPeriodosCommandResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateCalificadoraPeriodosCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreateCalificadoraPeriodosCommandResponse> Handle(CreateCalificadoraPeriodoCommand request,
        CancellationToken cancellationToken)
    {
        var calificadora = await _context.Calificadoras.FindAsync(request.CalificadoraId);

        if (calificadora is null)
        {
            throw new NotFoundException(nameof(Calificadora), request.CalificadoraId);
        }

        if (calificadora.Periodos.Any(x => (x.FechaAlta <= request.FechaAlta.ToDateTime(TimeOnly.MinValue) || request.FechaAlta <= request.FechaBaja) && (x.FechaAlta <= request.FechaBaja!.Value.ToDateTime(TimeOnly.MinValue) || request.FechaBaja <= request.FechaBaja)))
            throw new ForbiddenException("Esta entidad ya existe.");

        var entity = new CalificadoraPeriodo
        {
            FechaAlta = request.FechaAlta.ToDateTime(TimeOnly.MinValue),
            FechaAltaBCRA = request.FechaAltaBCRA.ToDateTime(TimeOnly.MinValue),
            FechaBaja = request.FechaBaja!.Value.ToDateTime(TimeOnly.MinValue),
            FechaBajaBCRA = request.FechaBajaBCRA!.Value.ToDateTime(TimeOnly.MinValue),
            Equivalencias = request.Equivalencias.Select(kv => new Equivalencia { BcraCalificacionId = kv.Key, CalificacionCalificadora = kv.Value }).ToList()
        };

        _context.CalificadoraPeriodos.Add(entity);
        
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateCalificadoraPeriodosCommandResponse { Id = entity.Id };
    }
}

public class CreateCalificadoraPeriodosCommandResponse
{
    public int Id { get; set; }
}