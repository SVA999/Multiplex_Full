using System;
using MultiplexCinema;
using MultiplexFull.Estrategias.Sillas;

namespace MultiplexFull.Core.Cine.Sala.TiposSala;

/// <summary>
/// SalaVip - VIP cinema type class
/// </summary>
public class SalaVip : ITipoSala
{
    public byte Id { get; init; }

    private readonly string _descripcion = "VIP";
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

    public SalaVip(byte id, string descripcion = "VIP", int filas = ConfiguracionMultiplex.FilasVipDefault, int columnas = ConfiguracionMultiplex.ColumnasVipDefault)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        Id = id;
        Descripcion = descripcion;
        
        EstrategiaLlenado = new LlenarVip(filas, columnas, ConfiguracionMultiplex.PrecioSillaVip);
    }

    public string ObtenerTipoSala() => Descripcion;
}
