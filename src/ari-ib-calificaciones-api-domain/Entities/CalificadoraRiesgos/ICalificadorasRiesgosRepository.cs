using ari_ib_calificaciones_api_domain.Entities.CalificadoraRiesgosPeriodo;
using ari_ib_calificaciones_api_domain.Enums;

namespace ari_ib_calificaciones_api_domain.Entities.CalificadoraRiesgos;

public interface ICalificadorasRiesgosRepository
{
    void AddCalificadorasRiesgos(CalificadorasRiesgos calificadorasRiesgos);
    void AddCalificadorasRiesgosPeriodo(CalificadoraRiesgosPeriodos calificadoraRiesgosPeriodo);
    void UpdateCalificadorasRiesgosPeriodo(CalificadoraRiesgosPeriodos calificadoraRiesgosPeriodo);
    void RemoveCalificadorasRiesgos(CalificadorasRiesgos calificadorasRiesgos);
    void RemoveCalificadorasRiesgosPeriodo(CalificadoraRiesgosPeriodos calificadorasRiesgos);
    IQueryable<CalificadorasRiesgos> GetClasificadoraRiesgos();
    CalificadorasRiesgos GetCalificadorasRiesgosById(int id);
    CalificadoraRiesgosPeriodos GetCalificadorasRiesgosPeriodoById(int id);
    IQueryable<CalificadorasRiesgos> GetClasificadoraRiesgosByEstados(bool vigente = true,
            bool borrador = true, bool rechazado = true, bool obsoleto = true);
}
