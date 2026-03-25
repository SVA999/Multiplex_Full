using System;

namespace MultiplexCinema;

/// <summary>
/// Coleccionable - Clase para ítems coleccionables
/// </summary>
public class Coleccionable : IConsumible
{
    private readonly string _descripcion = string.Empty;
    private readonly decimal _precio;
    private readonly string _coleccion = string.Empty;

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

    public string Coleccion 
    { 
        get => _coleccion; 
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Coleccion));
            _coleccion = value;
        }
    }

    // Cambiado de protected a public
    public Coleccionable(string descripcion, decimal precio, string coleccion)
    {
        Descripcion = descripcion;
        Precio = precio;
        Coleccion = coleccion;
    }

    public virtual string ObtenerTipo() => $"Coleccionable de {Coleccion}: {Descripcion}";
    public virtual decimal ObtenerPrecio() => Precio;
}
