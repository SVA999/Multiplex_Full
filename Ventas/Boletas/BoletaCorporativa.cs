using System;
using MultiplexFull.Core.Cine.Sala.Sillas;
using MultiplexFull.Suscripciones;

namespace MultiplexCinema;

/// <summary>
/// BoletaCorporativa - Corporate ticket class
/// </summary>
public class BoletaCorporativa : IBoletaEvento
{
    public long Id { get; init; }
    public Espectador Espectador { get; init; }
    public ITipoEvento Evento { get; init; }
    public string Empresa { get; init; }
    public Silla Silla { get; init; }
    public decimal Valor { get; init; }

    public BoletaCorporativa(long id, Espectador espectador, ITipoEvento evento, string empresa, Silla silla, decimal valor)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        ArgumentNullException.ThrowIfNull(espectador, nameof(espectador));
        ArgumentNullException.ThrowIfNull(evento, nameof(evento));
        ArgumentNullException.ThrowIfNull(silla, nameof(silla));
        ArgumentException.ThrowIfNullOrWhiteSpace(empresa, nameof(empresa));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(valor, nameof(valor));

        Id = id;
        Espectador = espectador;
        Evento = evento;
        Empresa = empresa;
        Silla = silla;
        Valor = valor;
    }

    public string ObtenerInfo() => $"[Boleta Evento Corporativo: {Empresa}] ID: {Id} | Espectador: {Espectador.Nombre} | Evento: {Evento.ObtenerTipo()} | Valor: {Valor:C}";

    public INivel AplicarDescuento() => Espectador.Suscripcion?.Nivel;

    public decimal ObtenerValor() => Valor;
}
