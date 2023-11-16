using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ValidationException = BNA.IB.Calificaciones.API.Application.Exceptions.ValidationException;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Commands;

public class CreateCalificadoraCommand : IRequest<CreateCalificadoraCommandResponse>
{
    public int Clave { get; set; }
    public string Nombre { get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly FechaAltaBCRA { get; set; }
    public DateOnly? FechaBaja { get; set; } = DateOnly.Parse("01-01-2900");
    public DateOnly? FechaBajaBCRA { get; set; } = DateOnly.Parse("01-01-2900");
}

public class CreateCalificadoraValidator : AbstractValidator<CreateCalificadoraCommand> 
{
    public CreateCalificadoraValidator()
    {
        RuleFor(x => x.Clave).NotNull();
        RuleFor(x => x.Nombre).Length(3, 50);
        RuleFor(x => x.FechaAlta).NotNull();
        RuleFor(x => x.FechaAltaBCRA).NotNull();
        RuleFor(x => x.FechaBaja).GreaterThanOrEqualTo(x => x.FechaAlta)
            .When(x => x.FechaBaja.HasValue);
        RuleFor(x => x.FechaBajaBCRA).GreaterThanOrEqualTo(x => x.FechaAltaBCRA)
            .When(x => x.FechaBajaBCRA.HasValue);
    }
}

public class
    CreateCalificadoraCommandHandler : IRequestHandler<CreateCalificadoraCommand, CreateCalificadoraCommandResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateCalificadoraCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreateCalificadoraCommandResponse> Handle(CreateCalificadoraCommand request,
        CancellationToken cancellationToken)
    {

        if (_context.Calificadoras.Any(x => x.Clave != request.Clave && x.Nombre == request.Nombre))
        {
            var exceptions = new List<ValidationFailure>()
            {
                new ValidationFailure("Nombre","Esta entidad ya existe.")
            };
            throw new ValidationException(exceptions);
        }

        var entity = new Calificadora
        {
            Clave = request.Clave,
            Nombre = request.Nombre,
            FechaAlta = request.FechaAlta.ToDateTime(TimeOnly.MinValue),
            FechaAltaBCRA = request.FechaAltaBCRA.ToDateTime(TimeOnly.MinValue),
            FechaBaja = request.FechaBaja == null ? DateTime.Parse("01-01-2900") : ((DateOnly)request.FechaBaja).ToDateTime(TimeOnly.MinValue),
            FechaBajaBCRA = request.FechaBajaBCRA == null ? DateTime.Parse("01-01-2900") : ((DateOnly)request.FechaBajaBCRA).ToDateTime(TimeOnly.MinValue)
        };

        _context.Calificadoras.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateCalificadoraCommandResponse { Id = entity.Id };
    }
}

public class CreateCalificadoraCommandResponse
{
    public int Id { get; set; }
}