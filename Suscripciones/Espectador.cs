using MultiplexCinema;

namespace MultiplexFull.Suscripciones;

/// <summary>
/// Espectador - Audience/spectator class
/// </summary>
public class Espectador
{
    private long id;
    private byte edad;
    private string nombre;
    private int telefono;
    private string correo;
    private Suscripcion suscripcion;

    public Espectador(long id, string nombre, int telefono, byte edad, Suscripcion suscripcion)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));
        ArgumentException.ThrowIfNullOrWhiteSpace(nombre, nameof(nombre));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(telefono, nameof(telefono));
        ArgumentOutOfRangeException.ThrowIfZero(edad, nameof(edad));

        Id = id;
        Nombre = nombre;
        Telefono = telefono;
        Edad = edad;
        Suscripcion = suscripcion;
    }

    public long Id
    {
        get { return id; }
        set { id = value; }
    }

    public byte Edad
    {
        get { return edad; }
        set { edad = value; }
    }

    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }

    public int Telefono
    {
        get { return telefono; }
        set { telefono = value; }
    }

    public string Correo
    {
        get { return correo; }
        set { correo = value; }
    }

    public Suscripcion Suscripcion
    {
        get { return suscripcion; }
        set { suscripcion = value; }
    }
}