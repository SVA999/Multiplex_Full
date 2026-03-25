using System;
using System.Collections.Generic;
using MultiplexFull.Core.Cine;

namespace MultiplexCinema;
public class ActualizacionCartelera : ISuscripcionCartelera
{
    private readonly Cartelera _cartelera;
    private readonly Publisher_CambioCartelera _publisher;

    // Última lista pendiente de aplicar (la establece quien detecta el cambio)
    private List<Pelicula>? _nuevasPeliculas;

    public ActualizacionCartelera(Cartelera cartelera, Publisher_CambioCartelera publisher)
    {
        ArgumentNullException.ThrowIfNull(cartelera, nameof(cartelera));
        ArgumentNullException.ThrowIfNull(publisher, nameof(publisher));
        _cartelera = cartelera;
        _publisher = publisher;

        // Auto-registro como suscriptor
        _publisher.Suscribir(this);
    }

    // ── ISuscripcionCartelera ────────────────────────────────────────────────
    public void ActualizarCartelera()
    {
        if (_nuevasPeliculas is not null)
        {
            _cartelera.L_peliculas = _nuevasPeliculas;
            Console.WriteLine($"[ActualizacionCartelera] Cartelera actualizada: " +
                              $"{_nuevasPeliculas.Count} película(s) en cartelera.");
            _nuevasPeliculas = null;
        }
        else
        {
            Console.WriteLine("[ActualizacionCartelera] Notificación recibida — sin cambios pendientes.");
        }
    }

    public void ProgramarCambio(List<Pelicula> nuevasPeliculas)
    {
        ArgumentNullException.ThrowIfNull(nuevasPeliculas, nameof(nuevasPeliculas));
        _nuevasPeliculas = nuevasPeliculas;
        _publisher.NotificarCambioCartelera();
    }

    public void Desregistrar() => _publisher.Desuscribir(this);
}