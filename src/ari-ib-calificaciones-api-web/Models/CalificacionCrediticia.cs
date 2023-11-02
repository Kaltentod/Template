using System.ComponentModel;

namespace ari_ib_calificaciones_api_web.Models;

public enum CalificacionCrediticia
{
    [Description("CCC-")]
    CCCminus,

    [Description("CCC")]
    CCC,

    [Description("CCC+")]
    CCCplus,

    [Description("B-")]
    Bminus,

    [Description("B")]
    B,

    [Description("B+")]
    Bplus,

    [Description("BB-")]
    BBminus,

    [Description("BB")]
    BB,

    [Description("BB+")]
    BBplus,

    [Description("BBB-")]
    BBBminus,

    [Description("BBB")]
    BBB,

    [Description("BBB+")]
    BBBplus,

    [Description("A-")]
    Aminus,

    [Description("A")]
    A,

    [Description("A+")]
    Aplus,

    [Description("AA-")]
    AAminus,

    [Description("AA")]
    AA,

    [Description("AA+")]
    AAplus,

    [Description("AAA")]
    AAA
}
