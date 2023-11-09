namespace BNA.IB.Calificaciones.API.Domain.Entities.Adjuntos
{
    public class Adjunto
    {
        public int Id { get; set; }
        public string ArchivoNombre { get; set; }
        public long Size { get; set; }
        public byte[] ArchivoContenido { get; set; }
        public string ArchivoTipo { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserCreated { get; set; }

        public static Adjunto Crear(string usuario, string nombre, byte[] contenido, string archivoTipo, long size)
        {
            return new Adjunto
            {
                UserCreated = usuario,
                ArchivoNombre = nombre,
                ArchivoTipo = archivoTipo,
                ArchivoContenido = contenido,
                DateCreated = DateTime.Now,
                Size = size
            };
        }
    }

    public class AdjuntoVinculado
    {
        public int Id { get; set; }
        public int PropietarioId { get; set; }
        public int AdjuntoId { get; set; }
        public string PropietarioTipo { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserCreated { get; set; }

        public static AdjuntoVinculado Crear(string usuario, int propietarioId, int adjuntoId, string propietarioTipo)
        {
            return new AdjuntoVinculado
            {
                UserCreated = usuario,
                PropietarioId = propietarioId,
                AdjuntoId = adjuntoId,
                PropietarioTipo = propietarioTipo
            };
        }
        public static List<AdjuntoVinculado> CrearLista(string usuario, int propietarioId, List<int> adjuntoId, string propietarioTipo)
        {
            var lista = new List<AdjuntoVinculado>();

            foreach (var id in adjuntoId)
            {
                lista.Add(
                    new AdjuntoVinculado
                    {
                        UserCreated = usuario,
                        PropietarioId = propietarioId,
                        AdjuntoId = id,
                        PropietarioTipo = propietarioTipo
                    });
            }


            return lista;
        }
    }
}
