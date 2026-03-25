using System;

namespace MultiplexCinema;

/// <summary>
/// SalaMantenimiento - Room maintenance state class
/// </summary>
public class SalaMantenimiento : IEstadoSala
{
    public void IniciarFuncion()
    {
        throw new InvalidOperationException("La sala está en mantenimiento. No se puede iniciar función.");
    }

    public void FinalizarFuncion()
    {
        throw new InvalidOperationException("La sala está en mantenimiento. No hay función para finalizar.");
    }

    public void EnviarMantenimiento()
    {
        throw new InvalidOperationException("La sala ya se encuentra en mantenimiento.");
    }

    public void Reactivar()
    {
        Console.WriteLine("Mantenimiento finalizado. Sala reactivada a estado disponible.");
    }
}
