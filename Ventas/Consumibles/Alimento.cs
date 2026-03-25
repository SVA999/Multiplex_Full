using System;

namespace MultiplexCinema;

/// <summary>
/// Tamaños disponibles para los consumibles
/// </summary>
public enum TamanoConsumible
{
    Pequeno,
    Mediano,
    Grande,
    Familiar,
    Unico
}

/// <summary>
/// Alimento - Abstract class for food items
/// </summary>
public abstract class Alimento : IConsumible
{
    private readonly string _descripcion = string.Empty;
    private readonly decimal _precio;

    public string Descripcion 
    { 
        get => _descripcion; 
        init 
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Descripcion));
            _descripcion = value;
        }
    }

    public decimal Precio 
    { 
        get => _precio; 
        init
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(Precio));
            _precio = value;
        }
    }

    protected Alimento(string descripcion, decimal precio)
    {
        Descripcion = descripcion;
        Precio = precio;
    }

    public abstract string ObtenerTipo();
    public virtual decimal ObtenerPrecio() => Precio;
}
