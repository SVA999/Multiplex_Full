using System;

namespace MultiplexCinema;

/// <summary>
/// SnackGrande - Large snack
/// </summary>
public class SnackGrande : Snack
{
    public SnackGrande(string descripcion, decimal precio, string marca)
        : base(descripcion, precio, marca, TamanoConsumible.Grande)
    {
    }
}
