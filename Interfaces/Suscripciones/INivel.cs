namespace MultiplexCinema;
using System.Collections.Generic;

/// <summary>
/// INivel - Interface for subscription level/tier
/// </summary>
public interface INivel
{
    decimal AplicarDescuentoBoleta(decimal precio);
    decimal AplicarDescuentoComida(decimal precio);
    List<IBeneficios> ObtenerBeneficios();
    decimal ObtenerDescuento();
}
