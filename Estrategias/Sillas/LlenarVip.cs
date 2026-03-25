using System;
using MultiplexCinema;
using MultiplexFull.Core.Cine.Sala.Sillas;

namespace MultiplexFull.Estrategias.Sillas;

/// <summary>
/// LlenarVip - Strategy for filling VIP rooms entirely with VIP seats
/// </summary>
public class LlenarVip : IStrategyLlenarSilla
{
    private readonly int _filas;
    private readonly int _columnas;
    private readonly decimal _precioVip;

    public LlenarVip(int filas = 10, int columnas = 8, decimal precioVip = 20000)
    {
        _filas = filas;
        _columnas = columnas;
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

        for (int fila = 0; fila < _filas; fila++)
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
