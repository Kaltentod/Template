using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities.Adjuntos;
using BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraRiesgos;
using BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraRiesgosPeriodo;

namespace BNA.IB.Calificaciones.API.Application.Feature.CalificadoraRiesgos.Commands;

public interface IAgregarCalificadoraRiesgosCommand : ICommand<AgregarCalificadoraRiesgosInput>
{
}

public interface IAgregarCalificadoraRiesgosOutputPort : IOutputPortStandard<AgregarCalificadoraRiesgosOutput>, IOutputPortError
{
}

public sealed class AgregarCalificadoraRiesgosInput : Input
{
    public AgregarCalificadoraRiesgosInput(string usuario, string nombre, List<CalificadoraRiesgosPeriodos> calificadorasRiesgosPeriodo, List<AdjuntoVinculado> adjuntosVinculados) : base(usuario)
    {
        Nombre = nombre;
        CalificadorasRiesgosPeriodo = calificadorasRiesgosPeriodo;
        AdjuntosVinculados = adjuntosVinculados;
    }

    public string Nombre { get; }
    public List<CalificadoraRiesgosPeriodos> CalificadorasRiesgosPeriodo { get; }
    public List<AdjuntoVinculado> AdjuntosVinculados { get; }
}
public sealed class AgregarCalificadoraRiesgosCommand : IAgregarCalificadoraRiesgosCommand
{
    private readonly IAgregarCalificadoraRiesgosOutputPort _outputPort;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICalificadorasRiesgosRepository _calificadorasRiesgosRepository;
    private readonly IAdjuntosRepository _adjuntosRepository;

    public AgregarCalificadoraRiesgosCommand(IAgregarCalificadoraRiesgosOutputPort agregarCalificadoraRiesgosOutputPort, IUnitOfWork unitOfWork,
        ICalificadorasRiesgosRepository calificadorasRiesgosRepository, IAdjuntosRepository adjuntosRepository)
    {
        _outputPort = agregarCalificadoraRiesgosOutputPort;
        _unitOfWork = unitOfWork;
        _calificadorasRiesgosRepository = calificadorasRiesgosRepository;
        _adjuntosRepository = adjuntosRepository;
    }
    public Task Execute(AgregarCalificadoraRiesgosInput input)
    {
        if (input is null)
        {
            _outputPort.WriteError("Entrada nula.");
            return Task.CompletedTask;
        }
        try
        {

            var existeClaficadoras = _calificadorasRiesgosRepository.GetClasificadoraRiesgosByEstados();
            if (existeClaficadoras.FirstOrDefault(x => x.Nombre == input.Nombre) != null)
            {
                _outputPort.WriteError("Ya existe una calificadora con ese Nombre.");
                return Task.CompletedTask;
            }

            var clasificadoraRiesgos = CalificadorasRiesgos.Crear(input.Usuario, input.Nombre, input.CalificadorasRiesgosPeriodo);

            var validacionPeriodo = clasificadoraRiesgos.VerificarPeriodos();

            if (!validacionPeriodo.EsValido)
            {
                _outputPort.WriteError(validacionPeriodo.ErrorMensaje);
                return Task.CompletedTask;
            }

            _calificadorasRiesgosRepository.AddCalificadorasRiesgos(clasificadoraRiesgos);


            _adjuntosRepository.AddAdjuntoVinculado(input.AdjuntosVinculados);

            //clasificadoraRiesgos.AgregarEvento(new CalificadoraRiesgosAgregado(input.Usuario, clasificadoraRiesgos));


            BuildOutput(clasificadoraRiesgos);

            _unitOfWork.Save();

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _outputPort.WriteError("No se pudo Agregar.");
            return Task.FromException(ex);
        }
    }

    private void BuildOutput(CalificadorasRiesgos calificadoraRiesgos)
    {
        var output = new AgregarCalificadoraRiesgosOutput(calificadoraRiesgos.Id);

        _outputPort.Standard(output);
    }
}

public sealed class AgregarCalificadoraRiesgosOutput : Output
{
    public AgregarCalificadoraRiesgosOutput(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
