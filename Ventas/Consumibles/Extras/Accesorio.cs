using System;

namespace MultiplexCinema;

/// <summary>
/// Accesorio - Clase de accesorios
/// </summary>
public class Accesorio : IConsumible
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

    // Cambiado de protected a public
    public Accesorio(string descripcion, decimal precio)
    {
        Descripcion = descripcion;
        Precio = precio;
    }

    public virtual string ObtenerTipo() => $"Accesorio: {Descripcion}";
    public virtual decimal ObtenerPrecio() => Precio;
}
