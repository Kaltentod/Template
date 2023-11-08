using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ari_ib_calificaciones_api_domain.Entities.CalificacionesBCRACodigo
{
    public class CalificacionesBCRACodigo
    {
        public int Id { get; set; }
        public string Calificacion { get; set; }
        public int CalificacionBCRAId { get; set; }
        public int ValorNumerico { get; set; }
    }
}
