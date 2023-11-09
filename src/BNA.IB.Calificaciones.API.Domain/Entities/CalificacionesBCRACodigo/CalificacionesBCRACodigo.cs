using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNA.IB.Calificaciones.API.Domain.Entities.CalificacionesBCRACodigo
{
    public class CalificacionesBCRACodigo
    {
        public int Id { get; set; }
        public string Calificacion { get; set; }
        public int CalificacionBCRAId { get; set; }
        public int ValorNumerico { get; set; }
    }
}
