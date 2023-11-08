using ari_ib_calificaciones_api_application.DomainServices;
using ari_ib_calificaciones_api_domain;
using ari_ib_calificaciones_api_domain.Entities.CalificadoraRiesgos;
using ari_ib_calificaciones_api_domain.Enums;

namespace ari_ib_calificaciones_api_application.Feature.CalificadoraRiegosPeriodo.Commands;

public interface IEliminarCalificadoraRiesgosPeriodoCommand : ICommand<EliminarCalificadoraRiesgosPeriodoInput>
{
}

public interface IEliminarCalificadoraRiesgosPeriodoOutputPort : IOutputPortStandard<EliminarCalificadoraRiesgosPeriodoOutput>, IOutputPortError
{
}

public sealed class EliminarCalificadoraRiesgosPeriodoInput : Input
{
    public EliminarCalificadoraRiesgosPeriodoInput(string usuario, int calificadoraRiesgosId, int periodoId) : base(usuario)
    {
        CalificadoraRiesgosId = calificadoraRiesgosId;
        PeriodoId = periodoId;
    }
    public int CalificadoraRiesgosId { get; }
    public int PeriodoId { get; }
}
public sealed class EliminarCalificadoraRiesgosPeriodoCommand : IEliminarCalificadoraRiesgosPeriodoCommand
{
    private readonly IEliminarCalificadoraRiesgosPeriodoOutputPort _outputPort;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICalificadorasRiesgosRepository _calificadorasRiesgosRepository;

    public EliminarCalificadoraRiesgosPeriodoCommand(IEliminarCalificadoraRiesgosPeriodoOutputPort eliminarCalificadoraRiesgosPeriodoOutputPort, IUnitOfWork unitOfWork,
        ICalificadorasRiesgosRepository calificadorasRiesgosRepository)
    {
        _outputPort = eliminarCalificadoraRiesgosPeriodoOutputPort;
        _unitOfWork = unitOfWork;
        _calificadorasRiesgosRepository = calificadorasRiesgosRepository;
    }
    public Task Execute(EliminarCalificadoraRiesgosPeriodoInput input)
    {
        if (input is null)
        {
            _outputPort.WriteError("Entrada nula.");
            return Task.CompletedTask;
        }
        try
        {
            var periodo = _calificadorasRiesgosRepository.GetCalificadorasRiesgosPeriodoById(input.PeriodoId);

            if (periodo == null)
            {
                _outputPort.WriteError($"No Existe este periodo. Id: {input.PeriodoId}");
                return Task.CompletedTask;
            }

            if (periodo.Status != TipoEstado.SinVerificar && periodo.Status != TipoEstado.Rechazado)
            {
                _outputPort.WriteError($"No es posible eliminar Periodos que no sean {TipoEstado.SinVerificar.GetDescription()} o {TipoEstado.Rechazado.GetDescription()}.");
                return Task.CompletedTask;
            }

            if (periodo.CalificadoraRiesgosId != input.CalificadoraRiesgosId)
            {
                _outputPort.WriteError(
                    $"No Id de la Califadora de Riesgos no coinciden. IdPeriodo: {input.PeriodoId}, IdCalificadora: {input.CalificadoraRiesgosId}.");
                return Task.CompletedTask;
            }

            _calificadorasRiesgosRepository.RemoveCalificadorasRiesgosPeriodo(periodo);

            //clasificadoraRiesgos.AgregarEvento(new CalificadoraRiesgosEliminado(input.Usuario, clasificadoraRiesgos));

            _unitOfWork.Save();

            BuildOutput();


            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _outputPort.WriteError("No se pudo Eliminar.");
            return Task.FromException(ex);
        }
    }

    private void BuildOutput()
    {
        var output = new EliminarCalificadoraRiesgosPeriodoOutput();

        _outputPort.Standard(output);
    }
}

public sealed class EliminarCalificadoraRiesgosPeriodoOutput : Output
{
    public EliminarCalificadoraRiesgosPeriodoOutput()
    {
    }
}
