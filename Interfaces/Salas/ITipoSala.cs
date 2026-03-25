using MultiplexFull.Core.Cine.Pelicula;

namespace MultiplexCinema;

/// <summary>
/// ITipoSala - Interface for room/hall type
/// </summary>
public interface ITipoSala
{
    string ObtenerTipoSala();
    
    /// <summary>
    /// Estrategia que dictamina cómo deben generarse las sillas para este tipo de sala.
    /// </summary>
    IStrategyLlenarSilla EstrategiaLlenado { get; }
}
