using System;
using MultiplexFull.Core.Cine.Sala.Sillas;
using MultiplexFull.Suscripciones;

namespace MultiplexCinema;

/// <summary>
/// BoletaPrivada - Private event ticket class
/// </summary>
public class BoletaPrivada : IBoletaEvento
{
    public long Id { get; init; }
    public Espectador Espectador { get; init; }
    public ITipoEvento Evento { get; init; }
    public Silla Silla { get; init; }
    public decimal Valor { get; init; }

    public BoletaPrivada(long id, Espectador espectador, ITipoEvento evento, Silla silla, decimal valor)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        ArgumentNullException.ThrowIfNull(espectador, nameof(espectador));
        ArgumentNullException.ThrowIfNull(evento, nameof(evento));
        ArgumentNullException.ThrowIfNull(silla, nameof(silla));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(valor, nameof(valor));

        Id = id;
        Espectador = espectador;
        Evento = evento;
        Silla = silla;
        Valor = valor;
    }

    public string ObtenerInfo() => $"[Boleta Evento Privado] ID: {Id} | Espectador: {Espectador.Nombre} | Evento: {Evento.ObtenerTipo()} | Valor: {Valor:C}";

    public INivel AplicarDescuento() => Espectador.Suscripcion?.Nivel;

    public decimal ObtenerValor() => Valor;
}
