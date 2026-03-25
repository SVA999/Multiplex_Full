namespace MultiplexCinema;

/// <summary>
/// IVenta_Consumible - Interface for consumable sales
/// </summary>
public interface IVenta_Consumible
{
    IConsumible Vender(int dinero, INivel descuento);

    Combo Vender(int dinero, List<IConsumible> listaConsumibles, INivel descuento);

    Combo Vender(int dinero, INivel descuento, string nombre);
}
