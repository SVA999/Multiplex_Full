using MultiplexCinema;

namespace MultiplexFull.Core.Cine.Sala.Sillas;

/// <summary>
/// SillaAcompanante - Companion seat class
/// </summary>
public class SillaAcompanante : Silla
{
    public SillaAcompanante(string fila, byte numero, IEstadoSilla estado, decimal valor) 
        : base(fila, numero, estado, valor)
    {
    }
}
