using System;

namespace MultiplexCinema;

/// <summary>
/// SuscripcionActiva — Estado: la suscripción está vigente y todos sus beneficios son accesibles.
/// </summary>
public class SuscripcionActiva : IEstadoSuscripcion
{
    private readonly Suscripcion _contexto;

    public SuscripcionActiva(Suscripcion contexto)
    {
        ArgumentNullException.ThrowIfNull(contexto, nameof(contexto));
        _contexto = contexto;
    }

    /// <summary>Aplica los beneficios del nivel actual al espectador.</summary>
    public void UsarBeneficios()
    {
        decimal descuento = _contexto.Nivel.ObtenerDescuento();
        Console.WriteLine($"[SuscripcionActiva] Usando beneficios — descuento activo: {descuento:P0}");

        foreach (var b in _contexto.Nivel.ObtenerBeneficios())
            Console.WriteLine($"  · Beneficio: {b.ObtenerBeneficio()}");
    }

    /// <summary>Ya está activa — registra que no hay cambio.</summary>
    public void Activar()
    {
        Console.WriteLine("[SuscripcionActiva] La suscripción ya se encuentra activa.");
    }

    /// <summary>Suspende (cancela) la suscripción.</summary>
    public void Suspender()
    {
        Console.WriteLine("[SuscripcionActiva] Suspendiendo suscripción...");
        _contexto.Estado = new SuscripcionCancelada(_contexto);
    }
}