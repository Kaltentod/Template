using BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraRiesgosPeriodo;
using BNA.IB.Calificaciones.API.Domain.Enums;

namespace BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraRiesgos;

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
