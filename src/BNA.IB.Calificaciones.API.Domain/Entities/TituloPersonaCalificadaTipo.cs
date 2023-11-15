using System.ComponentModel;

namespace BNA.IB.Calificaciones.API.Domain.Entities;

public enum TituloPersonaCalificadaTipo
{
    [Description("Cliente")] Cliente = 1,
    [Description("Titulo")] Titulo = 2
}