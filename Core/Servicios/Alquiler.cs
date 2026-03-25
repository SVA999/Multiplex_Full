using b_Multiplex.Interfaces;
using MultiplexFull.Core.Cine.Sala;
using System;

namespace MultiplexCinema;

public class Alquiler : IServicio
{
    private ITipoEvento evento;
    private int valor;
    private Sala sala;
    private DateTime fechaInicio;
    private DateTime fechaFin;

    public Alquiler(ITipoEvento evento, int valor, Sala sala, DateTime fechaInicio, DateTime fechaFin)
    {
        this.evento = evento;
        this.valor = valor;
        this.sala = sala;
        this.fechaInicio = fechaInicio;
        this.fechaFin = fechaFin;
    }

    public string ObtenerServicio()
    {
        return $"Alquiler de sala {sala.Id} para evento: {evento.ObtenerTipo()} | Desde: {fechaInicio:g} Hasta: {fechaFin:g} | Valor: {valor:C}";
    }

    public ITipoEvento Evento
    {
        get { return evento; }
        set { evento = value; }
    }

    public int Valor
    {
        get { return valor; }
        set { valor = value; }
    }

    public Sala Sala
    {
        get { return sala; }
        set { sala = value; }
    }

    public DateTime FechaInicio
    {
        get { return fechaInicio; }
        set { fechaInicio = value; }
    }

    public DateTime FechaFin
    {
        get { return fechaFin; }
        set { fechaFin = value; }
    }
}
