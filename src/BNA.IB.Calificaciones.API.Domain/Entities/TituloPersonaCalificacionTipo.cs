using System.ComponentModel;

namespace BNA.IB.Calificaciones.API.Domain.Entities;

public enum TituloPersonaCalificacionTipo
{
    [Description("Cliente")] Cliente = 1,
    [Description("Titulo")] Titulo = 2
}