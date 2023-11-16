using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Application.Exceptions;
using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.CalificadorasPeriodos.Commands;

public class CreateCalificadoraPeriodoEquivalenciaCommand : IRequest<CreateCalificadoraPeriodoEquivalenciasCommandResponse>
{
    public int CalificadoraId { get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly? FechaBaja { get; set; } = DateOnly.Parse("01-01-2900");
}



public class
    CreateCalificadoraPeriodoEquivalenciasCommandHandler : IRequestHandler<CreateCalificadoraPeriodoEquivalenciaCommand, CreateCalificadoraPeriodoEquivalenciasCommandResponse>
{
    private readonly IApplicationDbContext _context;

    public CreateCalificadoraPeriodoEquivalenciasCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<CreateCalificadoraPeriodoEquivalenciasCommandResponse> Handle(CreateCalificadoraPeriodoEquivalenciaCommand request,
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

        var entity = new CalificadoraPeriodoEquivalencia
        {
            FechaAlta = request.FechaAlta.ToDateTime(TimeOnly.MinValue),
            FechaBaja = ((DateOnly)request.FechaBaja).ToDateTime(TimeOnly.MinValue)
        };

        _context.CalificadoraPeriodoEquivalencias.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateCalificadoraPeriodoEquivalenciasCommandResponse { Id = entity.Id };
    }
}

public class CreateCalificadoraPeriodoEquivalenciasCommandResponse
{
    public int Id { get; set; }
}