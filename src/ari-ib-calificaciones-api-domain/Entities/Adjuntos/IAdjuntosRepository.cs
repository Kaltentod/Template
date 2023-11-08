namespace ari_ib_calificaciones_api_domain.Entities.Adjuntos;

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
