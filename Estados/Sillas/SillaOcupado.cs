using System;

namespace MultiplexCinema;

/// <summary>
/// SillaOcupado - Occupied seat state class
/// </summary>
public class SillaOcupado : IEstadoSilla
{
    public string Descripcion { get; init; } = "Ocupado";

    public bool PuedeSeleccionarse() => false;

    public string Liberar()
    {
        return "Silla liberada exitosamente.";
    }

    public IEstadoSilla Reservar(IEstadoSilla estado)
    {
        throw new InvalidOperationException("La silla ya se encuentra ocupada y no puede ser reservada.");
    }
}
