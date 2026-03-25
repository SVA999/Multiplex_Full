namespace MultiplexCinema;

/// <summary>
/// IBoleta - Interface base for ticket functionality
/// </summary>
public interface IBoleta
{
    string ObtenerInfo();
    INivel AplicarDescuento();
    decimal ObtenerValor();
}
