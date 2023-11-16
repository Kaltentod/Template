using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Commands;
using BNA.IB.Calificaciones.API.Domain;
using FluentValidation;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Periodos.Commands;

public class UpdateCalificadoraPeriodoCommand : IRequest
{
    public int Id { get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly? FechaBaja { get; set; } = DateOnly.FromDateTime(Const.FechaMax);
}

public class UpdateCalificadoraPeriodosValidator : AbstractValidator<UpdateCalificadoraPeriodoCommand>
{
    public UpdateCalificadoraPeriodosValidator()
    {
        RuleFor(x => x.FechaAlta).NotNull().WithMessage("La fecha de alta no puede ser nula.");

        RuleFor(x => x.FechaBaja)
            .GreaterThanOrEqualTo(x => x.FechaAlta)
            .When(x => x.FechaBaja.HasValue)
            .WithMessage("La fecha de baja debe ser mayor o igual a la fecha de alta.");
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
        entity.FechaBaja = request.FechaBaja!.Value.ToDateTime(TimeOnly.MinValue);
        

        await _context.SaveChangesAsync(cancellationToken);
    }
}