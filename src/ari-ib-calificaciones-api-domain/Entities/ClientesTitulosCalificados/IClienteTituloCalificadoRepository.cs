using ari_ib_calificaciones_api_domain.Entities.Adjuntos;
using ari_ib_calificaciones_api_domain.Enums;
using ari_ib_calificaciones_api_domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ari_ib_calificaciones_api_domain.Entities.ClientesTitulosCalificados;

public interface IClienteTituloCalificadoRepository
{
    void AddClienteTituloCalificado(ClienteTituloCalificado clienteTituloCalificado, List<AdjuntoVinculado> adjuntos);
    void UpdateClienteTituloCalificado(ClienteTituloCalificado clienteTituloCalificado, List<AdjuntoVinculado>? adjuntoVinculados = null);
    void RemoveClienteTituloCalificado(ClienteTituloCalificado clienteTituloCalificado);
    IQueryable<ClienteTituloCalificado> GetClienteTituloCalificadoByKeys(TipoCalificado tipoCalificado, string identificacionClienteTitulo, DateTime fechaDesde, DateTime? fechaHasta = null);
    ClienteTituloCalificado GetClienteTituloCalificadoById(int id);
    Task<IQueryable<ClienteTituloCalificado>> ListClienteTituloCalificado(TipoCalificado? tipoCalificado = null);
    ClienteTituloCalificado GetLastClienteTituloCalificadoVigente(int clave, int version);
    IQueryable<ClienteTituloCalificado> GetClienteTituloCalificadoByEstados(int? clave = null, bool vigente = true,
            bool borrador = true, bool rechazado = true, bool obsoleto = true);

    IQueryable<ClienteTituloCalificado> GetClienteTituloCalificadoByIdentificadorAndCalificadora(string identificador, int calificadoraId);
}
