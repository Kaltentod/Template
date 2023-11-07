using ari_ib_calificaciones_api_domain.Entities.Adjuntos;
using ari_ib_calificaciones_api_domain.Shared.Enums;

namespace ari_ib_calificaciones_api_domain.Entities.CalificadoraRiesgos;

public interface ICalificadorasRiesgosRepository
{
    void AddCalificadorasRiesgos(CalificadorasRiesgos calificadorasRiesgos, List<AdjuntoVinculado> adjuntos);
    void UpdateCalificadorasRiesgos(CalificadorasRiesgos calificadorasRiesgos, bool editarRangos = true, List<AdjuntoVinculado>? adjuntoVinculados = null);
    void RemoveCalificadorasRiesgos(CalificadorasRiesgos calificadorasRiesgos);
    IQueryable<CalificadorasRiesgos> GetClasificadoraRiesgos();
    CalificadorasRiesgos GetCalificadorasRiesgosById(int id);
    IQueryable<CalificadorasRiesgos> ListCalificadorasRiesgos(TipoEstado? tipoEstado = null);
    IQueryable<CalificadorasRiesgos> GetCalificadorasRiesgosByClave(int clave);
    CalificadorasRiesgos GetCalificadorasRiesgosVigente(int clave, DateTime fechaDesde, int version);
    IQueryable<CalificadorasRiesgos> GetClasificadoraRiesgosByEstados(int? clave = null, bool vigente = true,
            bool borrador = true, bool rechazado = true, bool obsoleto = true);
    List<CalificadorasRiesgos> GetCalificadorasRiesgosClaves();
}
