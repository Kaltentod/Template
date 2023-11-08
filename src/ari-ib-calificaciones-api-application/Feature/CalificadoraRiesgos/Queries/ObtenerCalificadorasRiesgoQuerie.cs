using ari_ib_calificaciones_api_domain.Entities.CalificadoraRiesgos;

namespace ari_ib_calificaciones_api_application.Feature.CalificadoraRiesgos.Queries;

public interface IObtenerCalificadorasRiesgosQuery : IQuery<ObtenerCalificadorasRiesgosInput>
{
}

public interface IObtenerCalificadorasRiesgosOutputPort : IOutputPortStandard<ObtenerCalificadorasRiesgosOutput>, IOutputPortError
{
}

public sealed class ObtenerCalificadorasRiesgosInput : Input
{
    public ObtenerCalificadorasRiesgosInput(string usuario) : base(usuario)
    {
    }

}
public sealed class ObtenerCalificadorasRiesgosQuery : IObtenerCalificadorasRiesgosQuery
{
    private readonly IObtenerCalificadorasRiesgosOutputPort _outputPort;
    private readonly ICalificadorasRiesgosRepository _calificadorasRiesgosRepository;

    public ObtenerCalificadorasRiesgosQuery(IObtenerCalificadorasRiesgosOutputPort outputPort,
        ICalificadorasRiesgosRepository calificadorasRiesgosRepository)
    {
        _outputPort = outputPort;
        _calificadorasRiesgosRepository = calificadorasRiesgosRepository;
    }

    public async Task Execute(ObtenerCalificadorasRiesgosInput input)
    {
        if (input is null)
        {
            _outputPort.WriteError("Entrada nula.");
            await Task.CompletedTask;
        }

        var calificadorasRiesgos = _calificadorasRiesgosRepository.GetClasificadoraRiesgosByEstados();

        BuildOutput(calificadorasRiesgos);

        await Task.CompletedTask;
    }

    private void BuildOutput(IQueryable<CalificadorasRiesgos> calificadorasRiesgos)
    {
        var output = new ObtenerCalificadorasRiesgosOutput(calificadorasRiesgos);

        _outputPort.Standard(output);
    }
}

public sealed class ObtenerCalificadorasRiesgosOutput : Output
{
    public ObtenerCalificadorasRiesgosOutput(IQueryable<CalificadorasRiesgos> calificadorasRiesgos)
    {
        CalificadorasRiesgos = calificadorasRiesgos;
    }

    public IQueryable<CalificadorasRiesgos> CalificadorasRiesgos { get; }
}
