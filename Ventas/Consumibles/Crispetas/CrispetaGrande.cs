using System;

namespace MultiplexCinema;

/// <summary>
/// CrispetaGrande - Large popcorn class
/// </summary>
public class CrispetaGrande : Crispeta
{
    public CrispetaGrande(string descripcion, decimal precio)
        : base(descripcion, precio, TamanoConsumible.Grande)
    {
    }
}
