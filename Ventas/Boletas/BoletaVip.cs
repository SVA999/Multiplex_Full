using System;
using MultiplexFull.Core.Cine;
using MultiplexFull.Core.Cine.Sala.Sillas;
using MultiplexFull.Suscripciones;

namespace MultiplexCinema;

/// <summary>
/// BoletaVip - VIP ticket class
/// </summary>
public class BoletaVip : IBoletaFuncion
{
    public long Id { get; init; }
    public Espectador Espectador { get; init; }
    public Funcion Funcion { get; init; }
    public Silla Silla { get; init; }
    public decimal Valor { get; init; }

    public BoletaVip(long id, Espectador espectador, Funcion funcion, Silla silla, decimal valor)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        ArgumentNullException.ThrowIfNull(espectador, nameof(espectador));
        ArgumentNullException.ThrowIfNull(funcion, nameof(funcion));
        ArgumentNullException.ThrowIfNull(silla, nameof(silla));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(valor, nameof(valor));

        Id = id;
        Espectador = espectador;
        Funcion = funcion;
        Silla = silla;
        Valor = valor;
    }

    public string ObtenerInfo() => $"[Boleta VIP] ID: {Id} | Espectador: {Espectador.Nombre} | Función: {Funcion.Codigo} | Valor: {Valor:C}";

    public INivel AplicarDescuento() => Espectador.Suscripcion?.Nivel;

    public decimal ObtenerValor() => Valor;
}
