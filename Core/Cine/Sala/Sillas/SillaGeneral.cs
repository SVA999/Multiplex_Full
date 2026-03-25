using MultiplexCinema;

namespace MultiplexFull.Core.Cine.Sala.Sillas;

/// <summary>
/// SillaGeneral - General admission seat class
/// </summary>
public class SillaGeneral : Silla
{
    public SillaGeneral(string fila, byte numero, IEstadoSilla estado, decimal valor) 
        : base(fila, numero, estado, valor)
    {
    }
}
