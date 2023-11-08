namespace ari_ib_calificaciones_api_application.Feature;

/// <summary>
///     Query.
/// </summary>
/// <typeparam name="TQueryInput">Any Input Message.</typeparam>
public interface IQuery<in TQueryInput>
{
    /// <summary>
    ///     Executes the Query.
    /// </summary>
    /// <param name="input">Input Message.</param>
    /// <returns>Task.</returns>
    Task Execute(TQueryInput input);
}