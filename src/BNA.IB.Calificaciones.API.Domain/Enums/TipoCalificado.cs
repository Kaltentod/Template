using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BNA.IB.Calificaciones.API.Domain.Enums;

public enum TipoCalificado
{
    [Description("Cliente")] Cliente = 1,
    [Description("Título")] Titulo = 2,
}