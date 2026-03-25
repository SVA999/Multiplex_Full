using b_Multiplex.Interfaces;
using System.Collections.Generic;

namespace MultiplexCinema;

/// <summary>
/// NivelNormal - NivelNormal subscription tier class
/// </summary>
public class NivelNormal : INivel
{
    private List<IBeneficios> _beneficios = new List<IBeneficios>();
    private decimal descuento = 0.0m;

    public decimal AplicarDescuentoBoleta(decimal precio)
    {
        return precio - (precio * descuento);
    }

    public decimal AplicarDescuentoComida(decimal precio)
    {
        return precio;
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

