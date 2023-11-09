using BNA.IB.Calificaciones.API.Domain;
using BNA.IB.Calificaciones.API.Domain.Entities.Adjuntos;
using BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraRiesgos;
using BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraRiesgosPeriodo;
using BNA.IB.Calificaciones.API.Domain.Enums;

namespace BNA.IB.Calificaciones.API.Application.Feature.CalificadoraRiegosPeriodo.Commands;

public interface IEditarCalificadoraRiesgosPeriodoCommand : ICommand<EditarCalificadoraRiesgosPeriodoInput>
{
}

public interface IEditarCalificadoraRiesgosPeriodoOutputPort : IOutputPortStandard<EditarCalificadoraRiesgosPeriodoOutput>, IOutputPortError
{
}

public sealed class EditarCalificadoraRiesgosPeriodoInput : Input
{
    public EditarCalificadoraRiesgosPeriodoInput(string usuario, CalificadoraRiesgosPeriodos calificadoraRiegosPeriodo) : base(usuario)
    {
        CalificadoraRiegosPeriodo = calificadoraRiegosPeriodo;
    }

    public CalificadoraRiesgosPeriodos CalificadoraRiegosPeriodo { get; }
}
public sealed class EditarCalificadoraRiesgosPeriodoCommand : IEditarCalificadoraRiesgosPeriodoCommand
{
    private readonly IEditarCalificadoraRiesgosPeriodoOutputPort _outputPort;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICalificadorasRiesgosRepository _calificadorasRiesgosRepository;

    public EditarCalificadoraRiesgosPeriodoCommand(IEditarCalificadoraRiesgosPeriodoOutputPort EditarCalificadoraRiesgosPeriodoOutputPort, IUnitOfWork unitOfWork,
        ICalificadorasRiesgosRepository calificadorasRiesgosRepository)
    {
        _outputPort = EditarCalificadoraRiesgosPeriodoOutputPort;
        _unitOfWork = unitOfWork;
        _calificadorasRiesgosRepository = calificadorasRiesgosRepository;
    }
    public Task Execute(EditarCalificadoraRiesgosPeriodoInput input)
    {
        if (input is null)
        {
            _outputPort.WriteError("Entrada nula.");
            return Task.CompletedTask;
        }
        try
        {
            if (input.CalificadoraRiegosPeriodo.Status == TipoEstado.SinVerificar || input.CalificadoraRiegosPeriodo.Status == TipoEstado.Rechazado)
            {
                _outputPort.WriteError(
                    $"Solo se pueden Editar Periodos Rechazados o Sin Verificar.");
                return Task.CompletedTask;
            }

            var calificadoraRiesgos = _calificadorasRiesgosRepository.GetCalificadorasRiesgosById(input.CalificadoraRiegosPeriodo.CalificadoraRiesgosId);

            if (calificadoraRiesgos is null)
            {
                _outputPort.WriteError(
                    $"No Existe la Calificadora. Id: {input.CalificadoraRiegosPeriodo.CalificadoraRiesgosId}.");
                return Task.CompletedTask;
            }

            var periodoCalificadora = calificadoraRiesgos.CalificadorasRiesgosPeriodos.SingleOrDefault(x => x.Id == input.CalificadoraRiegosPeriodo.Id);

            if (periodoCalificadora == null)
            {
                _outputPort.WriteError(
                    $"No Id de la Califadora de Riesgos no coinciden. IdPeriodo: {input.CalificadoraRiegosPeriodo.Id}, IdCalificadora: {input.CalificadoraRiegosPeriodo.CalificadoraRiesgosId}.");
                return Task.CompletedTask;
            }

            periodoCalificadora = input.CalificadoraRiegosPeriodo;

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

            _calificadorasRiesgosRepository.UpdateCalificadorasRiesgosPeriodo(input.CalificadoraRiegosPeriodo);

            _unitOfWork.Save();

            BuildOutput(input.CalificadoraRiegosPeriodo);


            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _outputPort.WriteError("No se pudo Editar.");
            return Task.FromException(ex);
        }
    }

    private void BuildOutput(CalificadoraRiesgosPeriodos calificadoraRiesgosPeriodo)
    {
        var output = new EditarCalificadoraRiesgosPeriodoOutput(calificadoraRiesgosPeriodo.Id);

        _outputPort.Standard(output);
    }
}

public sealed class EditarCalificadoraRiesgosPeriodoOutput : Output
{
    public EditarCalificadoraRiesgosPeriodoOutput(int id)
    {
        Id = id;
    }

    public int Id { get; }
}
