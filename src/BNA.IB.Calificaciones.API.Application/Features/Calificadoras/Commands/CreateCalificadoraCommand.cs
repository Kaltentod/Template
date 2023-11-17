using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Domain.Entities;
using FluentValidation;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Commands;

public class CreateCalificadoraCommand : IRequest<CreateCalificadoraCommandResponse>
{
    public int Clave { get; set; }
    public string Nombre { get; set; }
}

public class CreateCalificadoraValidator : AbstractValidator<CreateCalificadoraCommand> 
{
    public CreateCalificadoraValidator()
    {
        RuleFor(x => x.Clave).NotNull().WithMessage("La clave no puede ser nula.");

        RuleFor(x => x.Nombre).Length(3, 50).WithMessage("El nombre debe tener entre 3 y 50 caracteres.");
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
            Nombre = request.Nombre
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