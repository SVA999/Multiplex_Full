namespace MultiplexCinema;

/// <summary>
/// FactoryComboFamiliar — Crea los consumibles tamaño familiar para el Combo Familiar.
/// Implementa <see cref="IConsumibleFactory"/>.
/// </summary>
public class FactoryComboFamiliar : IConsumibleFactory
{
    private const decimal PrecioCrispeta = 14_000m;
    private const decimal PrecioBebida = 7_000m;
    private const decimal PrecioSnack = 8_000m;
    private const decimal PrecioAccesorio = 4_500m;
    private const decimal PrecioColeccionable = 3_500m;

    public Crispeta CrearCrispeta(string descripcion, int valor)
    {
        string desc = string.IsNullOrWhiteSpace(descripcion) ? "Crispeta familiar mixta" : descripcion;
        decimal precio = valor > 0 ? valor : PrecioCrispeta;
        return new CrispetaFamiliar(desc, precio);
    }

    public Bebida CrearBebida()
        => new BebidaGrande("Jugo natural grande", PrecioBebida, "Hit");

    public Snack CrearSnack()
        => new SnackFamiliar("Nachos familiares con guacamole", PrecioSnack, "Yupi");

    public Accesorio? CrearAccesorio() => new Accesorio("Servilleta", 1500m);

    public Coleccionable? CrearColeccionable() => new Coleccionable("Vaso de Cine", 5000m, "Peliculas");
   }