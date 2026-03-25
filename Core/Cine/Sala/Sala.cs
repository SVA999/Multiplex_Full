using System;
using MultiplexCinema;
using MultiplexFull.Core.Cine.Sala.Sillas;

namespace MultiplexFull.Core.Cine.Sala;

/// <summary>
/// Sala - Theater room/hall class
/// </summary>
public class Sala
{
    private readonly byte _id;
    private readonly Silla[,] _mSillas;
    private readonly ITipoSala _tipoSala;

    public byte Id => _id;
    public Silla[,] MSillas => _mSillas;
    public ITipoSala TipoSala => _tipoSala;
    public IEstadoSala Estado { get; private set; }

    /// <summary>
    /// Crea una nueva sala delegando la configuración y llenado de las sillas a su respectivo TipoSala y Estrategia.
    /// </summary>
    public Sala(byte numero, ITipoSala tipoSala)
    {
        try
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numero, nameof(numero));
            ArgumentNullException.ThrowIfNull(tipoSala, nameof(tipoSala));

            _id = numero;
            _tipoSala = tipoSala;
            
            // LLENADO DINÁMICO REGLAS REQUERIMIENTO #1
            _mSillas = _tipoSala.EstrategiaLlenado.LlenarSillas(); 
            
            Estado = new SalaDisponible(); // Estado inicial
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Error al instanciar Sala", ex);
        }
    }

    public void LimpiarSala()
    {
        try
        {
            int filas = _mSillas.GetLength(0);
            int columnas = _mSillas.GetLength(1);

            for (int fila = 0; fila < filas; fila++)
            {
                for (int columna = 0; columna < columnas; columna++)
                {
                    var silla = _mSillas[fila, columna];
                    if (silla != null && !silla.Estado.PuedeSeleccionarse())
                    {
                        silla.CambiarEstado(new SillaDisponible());
                    }
                }
            }
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Error al LimpiarSala: " + e.Message, e);
        }
    }

    public void CambiarEstado(IEstadoSala nuevoEstado)
    {
        ArgumentNullException.ThrowIfNull(nuevoEstado, nameof(nuevoEstado));
        Estado = nuevoEstado;
    }
}
