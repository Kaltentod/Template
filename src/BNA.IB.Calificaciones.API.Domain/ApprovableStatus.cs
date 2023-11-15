using System.ComponentModel;

namespace BNA.IB.Calificaciones.API.Domain;

public enum ApprovableStatus
{
    [Description("Obsoleto")] Obsoleto = -1,
    [Description("Borrador")] Borrador = 0,
    [Description("Vigente")] Vigente = 1
}