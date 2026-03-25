namespace MultiplexCinema;

/// <summary>
/// IEstadoSilla - Interface for seat state
/// </summary>
public interface IEstadoSilla
{
    bool PuedeSeleccionarse();
    string Liberar();
    IEstadoSilla Reservar(IEstadoSilla estado);
}
