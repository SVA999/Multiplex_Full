namespace MultiplexCinema;

/// <summary>
/// FactoryComboPequeno — Crea los consumibles tamaño pequeño para el Combo Pequeño.
/// Implementa <see cref="IConsumibleFactory"/>.
/// </summary>
public class FactoryComboPequeno : IConsumibleFactory
{
    // Precios base del combo pequeño
    private const decimal PrecioCrispeta = 5_000m;
    private const decimal PrecioBebida = 4_000m;
    private const decimal PrecioSnack = 3_500m;
    private const decimal PrecioAccesorio = 2_000m;
    private const decimal PrecioColeccionable = 1_500m;

    public Crispeta CrearCrispeta(string descripcion, int valor)
    {
        string desc = string.IsNullOrWhiteSpace(descripcion) ? "Crispeta con sal" : descripcion;
        decimal precio = valor > 0 ? valor : PrecioCrispeta;
        return new CrispetaPequena(desc, precio);
    }

    public Bebida CrearBebida()
        => new BebidaPequena("Gaseosa pequeña", PrecioBebida, "Coca-Cola");

    public Snack CrearSnack()
        => new SnackSimple("Nachos pequeños", PrecioSnack, "Yupi");

    public Accesorio? CrearAccesorio() => null;

    public Coleccionable? CrearColeccionable() => null;
}