using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Domain.Entities;
using FluentValidation;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Commands;

public class UpdateCalificadoraCommand : IRequest
{
    public int Id { get; set; }
    public int Clave { get; set; }
    public string Nombre { get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly FechaAltaBCRA { get; set; }
    public DateOnly? FechaBaja { get; set; } = DateOnly.Parse("01-01-2900");
    public DateOnly? FechaBajaBCRA { get; set; } = DateOnly.Parse("01-01-2900");
}

public class UpdateCalificadoraValidator : AbstractValidator<UpdateCalificadoraCommand> 
{
    public UpdateCalificadoraValidator() 
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

public class UpdateCalificadoraCommandHandler : IRequestHandler<UpdateCalificadoraCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCalificadoraCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Handle(UpdateCalificadoraCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Calificadoras.FindAsync(request.Id);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Calificadora), request.Id);
        }

        entity.Clave = request.Clave;
        entity.Nombre = request.Nombre;

        await _context.SaveChangesAsync(cancellationToken);
    }
}