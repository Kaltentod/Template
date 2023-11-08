namespace ari_ib_calificaciones_api_domain;

/// <summary>
///     Unit Of Work. Sólo debe ser utilizado por los comandos.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Aplica los cambios a la base de datos.
    /// </summary>
    /// <returns>Número de filas afectadas.</returns>
    int Save(string? usuario = null);
}