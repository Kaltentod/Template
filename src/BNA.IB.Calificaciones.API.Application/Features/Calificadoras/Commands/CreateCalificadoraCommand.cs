using BNA.IB.Calificaciones.API.Application.Common;
using MediatR;
using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities;
using BNA.IB.Calificaciones.API.Infrastructure.SQLServer;

namespace BNA.IB.Calificaciones.API.Application.Feature.Calificadoras.Commands;

public class CreateCalificadoraCommand : IRequest
{
    public int Clave { get; set; }
    public string Nombre { get; set; }
    public DateOnly FechaAlta { get; set; }
    public DateOnly FechaAltaBCRA { get; set; }
    public DateOnly FechaBaja { get; set; }
    public DateOnly FechaBajaBCRA { get; set; }
}

public class CreateCalificadoraCommandHandler : IRequestHandler<CreateCalificadoraCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateCalificadoraCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCalificadoraCommand request, CancellationToken cancellationToken)
    {
        var calificadora = new Calificadora
        {
            Clave = request.Clave,
            Nombre = request.Nombre
        };

        _context.Calificadoras.Add(calificadora);

        await _context.SaveChangesAsync(cancellationToken);

        return calificadora.Id;
    }
}