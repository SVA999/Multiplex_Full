using System;

namespace MultiplexCinema;

/// <summary>
/// SnackFamiliar - Family Size Snack
/// </summary>
public class SnackFamiliar : Snack
{
    public SnackFamiliar(string descripcion, decimal precio, string marca)
        : base(descripcion, precio, marca, TamanoConsumible.Familiar)
    {
    }
}
