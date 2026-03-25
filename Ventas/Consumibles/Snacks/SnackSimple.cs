using System;

namespace MultiplexCinema;

/// <summary>
/// SnackSimple - Simple Snack
/// </summary>
public class SnackSimple : Snack
{
    public SnackSimple(string descripcion, decimal precio, string marca)
        : base(descripcion, precio, marca, TamanoConsumible.Unico)
    {
    }
}
