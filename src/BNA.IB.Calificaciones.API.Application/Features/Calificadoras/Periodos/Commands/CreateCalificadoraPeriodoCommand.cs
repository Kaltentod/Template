using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Commands;
using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.CalificadorasPeriodos.Commands;

public class CreateCalificadoraPeriodoCommand : IRequest<CreateCalificadoraPeriodosCommandResponse>
{
    public int CalificadoraId { get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly? FechaBaja { get; set; } = DateOnly.Parse("01-01-2900");
}

public class CreateCalificadoraPeriodoValidator : AbstractValidator<CreateCalificadoraPeriodoCommand>
{
    public CreateCalificadoraPeriodoValidator()
    {
        RuleFor(x => x.CalificadoraId).NotNull().GreaterThan(0);
        RuleFor(x => x.FechaAlta).NotNull();
        RuleFor(x => x.FechaBaja).GreaterThanOrEqualTo(x => x.FechaAlta)
            .When(x => x.FechaBaja.HasValue);
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

        if (calificadora.Periodos.Any(x => (x.FechaAlta <= request.FechaAlta.ToDateTime(TimeOnly.MinValue) || request.FechaAlta <= request.FechaBaja) && (x.FechaAlta <= ((DateOnly)request.FechaBaja).ToDateTime(TimeOnly.MinValue) || request.FechaBaja <= request.FechaBaja)))
        {
            var exceptions = new List<ValidationFailure>()
            {
                new ValidationFailure("FechaAlta","Esta entidad ya existe.")
            };
            throw new ValidationException(exceptions);
        }

        var entity = new CalificadoraPeriodo
        {
            FechaAlta = request.FechaAlta.ToDateTime(TimeOnly.MinValue),
            FechaBaja = ((DateOnly)request.FechaBaja).ToDateTime(TimeOnly.MinValue)
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