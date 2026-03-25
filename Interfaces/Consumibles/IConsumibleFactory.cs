namespace MultiplexCinema;

/// <summary>
/// IConsumibleFactory - Interface for consumable factory
/// </summary>
public interface IConsumibleFactory
{
    Crispeta CrearCrispeta(string descripcion, int valor);
    Bebida CrearBebida();
    Snack CrearSnack();
    Accesorio? CrearAccesorio();
    Coleccionable? CrearColeccionable();
}
