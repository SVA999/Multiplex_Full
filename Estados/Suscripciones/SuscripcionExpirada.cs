using System;

namespace MultiplexCinema;

/// <summary>
/// SuscripcionExpirada — Estado: la suscripción venció por fecha.
/// Requiere renovación explícita para volver a estar activa.
/// </summary>
public class SuscripcionExpirada : IEstadoSuscripcion
{
    private readonly Suscripcion _contexto;

    public SuscripcionExpirada(Suscripcion contexto)
    {
        ArgumentNullException.ThrowIfNull(contexto, nameof(contexto));
        _contexto = contexto;
    }

    /// <summary>Beneficios no disponibles — suscripción expirada.</summary>
    public void UsarBeneficios()
    {
        Console.WriteLine("[SuscripcionExpirada] No se pueden usar beneficios — la suscripción ha expirado. Renuévala para continuar.");
    }

    /// <summary>Renueva la suscripción por un mes y la activa desde hoy.</summary>
    public void Activar()
    {
        Console.WriteLine("[SuscripcionExpirada] Renovando suscripción por 1 mes...");
        _contexto.FechaInicio = DateTime.Now;
        _contexto.FechaFin = DateTime.Now.AddMonths(1);
        _contexto.Estado = new SuscripcionActiva(_contexto);
        Console.WriteLine($"[SuscripcionExpirada] Suscripción activa hasta: {_contexto.FechaFin:d}");
    }

    /// <summary>No aplica — ya expiró por tiempo, no por acción del usuario.</summary>
    public void Suspender()
    {
        Console.WriteLine("[SuscripcionExpirada] La suscripción ya expiró. Use Activar() para renovarla.");
    }
}