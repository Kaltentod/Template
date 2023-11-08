using ari_ib_calificaciones_api_domain.Enums;

namespace ari_ib_calificaciones_api_domain.Entities.ClientesTitulosCalificados;

public interface IClienteTituloCalificadoRepository
{
    void AddClienteTituloCalificado(ClienteTituloCalificado clienteTituloCalificado);
    void UpdateClienteTituloCalificado(ClienteTituloCalificado clienteTituloCalificado);
    void RemoveClienteTituloCalificado(ClienteTituloCalificado clienteTituloCalificado);
    IQueryable<ClienteTituloCalificado> GetClienteTituloCalificadoByKeys(TipoCalificado tipoCalificado, string identificacionClienteTitulo, DateTime fechaDesde, DateTime? fechaHasta = null);
    ClienteTituloCalificado GetClienteTituloCalificadoById(int id);
    Task<IQueryable<ClienteTituloCalificado>> ListClienteTituloCalificado(TipoCalificado? tipoCalificado = null);
    ClienteTituloCalificado GetLastClienteTituloCalificadoVigente(int clave, int version);
    IQueryable<ClienteTituloCalificado> GetClienteTituloCalificadoByEstados(int? clave = null, bool vigente = true,
            bool borrador = true, bool rechazado = true, bool obsoleto = true);

    IQueryable<ClienteTituloCalificado> GetClienteTituloCalificadoByIdentificadorAndCalificadora(string identificador, int calificadoraId);
}
