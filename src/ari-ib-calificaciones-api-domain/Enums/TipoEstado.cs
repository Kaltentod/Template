using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BNA.IB.WEBAPP.Domain.Shared.Enums;

public enum TipoEstado
{
    [Description("Obsoleto")] Obsoleto = -1,
    [Description("Sin Verificar")] SinVerificar = 0,
    [Description("Vigente")] Vigente = 1,
    [Description("Rechazado")] Rechazado = 2,
}