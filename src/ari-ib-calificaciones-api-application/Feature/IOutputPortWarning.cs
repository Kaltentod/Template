namespace ari_ib_calificaciones_api_application.Feature;

/// <summary>
///     Warning Output Port.
/// </summary>
public interface IOutputPortWarning
{
    /// <summary>
    ///     Informs a warning happened.
    /// </summary>
    /// <param name="message">Text description.</param>
    void WriteWarning(string message);
}