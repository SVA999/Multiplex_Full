using System;

namespace MultiplexCinema;

/// <summary>
/// BebidaGrande - Large beverage
/// </summary>
public class BebidaGrande : Bebida
{
    public BebidaGrande(string descripcion, decimal precio, string marca)
        : base(descripcion, precio, marca, TamanoConsumible.Grande)
    {
    }
}
