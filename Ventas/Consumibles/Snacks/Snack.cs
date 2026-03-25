using System;

namespace MultiplexCinema;

/// <summary>
/// Snack - Abstract base class for snack items
/// </summary>
public abstract class Snack : Alimento
{
    private readonly string _marca = string.Empty;

    public string Marca 
    { 
        get => _marca; 
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Marca));
            _marca = value;
        }
    }

    public TamanoConsumible Tamano { get; init; }

    protected Snack(string descripcion, decimal precio, string marca, TamanoConsumible tamano)
        : base(descripcion, precio)
    {
        Marca = marca;
        Tamano = tamano;
    }

    public override string ObtenerTipo() => $"Snack {Tamano} de {Marca} - {Descripcion}";
}
