using System;
using MultiplexFull.Core.Cine.Sala.Sillas;
using MultiplexFull.Suscripciones;

namespace MultiplexCinema;

public class FactoryBEvento : FactoryBoleta
{
    private readonly ITipoEvento _evento;

    public FactoryBEvento(ITipoEvento evento)
    {
        ArgumentNullException.ThrowIfNull(evento, nameof(evento));
        _evento = evento;
    }
    public ITipoEvento Evento => _evento;

    public IBoleta CrearBoleta(Espectador espectador, Silla silla, double valor, ITipoEvento evento)
    {
        ArgumentNullException.ThrowIfNull(evento, nameof(evento));

        if (!silla.Estado.PuedeSeleccionarse())
            throw new InvalidOperationException(
                $"La silla {silla.Fila}{silla.Numero} no está disponible para selección.");

        return base.CrearBoleta(espectador, silla, valor);
    }

    protected override IBoleta FabricarBoleta(Espectador espectador, Silla silla, decimal precioFinal)
    {
        long id = GenerarId();

        IBoleta boleta = _evento switch
        {
            EventoCorporativo corp =>
                new BoletaCorporativa(id, espectador, _evento, corp.Empresa, silla, precioFinal),
            _ =>
                new BoletaPrivada(id, espectador, _evento, silla, precioFinal)
        };

        // Marcar la silla como ocupada
        silla.CambiarEstado(new SillaOcupado());
        return boleta;
    }
}