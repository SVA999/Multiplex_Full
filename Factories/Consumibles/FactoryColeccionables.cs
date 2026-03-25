namespace MultiplexCinema;

/// <summary>
/// FactoryColeccionables — Crea ítems coleccionables de venta individual en confitería.
/// </summary>
public class FactoryColeccionables : FactoryExtras
{

    public override Coleccionable CrearColeccionable(string descripcion, int valor, string coleccion)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(descripcion, nameof(descripcion));
        ArgumentException.ThrowIfNullOrWhiteSpace(coleccion, nameof(coleccion));
        decimal precio = valor > 0 ? valor : 1_500m;
        return new Coleccionable(descripcion, precio, coleccion);
    }
}