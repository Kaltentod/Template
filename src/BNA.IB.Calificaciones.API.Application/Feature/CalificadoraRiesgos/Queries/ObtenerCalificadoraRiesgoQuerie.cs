using BNA.IB.Calificaciones.API.Domain.Entities.Adjuntos;
using BNA.IB.Calificaciones.API.Domain.Entities.CalificacionesBCRA;
using BNA.IB.Calificaciones.API.Domain.Entities.CalificacionesBCRACodigo;
using BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraRiesgos;

namespace BNA.IB.Calificaciones.API.Application.Feature.CalificadoraRiesgos.Queries;

public interface IObtenerCalificadoraRiesgosQuery : IQuery<ObtenerCalificadoraRiesgosInput>
{
}

public interface IObtenerCalificadoraRiesgosOutputPort : IOutputPortStandard<ObtenerCalificadoraRiesgosOutput>, IOutputPortError
{
}

public sealed class ObtenerCalificadoraRiesgosInput : Input
{
    public ObtenerCalificadoraRiesgosInput(string usuario, int id) : base(usuario)
    {
        Id = id;
    }

    public int Id { get; }
}
public sealed class ObtenerCalificadoraRiesgosQuery : IObtenerCalificadoraRiesgosQuery
{
    private readonly IObtenerCalificadoraRiesgosOutputPort _outputPort;
    private readonly ICalificadorasRiesgosRepository _calificadorasRiesgosRepository;
    private readonly IAdjuntosRepository _adjuntosRepository;
    private readonly ICalificacionesBCRARepository _calificacionesBCRARepository;

    public ObtenerCalificadoraRiesgosQuery(IObtenerCalificadoraRiesgosOutputPort outputPort, ICalificadorasRiesgosRepository calificadorasRiesgosRepository, IAdjuntosRepository adjuntosRepository,
        ICalificacionesBCRARepository calificacionesBCRARepository)
    {
        _outputPort = outputPort;
        _calificadorasRiesgosRepository = calificadorasRiesgosRepository;
        _adjuntosRepository = adjuntosRepository;
        _calificacionesBCRARepository = calificacionesBCRARepository;
    }

    public async Task Execute(ObtenerCalificadoraRiesgosInput input)
    {
        if (input is null)
        {
            _outputPort.WriteError("Entrada nula.");
            await Task.CompletedTask;
        }

        var calificadoraRiesgos = _calificadorasRiesgosRepository.GetCalificadorasRiesgosById(input.Id);

        //unficar metodos
        var adjuntos = _adjuntosRepository.GetAdjuntosVinculadosByPropietarioId(input.Id, "CalificadorasRiesgos");

        var adjuntosNombres = _adjuntosRepository.GetFileNames(adjuntos);
        //fin unficar metodos

        var calificaciones = _calificacionesBCRARepository.GetCalificacionesBCRA();

        BuildOutput(calificadoraRiesgos, calificaciones, adjuntosNombres);

        await Task.CompletedTask;
    }

    private void BuildOutput(CalificadorasRiesgos calificadoraRiesgos, List<CalificacionesBCRACodigo> calificacionesBCRA, List<Adjunto> adjuntos)
    {
        var output = new ObtenerCalificadoraRiesgosOutput(calificadoraRiesgos, calificacionesBCRA, adjuntos);

        _outputPort.Standard(output);
    }
}

public sealed class ObtenerCalificadoraRiesgosOutput : Output
{
    public ObtenerCalificadoraRiesgosOutput(CalificadorasRiesgos calificadoraRiesgos, List<CalificacionesBCRACodigo> calificacionesBCRA, List<Adjunto> adjuntos)
    {
        CalificadoraRiesgos = calificadoraRiesgos;
        CalificacionesBCRA = calificacionesBCRA;
        Adjuntos = adjuntos;
    }

    public CalificadorasRiesgos CalificadoraRiesgos { get; }
    public List<CalificacionesBCRACodigo> CalificacionesBCRA { get; }
    public List<Adjunto> Adjuntos { get; }
}
