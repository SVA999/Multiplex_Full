using System;

namespace MultiplexCinema;

/// <summary>
/// CrispetaFamiliar - Family-size popcorn class
/// </summary>
public class CrispetaFamiliar : Crispeta
{
    public CrispetaFamiliar(string descripcion, decimal precio)
        : base(descripcion, precio, TamanoConsumible.Familiar)
    {
    }
}
