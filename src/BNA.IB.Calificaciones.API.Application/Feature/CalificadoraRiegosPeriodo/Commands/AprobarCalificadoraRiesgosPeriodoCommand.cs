using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities;
using BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraRiesgos;

namespace BNA.IB.Calificaciones.API.Application.Feature.CalificadoraRiegosPeriodo.Commands;

public interface IAprobarCalificadoraRiesgosPeriodoCommand : ICommand<AprobarCalificadoraRiesgosPeriodoInput>
{
}

public interface IAprobarCalificadoraRiesgosPeriodoOutputPort : IOutputPortStandard<AprobarCalificadoraRiesgosPeriodoOutput>, IOutputPortError
{
}

public sealed class AprobarCalificadoraRiesgosPeriodoInput : Input
{
    public AprobarCalificadoraRiesgosPeriodoInput(string usuario, int idCalificadora, int idPeriodo) : base(usuario)
    {
        IdCalificadora = idCalificadora;
        IdPeriodo = idPeriodo;
    }
    public int IdCalificadora { get; }
    public int IdPeriodo { get; }
}
public sealed class AprobarCalificadoraRiesgosPeriodoCommand : IAprobarCalificadoraRiesgosPeriodoCommand
{
    private readonly IAprobarCalificadoraRiesgosPeriodoOutputPort _outputPort;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICalificadorasRiesgosRepository _calificadorasRiesgosRepository;

    public AprobarCalificadoraRiesgosPeriodoCommand(IAprobarCalificadoraRiesgosPeriodoOutputPort AprobarCalificadoraRiesgosPeriodoOutputPort, IUnitOfWork unitOfWork,
        ICalificadorasRiesgosRepository calificadorasRiesgosRepository)
    {
        _outputPort = AprobarCalificadoraRiesgosPeriodoOutputPort;
        _unitOfWork = unitOfWork;
        _calificadorasRiesgosRepository = calificadorasRiesgosRepository;
    }
    public Task Execute(AprobarCalificadoraRiesgosPeriodoInput input)
    {
        if (input is null)
        {
            _outputPort.WriteError("Entrada nula.");
            return Task.CompletedTask;
        }
        try
        {
            var calificadoraRiesgos =
                _calificadorasRiesgosRepository.GetCalificadorasRiesgosById(input.IdCalificadora);


            if (calificadoraRiesgos is null)
            {
                _outputPort.WriteError(
                    $"No Existe la CalificadoraRiesos. Id: {input.IdCalificadora}.");
                return Task.CompletedTask;
            }

            var periodo = calificadoraRiesgos.CalificadorasRiesgosPeriodos.SingleOrDefault(x => x.Id ==  input.IdPeriodo);
            if (periodo is null)
            {
                _outputPort.WriteError(
                    $"No Existe un Periodo. Id: {input.IdPeriodo}.");
                return Task.CompletedTask;
            }
            periodo.Confirmar(input.Usuario, "");

            var validacion = periodo.VerificarCalificaciones();
            if (!validacion.EsValido)
            {
                _outputPort.WriteError(validacion.ErrorMensaje);
                return Task.CompletedTask;
            }

            _calificadorasRiesgosRepository.UpdateCalificadorasRiesgosPeriodo(periodo);

            //evento
            _unitOfWork.Save();

            BuildOutput(calificadoraRiesgos);

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _outputPort.WriteError("No se pudo Aprobar.");
            return Task.FromException(ex);
        }
    }

    private void BuildOutput(CalificadorasRiesgos calificadoraRiesgos)
    {
        var output = new AprobarCalificadoraRiesgosPeriodoOutput(calificadoraRiesgos.Id);

        _outputPort.Standard(output);
    }
}

public sealed class AprobarCalificadoraRiesgosPeriodoOutput : Output
{
    public AprobarCalificadoraRiesgosPeriodoOutput(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
