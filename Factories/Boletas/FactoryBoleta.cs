using System;
using MultiplexFull.Core.Cine.Sala.Sillas;
using MultiplexFull.Suscripciones;

namespace MultiplexCinema;

public abstract class FactoryBoleta
{
    private static long _contadorId = 1;

    protected static long GenerarId() => _contadorId++;

    public static long GenerarIdPublico() => _contadorId++;
    public IBoleta CrearBoleta(Espectador espectador, Silla silla, double valor)
    {
        ArgumentNullException.ThrowIfNull(espectador, nameof(espectador));
        ArgumentNullException.ThrowIfNull(silla, nameof(silla));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(valor, nameof(valor));

        // Aplicar descuento de nivel si el espectador tiene suscripción activa
        decimal precioFinal = (decimal)valor;
        INivel? nivel = espectador.Suscripcion?.Nivel;
        if (nivel is not null)
            precioFinal = nivel.AplicarDescuentoBoleta((decimal)valor);

        return FabricarBoleta(espectador, silla, precioFinal);
    }

    protected abstract IBoleta FabricarBoleta(Espectador espectador, Silla silla, decimal precioFinal);
}