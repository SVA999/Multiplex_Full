using System;

namespace MultiplexCinema;

/// <summary>
/// Bebida - Abstract class for beverages
/// </summary>
public abstract class Bebida : Alimento
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

    protected Bebida(string descripcion, decimal precio, string marca, TamanoConsumible tamano)
        : base(descripcion, precio)
    {
        Marca = marca;
        Tamano = tamano;
    }

    public override string ObtenerTipo() => $"Bebida {Tamano} de {Marca} - {Descripcion}";
}
