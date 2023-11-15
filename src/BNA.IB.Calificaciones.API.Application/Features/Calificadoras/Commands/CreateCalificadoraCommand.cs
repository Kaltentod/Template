using BNA.IB.Calificaciones.API.Application.Common;
using BNA.IB.Calificaciones.API.Domain.Entities;
using MediatR;

namespace BNA.IB.Calificaciones.API.Application.Features.Calificadoras.Commands;

public class CreateCalificadoraCommand : IRequest<CreateCalificadoraCommandResponse>
{
    public int Clave { get; set; }
    public string Nombre { get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly FechaAltaBCRA { get; set; }
    public DateOnly FechaBaja { get; set; }
    public DateOnly FechaBajaBCRA { get; set; }
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
            FechaAlta = DateTime.Now
        };

        _context.Calificadoras.Add(entity);

        return new CreateCalificadoraCommandResponse { Id = await _context.SaveChangesAsync(cancellationToken) };
    }
}

public class CreateCalificadoraCommandResponse
{
    public int Id { get; set; }
}