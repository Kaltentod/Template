namespace ari_ib_calificaciones_api_application.Feature;

/// <summary>
///     Standard Output Port.
/// </summary>
/// <typeparam name="TOutput">Any IOutput.</typeparam>
public interface IOutputPortStandard<in TOutput>
{
    /// <summary>
    ///     Writes to the Standard Output.
    /// </summary>
    /// <param name="output">The Output Port Message.</param>
    void Standard(TOutput output);
}