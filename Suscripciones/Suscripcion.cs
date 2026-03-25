using System;

namespace MultiplexCinema;

/// <summary>
/// Suscripcion - Manages a spectator's subscription lifecycle and tier (State pattern host).
/// Implements ISuscripcionCartelera so Espectadores can be registered as Observer subscribers.
/// </summary>
public class Suscripcion : ISuscripcionCartelera
{
    private static readonly INivel[] _escaleraDeNiveles =
        [new NivelNormal(), new NivelOro(), new NivelPlatino()];

    private DateTime _fechaInicio;
    private DateTime _fechaFin;
    private IEstadoSuscripcion _estado;
    private INivel _nivel;

    // ── Constructor ──────────────────────────────────────────────────────────
    public Suscripcion()
    {
        _fechaInicio = DateTime.Now;
        _fechaFin = _fechaInicio.AddMonths(1);
        _nivel = new NivelNormal();
        _estado = new SuscripcionActiva(this);
    }

    public Suscripcion(DateTime fechaInicio, DateTime fechaFin, INivel nivel)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(fechaInicio, fechaFin, nameof(fechaInicio));
        ArgumentNullException.ThrowIfNull(nivel, nameof(nivel));

        _fechaInicio = fechaInicio;
        _fechaFin = fechaFin;
        _nivel = nivel;
        _estado = new SuscripcionActiva(this);
    }

    // ── ISuscripcionCartelera ────────────────────────────────────────────────
    public void ActualizarCartelera()
    {
        Console.WriteLine($"[Suscripcion] Cartelera actualizada — nivel: {_nivel.GetType().Name}");
    }

    // ── Lifecycle ────────────────────────────────────────────────────────────
    /// <summary>Activa la suscripción y devuelve el estado resultante.</summary>
    public IEstadoSuscripcion Suscribir()
    {
        _estado.Activar();
        return _estado;
    }

    /// <summary>
    /// Sube un nivel (Normal → Oro → Platino).
    /// Devuelve el nuevo <see cref="INivel"/> o el máximo si ya está en Platino.
    /// </summary>
    public INivel Ascender()
    {
        int indiceActual = BuscarIndiceNivel(_nivel);
        int siguiente = Math.Min(indiceActual + 1, _escaleraDeNiveles.Length - 1);
        _nivel = _escaleraDeNiveles[siguiente];
        Console.WriteLine($"[Suscripcion] Nivel ascendido a: {_nivel.GetType().Name}");
        return _nivel;
    }

    /// <summary>
    /// Baja un nivel (Platino → Oro → Normal).
    /// Devuelve el nuevo <see cref="INivel"/> o el mínimo si ya está en Normal.
    /// </summary>
    public INivel Descender()
    {
        int indiceActual = BuscarIndiceNivel(_nivel);
        int anterior = Math.Max(indiceActual - 1, 0);
        _nivel = _escaleraDeNiveles[anterior];
        Console.WriteLine($"[Suscripcion] Nivel reducido a: {_nivel.GetType().Name}");
        return _nivel;
    }

    /// <summary>Verifica si la suscripción expiró y actualiza su estado.</summary>
    public void VerificarExpiracion()
    {
        if (DateTime.Now > _fechaFin && _estado is SuscripcionActiva)
        {
            _estado = new SuscripcionExpirada(this);
            Console.WriteLine("[Suscripcion] La suscripción ha expirado.");
        }
    }

    // ── Helpers ──────────────────────────────────────────────────────────────
    private static int BuscarIndiceNivel(INivel nivel)
    {
        for (int i = 0; i < _escaleraDeNiveles.Length; i++)
            if (_escaleraDeNiveles[i].GetType() == nivel.GetType()) return i;
        return 0;
    }

    // ── Properties ───────────────────────────────────────────────────────────
    public DateTime FechaInicio
    {
        get => _fechaInicio;
        set => _fechaInicio = value;
    }

    public DateTime FechaFin
    {
        get => _fechaFin;
        set => _fechaFin = value;
    }

    public IEstadoSuscripcion Estado
    {
        get => _estado;
        set => _estado = value ?? throw new ArgumentNullException(nameof(value));
    }

    public INivel Nivel
    {
        get => _nivel;
        set => _nivel = value ?? throw new ArgumentNullException(nameof(value));
    }

    public bool EstaActiva => _estado is SuscripcionActiva;
}