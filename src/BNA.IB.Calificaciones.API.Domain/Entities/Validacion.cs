using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNA.IB.Calificaciones.API.Domain.Entities
{
    public sealed class Validacion
    {
        public bool EsValido { get; set; }
        public string ErrorMensaje { get; set; }
        public int IdError { get; set; }
    }
}
