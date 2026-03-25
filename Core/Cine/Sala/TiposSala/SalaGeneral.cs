using System;
using MultiplexCinema;
using MultiplexFull.Estrategias.Sillas;

namespace MultiplexFull.Core.Cine.Sala.TiposSala;

/// <summary>
/// SalaGeneral - General cinema type class
/// </summary>
public class SalaGeneral : ITipoSala
{
    public byte Id { get; init; }
    
    private readonly string _descripcion = "Normal";
    public string Descripcion 
    { 
        get => _descripcion; 
        init
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _descripcion = value;
            }
        }
    }

    public IStrategyLlenarSilla EstrategiaLlenado { get; init; }

    public SalaGeneral(byte id, string descripcion = "Normal", int filas = ConfiguracionMultiplex.FilasDefault, int columnas = ConfiguracionMultiplex.ColumnasDefault)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        Id = id;
        Descripcion = descripcion;
        
        // Uso de configuración central para poblar la estrategia dinámicamente.
        EstrategiaLlenado = new LlenarGeneral(filas, columnas, ConfiguracionMultiplex.PrecioSillaGeneral, ConfiguracionMultiplex.PrecioSillaVip);
    }

    public string ObtenerTipoSala() => Descripcion;

    public IStrategyLlenarSilla Llenado() => EstrategiaLlenado;
}
