using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ari_ib_calificaciones_api_domain.Entities
{
    public sealed class Validacion
    {
        public bool EsValido { get; set; }
        public string ErrorMensaje { get; set; }
        public int IdError { get; set; }
    }
}
