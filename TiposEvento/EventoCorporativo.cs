using System;

namespace MultiplexCinema;

/// <summary>
/// EventoCorporativo - Corporate event class
/// </summary>
public class EventoCorporativo : ITipoEvento
{
    private readonly string _descripcion = string.Empty;
    private readonly string _empresa = string.Empty;

    public string Descripcion 
    { 
        get => _descripcion; 
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Descripcion));
            _descripcion = value;
        }
    }

    public string Empresa 
    { 
        get => _empresa; 
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Empresa));
            _empresa = value;
        }
    }

    public EventoCorporativo(string empresa, string descripcion)
    {
        Empresa = empresa;
        Descripcion = descripcion;
    }

    public string ObtenerTipo() => $"Corporativo - {Empresa}: {Descripcion}";
}
