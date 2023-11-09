using BNA.IB.Calificaciones.API.Application.DomainServices;
using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraRiesgos;
using BNA.IB.Calificaciones.API.Domain.Enums;

namespace BNA.IB.Calificaciones.API.Application.Feature.CalificadoraRiegosPeriodo.Commands;

public interface IRechazarCalificadoraRiesgosPeriodoCommand : ICommand<RechazarCalificadoraRiesgosPeriodoInput>
{
}

public interface IRechazarCalificadoraRiesgosPeriodoOutputPort : IOutputPortStandard<RechazarCalificadoraRiesgosPeriodoOutput>, IOutputPortError
{
}

public sealed class RechazarCalificadoraRiesgosPeriodoInput : Input
{
    public RechazarCalificadoraRiesgosPeriodoInput(string usuario, int calificadoraRiesgosId, int periodoId, string comentarios) : base(usuario)
    { 
        CalificadoraRiesgosId = calificadoraRiesgosId;
        PeriodoId = periodoId;
        Comentarios = comentarios;
    }

    public int CalificadoraRiesgosId { get; }
    public int PeriodoId { get; }
    public string Comentarios { get; }
}
public sealed class RechazarCalificadoraRiesgosPeriodoCommand : IRechazarCalificadoraRiesgosPeriodoCommand
{
    private readonly IRechazarCalificadoraRiesgosPeriodoOutputPort _outputPort;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICalificadorasRiesgosRepository _calificadorasRiesgosRepository;

    public RechazarCalificadoraRiesgosPeriodoCommand(IRechazarCalificadoraRiesgosPeriodoOutputPort rechazarCalificadoraRiesgosPeriodoOutputPort, IUnitOfWork unitOfWork,
        ICalificadorasRiesgosRepository calificadorasRiesgosRepository)
    {
        _outputPort = rechazarCalificadoraRiesgosPeriodoOutputPort;
        _unitOfWork = unitOfWork;
        _calificadorasRiesgosRepository = calificadorasRiesgosRepository;
    }
    public Task Execute(RechazarCalificadoraRiesgosPeriodoInput input)
    {
        if (input is null)
        {
            _outputPort.WriteError("Entrada nula.");
            return Task.CompletedTask;
        }
        try
        {
            if (input.Comentarios == "")
            {
                _outputPort.WriteError("Es obligatorio un Comentario para Rechazar");
                return Task.CompletedTask;
            }

            var periodo = _calificadorasRiesgosRepository.GetCalificadorasRiesgosPeriodoById(input.PeriodoId);

            if (periodo.Status != TipoEstado.SinVerificar && periodo.Status != TipoEstado.Rechazado)
            {
                _outputPort.WriteError($"No es posible rechazar Periodos que no sean {TipoEstado.SinVerificar.GetDescription()}.");
                return Task.CompletedTask;
            }

            if (periodo.CalificadoraRiesgosId != input.CalificadoraRiesgosId)
            {
                _outputPort.WriteError(
                    $"No Id de la Califadora de Riesgos no coinciden. IdPeriodo: {input.PeriodoId}, IdCalificadora: {input.CalificadoraRiesgosId}.");
                return Task.CompletedTask;
            }

            periodo.Descartar(input.Usuario, input.Comentarios);

            //clasificadoraRiesgos.AgregarEvento(new CalificadoraRiesgosRechazado(input.Usuario, clasificadoraRiesgos));

            _calificadorasRiesgosRepository.UpdateCalificadorasRiesgosPeriodo(periodo);
            _unitOfWork.Save();

            BuildOutput();


            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _outputPort.WriteError("No se pudo Rechazar.");
            return Task.FromException(ex);
        }
    }

    private void BuildOutput()
    {
        var output = new RechazarCalificadoraRiesgosPeriodoOutput();

        _outputPort.Standard(output);
    }
}

public sealed class RechazarCalificadoraRiesgosPeriodoOutput : Output
{
    public RechazarCalificadoraRiesgosPeriodoOutput()
    {
    }
}
