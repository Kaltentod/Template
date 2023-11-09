namespace BNA.IB.Calificaciones.API.Application.Feature;

/// <summary>
///     Command.
/// </summary>
/// <typeparam name="TInput">Any Input Message.</typeparam>
public interface ICommand<in TInput>
{
    /// <summary>
    ///     Executes the Command.
    /// </summary>
    /// <param name="input">Input Message.</param>
    /// <returns>Task.</returns>
    Task Execute(TInput input);
}