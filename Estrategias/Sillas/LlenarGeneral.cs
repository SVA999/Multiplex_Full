using System;
using MultiplexCinema;
using MultiplexFull.Core.Cine.Sala.Sillas;

namespace MultiplexFull.Estrategias.Sillas;

/// <summary>
/// LlenarGeneral - Strategy for filling general admission seats with a few VIP at the back
/// </summary>
public class LlenarGeneral : IStrategyLlenarSilla
{
    private readonly int _filas;
    private readonly int _columnas;
    private readonly decimal _precioGeneral;
    private readonly decimal _precioVip;

    public LlenarGeneral(int filas = 16, int columnas = 10, decimal precioGeneral = 10000, decimal precioVip = 15000)
    {
        _filas = filas;
        _columnas = columnas;
        _precioGeneral = precioGeneral;
        _precioVip = precioVip;
    }

    private string GetLetraFila(int fila)
    {
        if (fila < 26)
            return ((char)('A' + fila)).ToString();
        return ((char)('A' + (fila / 26) - 1)).ToString() + ((char)('A' + (fila % 26))).ToString();
    }

    public Silla[,] LlenarSillas()
    {
        var sillas = new Silla[_filas, _columnas];
        int filasVip = 2;
        int filasGenerales = Math.Max(0, _filas - filasVip);

        // Fill general seats
        for (int fila = 0; fila < filasGenerales; fila++)
        {
            string letraFila = GetLetraFila(fila);
            for (int col = 0; col < _columnas; col++)
            {
                sillas[fila, col] = new SillaGeneral(letraFila, (byte)(col + 1), new SillaDisponible(), _precioGeneral);
            }
        }

        // Fill VIP seats at the back
        for (int fila = filasGenerales; fila < _filas; fila++)
        {
            string letraFila = GetLetraFila(fila);
            for (int col = 0; col < _columnas; col++)
            {
                sillas[fila, col] = new SillaVip(letraFila, (byte)(col + 1), new SillaDisponible(), _precioVip);
            }
        }

        return sillas;
    }
}
