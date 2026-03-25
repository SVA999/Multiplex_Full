using System;

namespace MultiplexCinema;

/// <summary>
/// SuscripcionCancelada — Estado: la suscripción fue cancelada voluntariamente.
/// Los beneficios no están disponibles hasta que se reactive.
/// </summary>
public class SuscripcionCancelada : IEstadoSuscripcion
{
    private readonly Suscripcion _contexto;

    public SuscripcionCancelada(Suscripcion contexto)
    {
        ArgumentNullException.ThrowIfNull(contexto, nameof(contexto));
        _contexto = contexto;
    }

    /// <summary>Sin beneficios en estado cancelado.</summary>
    public void UsarBeneficios()
    {
        Console.WriteLine("[SuscripcionCancelada] No se pueden usar beneficios — la suscripción está cancelada.");
    }

    /// <summary>Reactiva la suscripción extendiendo su fecha de fin un mes desde hoy.</summary>
    public void Activar()
    {
        Console.WriteLine("[SuscripcionCancelada] Reactivando suscripción...");
        _contexto.FechaInicio = DateTime.Now;
        _contexto.FechaFin = DateTime.Now.AddMonths(1);
        _contexto.Estado = new SuscripcionActiva(_contexto);
    }

    /// <summary>Ya está cancelada — no hay nada más que suspender.</summary>
    public void Suspender()
    {
        Console.WriteLine("[SuscripcionCancelada] La suscripción ya se encuentra cancelada.");
    }
}