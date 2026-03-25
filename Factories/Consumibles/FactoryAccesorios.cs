namespace MultiplexCinema;

/// <summary>
/// FactoryAccesorios — Crea accesorios de venta individual en confitería.
/// </summary>
public class FactoryAccesorios : FactoryExtras
{
    /// <summary>
    /// Crea un <see cref="Accesorio"/> estricto que requiere descripción.
    /// </summary>
    public override Accesorio CrearAccesorio(string descripcion, int valor)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(descripcion, nameof(descripcion));
        decimal precio = valor > 0 ? valor : 1_000m;
        return new Accesorio(descripcion, precio);
    }
}