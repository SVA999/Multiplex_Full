namespace MultiplexCinema;
using System.Collections.Generic;

/// <summary>
/// NivelOro - Gold subscription tier class
/// </summary>
public class NivelOro : INivel
{
    private List<IBeneficios> _beneficios = new List<IBeneficios>();
    private decimal descuento = 0.10m; // 10% discount example

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
