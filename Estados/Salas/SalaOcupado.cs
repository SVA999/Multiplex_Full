using System;

namespace MultiplexCinema;

/// <summary>
/// SalaOcupado - Occupied room state class
/// </summary>
public class SalaOcupado : IEstadoSala
{
    public void IniciarFuncion()
    {
        throw new InvalidOperationException("La sala ya está ocupada con una función en curso.");
    }

    public void FinalizarFuncion()
    {
        Console.WriteLine("Finalizando función. Cambiando la sala a estado disponible.");
    }

    public void EnviarMantenimiento()
    {
        throw new InvalidOperationException("No se puede enviar a mantenimiento mientras hay una función en curso.");
    }

    public void Reactivar()
    {
        throw new InvalidOperationException("La sala ya está activa y ocupada.");
    }
}
