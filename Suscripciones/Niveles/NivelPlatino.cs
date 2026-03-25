namespace MultiplexCinema;
using System.Collections.Generic;

/// <summary>
/// NivelPlatino - Platinum subscription tier class
/// </summary>
public class NivelPlatino : INivel
{
    private List<IBeneficios> _beneficios = new List<IBeneficios>();
    private decimal descuento = 0.20m; // 20% discount example

    public decimal AplicarDescuentoBoleta(decimal precio)
    {
        return precio - (precio * descuento);
    }

    public decimal AplicarDescuentoComida(decimal precio)
    {
        return precio - (precio * descuento);
    }

    public List<IBeneficios> ObtenerBeneficios()
    {
        return _beneficios;
    }

    public decimal ObtenerDescuento()
    {
        return descuento;
    }
}
