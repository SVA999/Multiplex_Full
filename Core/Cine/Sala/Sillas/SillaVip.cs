using MultiplexCinema;

namespace MultiplexFull.Core.Cine.Sala.Sillas;

/// <summary>
/// SillaVip - VIP seat class
/// </summary>
public class SillaVip : Silla
{
    public SillaVip(string fila, byte numero, IEstadoSilla estado, decimal valor) 
        : base(fila, numero, estado, valor)
    {
    }
}
