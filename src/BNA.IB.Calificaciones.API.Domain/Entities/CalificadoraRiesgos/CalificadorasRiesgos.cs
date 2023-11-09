using BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraRiesgosPeriodo;
using BNA.IB.Calificaciones.API.Domain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BNA.IB.Calificaciones.API.Domain.Entities.CalificadoraRiesgos
{
    public class CalificadorasRiesgos : ICalificadorasRiesgos
    {
        public int Id {get;set;}
        public string Nombre { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateAproved { get; set; }
        public string UserAproved { get; set; }
        public TipoEstado Status { get; set; }
        public List<CalificadoraRiesgosPeriodos> CalificadorasRiesgosPeriodos { get; set; }

        public static CalificadorasRiesgos Crear(string usuario, string nombre, List<CalificadoraRiesgosPeriodos> calificadorasRiesgosPeriodo)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException(null, nameof(nombre));
            var calificadora = new CalificadorasRiesgos
            {
                UserCreated = usuario,
                Nombre = nombre,
                CalificadorasRiesgosPeriodos = calificadorasRiesgosPeriodo,
            };

            return calificadora;
        }

        public void Aprobar(string usuario)
        {
            if (Status != TipoEstado.SinVerificar) throw new ApplicationException("Solo es posible aprobar Sin Verificar.");

            Status = TipoEstado.Vigente;
            UserAproved = usuario;
            DateAproved = DateTime.Now;
        }

        public Validacion VerificarPeriodos(List<CalificadoraRiesgosPeriodos>? nuevoPeriodos = null)
        {
            if (nuevoPeriodos == null)
            {
                nuevoPeriodos = new List<CalificadoraRiesgosPeriodos>();
            }

            var periodoComprar = nuevoPeriodos;

            periodoComprar.AddRange(CalificadorasRiesgosPeriodos);

            periodoComprar = periodoComprar.Where(x => x.Status != TipoEstado.Obsoleto).ToList();

            periodoComprar.Sort((p1, p2) => p1.FechaAlta.CompareTo(p2.FechaAlta));
            string errorMensaje = "";
            int idError = 0;

            for (int i = 0; i < periodoComprar.Count - 1 && errorMensaje == ""; i++)
            {
                var periodoActual = periodoComprar[i];
                var periodoSiguiente = periodoComprar[i + 1];

                if (periodoActual.FechaBaja >= periodoSiguiente.FechaAlta)
                {
                    errorMensaje = $"El periodo de {periodoActual.FechaAlta} hasta {periodoActual.FechaBaja} se superpone con el periodo de {periodoSiguiente.FechaAlta} hasta {periodoSiguiente.FechaBaja}";
                    idError = periodoActual.Id;
                }
            }


            return new Validacion()
            {
                EsValido = errorMensaje == ""? true : false,
                ErrorMensaje = errorMensaje,
                IdError = idError
            };
        }
    }
    
}
