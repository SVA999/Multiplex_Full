namespace MultiplexCinema;

/// <summary>
/// IEstadoSuscripcion - Interface for subscription state
/// </summary>
public interface IEstadoSuscripcion
{
    void UsarBeneficios();
    void Activar();
    void Suspender();
}
