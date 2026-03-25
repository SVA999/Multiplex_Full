using System;

namespace MultiplexCinema;

/// <summary>
/// EventoPrivado - Private event class
/// </summary>
public class EventoPrivado : ITipoEvento
{
    private readonly string _descripcion = string.Empty;

    public string Descripcion 
    { 
        get => _descripcion; 
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Descripcion));
            _descripcion = value;
        }
    }

    public EventoPrivado(string descripcion)
    {
        Descripcion = descripcion;
    }

    public string ObtenerTipo() => $"Privado: {Descripcion}";
}
