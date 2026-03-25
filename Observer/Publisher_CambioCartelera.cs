using System;
using System.Collections.Generic;

namespace MultiplexCinema;

public class Publisher_CambioCartelera
{
    // Delegado y evento C# para la notificación
    public delegate void DelegadoCambioCartelera();
    public event DelegadoCambioCartelera? EvtCambioCartelera;

    // Lista explícita de suscriptores (para poder agregar/quitar manualmente)
    private readonly List<ISuscripcionCartelera> _suscriptores = [];

    // ── Gestión de suscriptores ──────────────────────────────────────────────

    public void Suscribir(ISuscripcionCartelera suscriptor)
    {
        ArgumentNullException.ThrowIfNull(suscriptor, nameof(suscriptor));
        if (!_suscriptores.Contains(suscriptor))
        {
            _suscriptores.Add(suscriptor);
            // También lo engancha al evento C#
            EvtCambioCartelera += suscriptor.ActualizarCartelera;
            Console.WriteLine($"[Publisher] Suscriptor registrado: {suscriptor.GetType().Name}");
        }
    }


    public void Desuscribir(ISuscripcionCartelera suscriptor)
    {
        ArgumentNullException.ThrowIfNull(suscriptor, nameof(suscriptor));
        if (_suscriptores.Remove(suscriptor))
        {
            EvtCambioCartelera -= suscriptor.ActualizarCartelera;
            Console.WriteLine($"[Publisher] Suscriptor eliminado: {suscriptor.GetType().Name}");
        }
    }

    // ── Publicación ──────────────────────────────────────────────────────────
    public void NotificarCambioCartelera()
    {
        Console.WriteLine($"[Publisher] Notificando cambio de cartelera a {_suscriptores.Count} suscriptor(es)...");
        EvtCambioCartelera?.Invoke();
    }

    // ── Propiedades de consulta ──────────────────────────────────────────────
    public IReadOnlyList<ISuscripcionCartelera> Suscriptores => _suscriptores.AsReadOnly();
}