using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraRiesgos;
using BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraRiesgosPeriodo;

namespace BNA.IB.Calificaciones.API.Application.Feature.CalificadoraRiegosPeriodo.Commands;

public interface IAgregarCalificadoraRiesgosPeriodoCommand : ICommand<AgregarCalificadoraRiesgosPeriodoInput>
{
}

public interface IAgregarCalificadoraRiesgosPeriodoOutputPort : IOutputPortStandard<AgregarCalificadoraRiesgosPeriodoOutput>, IOutputPortError
{
}

public sealed class AgregarCalificadoraRiesgosPeriodoInput : Input
{
    public AgregarCalificadoraRiesgosPeriodoInput(string usuario, CalificadoraRiesgosPeriodos calificadoraRiegosPeriodo) : base(usuario)
    {
        CalificadoraRiegosPeriodo = calificadoraRiegosPeriodo;
    }

    public CalificadoraRiesgosPeriodos CalificadoraRiegosPeriodo { get; }
}
public sealed class AgregarCalificadoraRiesgosPeriodoCommand : IAgregarCalificadoraRiesgosPeriodoCommand
{
    private readonly IAgregarCalificadoraRiesgosPeriodoOutputPort _outputPort;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICalificadorasRiesgosRepository _calificadorasRiesgosRepository;

    public AgregarCalificadoraRiesgosPeriodoCommand(IAgregarCalificadoraRiesgosPeriodoOutputPort AgregarCalificadoraRiesgosPeriodoOutputPort, IUnitOfWork unitOfWork,
        ICalificadorasRiesgosRepository calificadorasRiesgosRepository)
    {
        _outputPort = AgregarCalificadoraRiesgosPeriodoOutputPort;
        _unitOfWork = unitOfWork;
        _calificadorasRiesgosRepository = calificadorasRiesgosRepository;
    }
    public Task Execute(AgregarCalificadoraRiesgosPeriodoInput input)
    {
        if (input is null)
        {
            _outputPort.WriteError("Entrada nula.");
            return Task.CompletedTask;
        }
        try
        {
            var calificadoraRiesgos =  _calificadorasRiesgosRepository.GetCalificadorasRiesgosById(input.CalificadoraRiegosPeriodo.CalificadoraRiesgosId);

            if (calificadoraRiesgos is null)
            {
                _outputPort.WriteError(
                    $"No Existe la CalificadoraRiesos. Id: {input.CalificadoraRiegosPeriodo.CalificadoraRiesgosId}.");
                return Task.CompletedTask;
            }

            calificadoraRiesgos.CalificadorasRiesgosPeriodos.Add(input.CalificadoraRiegosPeriodo);

            var validacion = calificadoraRiesgos.VerificarPeriodos();
            if (!validacion.EsValido)
            {
                _outputPort.WriteError(validacion.ErrorMensaje);
                return Task.CompletedTask;
            }

            validacion = input.CalificadoraRiegosPeriodo.VerificarCalificaciones();
            if (!validacion.EsValido)
            {
                _outputPort.WriteError(validacion.ErrorMensaje);
                return Task.CompletedTask;
            }

            _calificadorasRiesgosRepository.AddCalificadorasRiesgosPeriodo(input.CalificadoraRiegosPeriodo);

            //evento
            _unitOfWork.Save();

            BuildOutput(calificadoraRiesgos);

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
        var output = new AgregarCalificadoraRiesgosPeriodoOutput(calificadoraRiesgos.Id);

        _outputPort.Standard(output);
    }
}

public sealed class AgregarCalificadoraRiesgosPeriodoOutput : Output
{
    public AgregarCalificadoraRiesgosPeriodoOutput(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
