using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities;
using FluentValidation;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Commands;

public class CreateCalificadoraCommand : IRequest<CreateCalificadoraCommandResponse>
{
    public int Clave { get; set; }
    public string Nombre { get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly FechaAltaBCRA { get; set; }
    public DateOnly? FechaBaja { get; set; } = DateOnly.FromDateTime(Const.FechaMax);
    public DateOnly? FechaBajaBCRA { get; set; } = DateOnly.FromDateTime(Const.FechaMax);
}

public class CreateCalificadoraValidator : AbstractValidator<CreateCalificadoraCommand> 
{
    public CreateCalificadoraValidator()
    {
        RuleFor(x => x.Clave).NotNull().WithMessage("La clave no puede ser nula.");

        RuleFor(x => x.Nombre).Length(3, 50).WithMessage("El nombre debe tener entre 3 y 50 caracteres.");

        RuleFor(x => x.FechaAlta).NotNull().WithMessage("La fecha de alta no puede ser nula.");

        RuleFor(x => x.FechaAltaBCRA).NotNull().WithMessage("La fecha de alta en BCRA no puede ser nula.");

        RuleFor(x => x.FechaBaja)
            .GreaterThanOrEqualTo(x => x.FechaAlta)
            .When(x => x.FechaBaja.HasValue)
            .WithMessage("La fecha de baja debe ser mayor o igual a la fecha de alta.");

        RuleFor(x => x.FechaBajaBCRA)
            .GreaterThanOrEqualTo(x => x.FechaAltaBCRA)
            .When(x => x.FechaBajaBCRA.HasValue)
            .WithMessage("La fecha de baja en BCRA debe ser mayor o igual a la fecha de alta en BCRA.");
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
            throw new ForbiddenException("Esta entidad ya existe.");

        var entity = new Calificadora
        {
            Clave = request.Clave,
            Nombre = request.Nombre,
            FechaAlta = request.FechaAlta.ToDateTime(TimeOnly.MinValue),
            FechaAltaBCRA = request.FechaAltaBCRA.ToDateTime(TimeOnly.MinValue),
            FechaBaja = request.FechaBaja!.Value.ToDateTime(TimeOnly.MinValue),
            FechaBajaBCRA = request.FechaBajaBCRA!.Value.ToDateTime(TimeOnly.MinValue)
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