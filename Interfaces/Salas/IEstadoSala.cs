namespace MultiplexCinema;

/// <summary>
/// IEstadoSala - Interface for room/hall state
/// </summary>
public interface IEstadoSala
{
    void IniciarFuncion();
    void FinalizarFuncion();
    void EnviarMantenimiento();
    void Reactivar();
}
