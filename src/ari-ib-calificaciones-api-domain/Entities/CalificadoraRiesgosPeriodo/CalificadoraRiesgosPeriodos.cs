using ari_ib_calificaciones_api_domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ari_ib_calificaciones_api_domain.Entities.CalificadoraRiesgosPeriodo
{
    public class CalificadoraRiesgosPeriodos:  AuditableAggregateRoot<int>
    {
        public int Id { get; set; }
        public int Clave { get; set; }
        public int CalificadoraRiesgosId { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public DateTime FechaNotificacionAlta { get; set; }
        public DateTime FechaNotificacionBaja { get; set; }
        public List<CalificadorasRiesgosCalificacion.CalificadorasRiesgosCalificacion> CalificadorasRiegosCalificaciones { get; set; }

        public Validacion VerificarCalificaciones()
        {
            string errorMensaje = "";
            int idError = 0;

            for (int i = 0; i< CalificadorasRiegosCalificaciones.Count && errorMensaje == ""; i++) {
                var calificacionActual = CalificadorasRiegosCalificaciones[i];
                if (calificacionActual.CalificacionCalificadora == "" || calificacionActual.CalificacionesBcraCodigoId == 0)
                {
                    errorMensaje = $"Existen Calificaciones sin Valor";
                    idError = calificacionActual.CalificacionesBcraCodigoId;
                }

                for (int j = 0; i < CalificadorasRiegosCalificaciones.Count && errorMensaje == ""; j++)
                {
                    if (i != j)
                    {
                        if (calificacionActual.CalificacionCalificadora == CalificadorasRiegosCalificaciones[j].CalificacionCalificadora)
                        {
                            errorMensaje = $"Existen Calificaciones con el mismo valor. Valor = {calificacionActual.CalificacionCalificadora}";
                            idError = calificacionActual.CalificacionesBcraCodigoId;
                        }
                    }
                }
            }


            return new Validacion()
            {
                EsValido = errorMensaje == "" ? true : false,
                ErrorMensaje = errorMensaje,
                IdError = idError
            };
        }
    }
}
