using System;

namespace MultiplexCinema;

/// <summary>
/// CrispetaPequena - Small popcorn class
/// </summary>
public class CrispetaPequena : Crispeta
{
    public CrispetaPequena(string descripcion, decimal precio)
        : base(descripcion, precio, TamanoConsumible.Pequeno)
    {
    }
}
