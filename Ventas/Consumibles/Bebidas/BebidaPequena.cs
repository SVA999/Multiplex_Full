using System;

namespace MultiplexCinema;

/// <summary>
/// BebidaPequena - Small beverage
/// </summary>
public class BebidaPequena : Bebida
{
    public BebidaPequena(string descripcion, decimal precio, string marca)
        : base(descripcion, precio, marca, TamanoConsumible.Pequeno)
    {
    }
}
