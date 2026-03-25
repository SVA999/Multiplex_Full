using System;

namespace MultiplexCinema;

/// <summary>
/// Crispeta - Abstract class for popcorn
/// </summary>
public abstract class Crispeta : Alimento
{
    public TamanoConsumible Tamano { get; init; }

    protected Crispeta(string descripcion, decimal precio, TamanoConsumible tamano)
        : base(descripcion, precio)
    {
        Tamano = tamano;
    }

    public override string ObtenerTipo() => $"Crispetas {Tamano} - {Descripcion}";
}
