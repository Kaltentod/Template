namespace BNA.IB.Calificaciones.API.Domain.Entities.Adjuntos;

public interface IAdjuntosRepository
{
    Adjunto AddAdjuntos(Adjunto adjunto);
    Task AddAdjuntoVinculado(AdjuntoVinculado adjuntoVinculado);
    Task AddAdjuntoVinculado(List<AdjuntoVinculado> adjuntoVinculado);
    List<AdjuntoVinculado> GetAdjuntosVinculadosByPropietarioId(int id, string propietarioTipo);
    void DelAdjuntoVinculado(AdjuntoVinculado adjuntoVinculado);
    Adjunto GetAdjuntosById(int id);

    List<Adjunto> GetFileNames(List<AdjuntoVinculado> adjuntoVinculados);
}
