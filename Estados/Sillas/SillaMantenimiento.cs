using System;

namespace MultiplexCinema;

/// <summary>
/// SillaMantenimiento - Maintenance seat state class
/// </summary>
public class SillaMantenimiento : IEstadoSilla
{
    public string Descripcion { get; init; } = "En Mantenimiento";

    public bool PuedeSeleccionarse() => false;

    public string Liberar()
    {
        return "Silla reparada y liberada exitosamente.";
    }

    public IEstadoSilla Reservar(IEstadoSilla estado)
    {
        throw new InvalidOperationException("La silla se encuentra en mantenimiento y no puede ser reservada.");
    }
}
