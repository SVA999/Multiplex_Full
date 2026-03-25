using System;
using MultiplexFull.Core.Cine;
using MultiplexFull.Core.Cine.Sala.Sillas;
using MultiplexFull.Suscripciones;

namespace MultiplexCinema;

public class FactoryBFuncion : FactoryBoleta
{
    private readonly Funcion _funcion;

    public FactoryBFuncion(Funcion funcion)
    {
        ArgumentNullException.ThrowIfNull(funcion, nameof(funcion));
        _funcion = funcion;
    }

    public Funcion Funcion => _funcion;

    public IBoleta CrearBoleta(Espectador espectador, Silla silla, double valor, Funcion funcion)
    {
        ArgumentNullException.ThrowIfNull(funcion, nameof(funcion));
        // Validar disponibilidad de la silla
        if (!silla.Estado.PuedeSeleccionarse())
            throw new InvalidOperationException(
                $"La silla {silla.Fila}{silla.Numero} no está disponible para selección.");

        return base.CrearBoleta(espectador, silla, valor);
    }

    protected override IBoleta FabricarBoleta(Espectador espectador, Silla silla, decimal precioFinal)
    {
        long id = GenerarId();

        // Seleccionar tipo de boleta según categoría de silla
        IBoleta boleta = silla switch
        {
            SillaVip => new BoletaVip(id, espectador, _funcion, silla, precioFinal),
            SillaGeneral => new BoletaGeneral(id, espectador, _funcion, silla, precioFinal),
            _ => new BoletaGeneral(id, espectador, _funcion, silla, precioFinal)
        };

        // Marcar la silla como ocupada
        silla.CambiarEstado(new SillaOcupado());
        return boleta;
    }
}