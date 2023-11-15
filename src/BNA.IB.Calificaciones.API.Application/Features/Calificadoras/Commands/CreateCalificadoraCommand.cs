using BNA.IB.Calificaciones.API.Application.Common;
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
}

public class CreateCalificadoraValidator : AbstractValidator<CreateCalificadoraCommand> 
{
    public CreateCalificadoraValidator() 
    {
        RuleFor(x => x.Clave).NotNull();
        RuleFor(x => x.Nombre).Length(3, 50);
    }
    public DateTime FechaAlta { get; set; }
    public DateTime FechaBaja { get; set; }
    public DateTime FechaAltaBCRA { get; set; }
    public DateTime FechaBajaBCRA { get; set; }
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
        var entity = new Calificadora
        {
            Clave = request.Clave,
            Nombre = request.Nombre,
            FechaAlta = request.FechaAlta.ToDateTime(TimeOnly.MinValue),
            FechaAltaBCRA = request.FechaAltaBCRA.ToDateTime(TimeOnly.MinValue)
            FechaAlta = request.FechaAlta,
            FechaAltaBCRA = request.FechaAltaBCRA,
            FechaBaja = request.FechaBaja,
            FechaBajaBCRA = request.FechaBajaBCRA
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