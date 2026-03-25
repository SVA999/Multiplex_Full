using System;

namespace MultiplexCinema;

/// <summary>
/// SillaDisponible - Available seat state class
/// </summary>
public class SillaDisponible : IEstadoSilla
{
    public string Descripcion { get; init; } = "Disponible";

    public bool PuedeSeleccionarse() => true;

    public string Liberar()
    {
        return "La silla ya se encuentra disponible.";
    }

    public IEstadoSilla Reservar(IEstadoSilla estado)
    {
        ArgumentNullException.ThrowIfNull(estado, nameof(estado));
        return estado;
    }
}
