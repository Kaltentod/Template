using System.ComponentModel;

namespace BNA.IB.Calificaciones.API.Web.Models;

public enum CalificacionCrediticiaEnum
{
    [Description("AAA")]
    AAA = 1,

    [Description("AA+")]
    AAplus = 2,

    [Description("AA")]
    AA = 3,

    [Description("AA-")]
    AAminus = 4,

    [Description("A+")]
    Aplus = 5,

    [Description("A")]
    A = 6,

    [Description("A-")]
    Aminus = 7,

    [Description("BBB+")]
    BBBplus = 8,

    [Description("BBB")]
    BBB = 9,

    [Description("BBB-")]
    BBBminus = 10,

    [Description("BB+")]
    BBplus = 11,

    [Description("BB")]
    BB = 12,

    [Description("BB-")]
    BBminus = 13,

    [Description("B+")]
    Bplus = 14,

    [Description("B")]
    B = 15,

    [Description("B-")]
    Bminus = 16,

    [Description("CCC+")]
    CCCplus = 17,

    [Description("CCC")]
    CCC = 18,

    [Description("CCC-")]
    CCCminus = 19,

    [Description("CC")]
    CC = 20

}
