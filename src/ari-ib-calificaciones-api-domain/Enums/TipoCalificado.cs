using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ari_ib_calificaciones_api_domain.Enums;

public enum TipoCalificado
{
    [Description("Cliente")] Cliente = 1,
    [Description("Título")] Titulo = 2,
}