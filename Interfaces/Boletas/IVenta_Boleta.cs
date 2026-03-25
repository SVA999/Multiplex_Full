using MultiplexFull.Core.Cine.Sala.Sillas;
using MultiplexFull.Suscripciones;

namespace MultiplexCinema;

/// <summary>
/// IVenta_Boleta - Interface for ticket sales
/// </summary>
public interface IVenta_Boleta
{
    IBoleta VenderBoleta(Espectador espectador, Silla silla, decimal dinero);
}
