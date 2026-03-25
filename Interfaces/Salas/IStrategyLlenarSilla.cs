using MultiplexFull.Core.Cine.Sala.Sillas;

namespace MultiplexCinema;

/// <summary>
/// IStrategyLlenarSilla - Interface for seat filling strategy
/// </summary>
public interface IStrategyLlenarSilla
{
    /// <summary>
    /// Genera la matriz de sillas de la sala y asigna su estado inicial y precio dinámicamente
    /// </summary>
    Silla[,] LlenarSillas();

}
