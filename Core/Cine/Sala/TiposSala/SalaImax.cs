using System;
using MultiplexCinema;
using MultiplexFull.Estrategias.Sillas;

namespace MultiplexFull.Core.Cine.Sala.TiposSala;

/// <summary>
/// SalaImax - IMAX cinema type class
/// </summary>
public class SalaImax : ITipoSala
{
    public byte Id { get; init; }

    private readonly string _descripcion = "IMAX";
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

    public SalaImax(byte id, string descripcion = "IMAX", int filas = ConfiguracionMultiplex.FilasImaxDefault, int columnas = ConfiguracionMultiplex.ColumnasImaxDefault)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        Id = id;
        Descripcion = descripcion;
        
        // Sala IMAX puede reutilizar la estrategia mixta (LlenarGeneral) pero con proporciones/precios inflados y diferentes.
        EstrategiaLlenado = new LlenarGeneral(filas, columnas, ConfiguracionMultiplex.PrecioSillaImax, ConfiguracionMultiplex.PrecioSillaImaxVip);
    }

    public string ObtenerTipoSala() => Descripcion;

    public IStrategyLlenarSilla Llenado() => EstrategiaLlenado;
}
