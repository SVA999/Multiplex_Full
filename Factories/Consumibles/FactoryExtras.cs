namespace MultiplexCinema;


public abstract class FactoryExtras
{

    public virtual Accesorio CrearAccesorio(string descripcion, int valor)
    {
        string desc = string.IsNullOrWhiteSpace(descripcion) ? "Accesorio genérico" : descripcion;
        decimal precio = valor > 0 ? valor : 1_000m;
        return new Accesorio(desc, precio);
    }

    public virtual Coleccionable CrearColeccionable(string descripcion, int valor, string coleccion)
    {
        string desc = string.IsNullOrWhiteSpace(descripcion) ? "Coleccionable genérico" : descripcion;
        string col = string.IsNullOrWhiteSpace(coleccion) ? "Colección genérica" : coleccion;
        decimal precio = valor > 0 ? valor : 1_500m;
        return new Coleccionable(desc, precio, col);
    }
}