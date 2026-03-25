using System;

namespace MultiplexCinema;

/// <summary>
/// SalaDisponible - Available room state class
/// </summary>
public class SalaDisponible : IEstadoSala
{
    public void IniciarFuncion()
    {
        Console.WriteLine("Iniciando función. Cambiando la sala a estado ocupado.");
    }

    public void FinalizarFuncion()
    {
        throw new InvalidOperationException("La sala ya está disponible, no hay función en curso.");
    }

    public void EnviarMantenimiento()
    {
        Console.WriteLine("Enviando sala a mantenimiento.");
    }

    public void Reactivar()
    {
        throw new InvalidOperationException("La sala ya está activa y disponible.");
    }
}
