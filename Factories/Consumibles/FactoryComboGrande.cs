namespace MultiplexCinema;

/// <summary>
/// FactoryComboGrande — Crea los consumibles tamaño grande para el Combo Grande.
/// Implementa <see cref="IConsumibleFactory"/>.
/// </summary>
public class FactoryComboGrande : IConsumibleFactory
{
    private const decimal PrecioCrispeta = 9_000m;
    private const decimal PrecioBebida = 6_000m;
    private const decimal PrecioSnack = 5_500m;
    private const decimal PrecioAccesorio = 3_000m;
    private const decimal PrecioColeccionable = 2_500m;

    public Crispeta CrearCrispeta(string descripcion, int valor)
    {
        string desc = string.IsNullOrWhiteSpace(descripcion) ? "Crispeta con mantequilla" : descripcion;
        decimal precio = valor > 0 ? valor : PrecioCrispeta;
        return new CrispetaGrande(desc, precio);
    }

    public Bebida CrearBebida()
        => new BebidaGrande("Gaseosa grande", PrecioBebida, "Pepsi");

    public Snack CrearSnack()
        => new SnackGrande("Nachos grandes con salsa", PrecioSnack, "Yupi");

    public Accesorio? CrearAccesorio() => null;

    public Coleccionable? CrearColeccionable() => null;
}