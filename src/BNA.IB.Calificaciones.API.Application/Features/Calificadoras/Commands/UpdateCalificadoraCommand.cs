using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities;
using FluentValidation;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Commands;

public class UpdateCalificadoraCommand : IRequest
{
    public int Id { get; set; }
    public int Clave { get; set; }
    public string Nombre { get; set; }
}

public class UpdateCalificadoraValidator : AbstractValidator<UpdateCalificadoraCommand> 
{
    public UpdateCalificadoraValidator() 
    {
        RuleFor(x => x.Clave).NotNull().WithMessage("La clave no puede ser nula.");

        RuleFor(x => x.Nombre).Length(3, 50).WithMessage("El nombre debe tener entre 3 y 50 caracteres.");
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

        if (entity is null) throw new NotFoundException(nameof(Calificadora), request.Id);

        if (_context.Calificadoras.Any(x => x.Clave != request.Clave && x.Nombre == request.Nombre))
            throw new ForbiddenException("Esta entidad ya existe.");

        entity.Nombre = request.Nombre;

        await _context.SaveChangesAsync(cancellationToken);
    }
}