using System;
using MultiplexFull.Core.Cine;
using MultiplexFull.Core.Cine.Sala.Sillas;
using MultiplexFull.Suscripciones;

namespace MultiplexCinema;

public class Boleteria : IVenta_Boleta
{
    private byte _id;

    public Boleteria(byte id)
    {
        ArgumentOutOfRangeException.ThrowIfZero(id, nameof(id));
        _id = id;
    }

    // Mantiene compatibilidad con el constructor vacío original
    public Boleteria() { }

    // ── IVenta_Boleta ────────────────────────────────────────────────────────
    public IBoleta VenderBoleta(Espectador espectador, Silla silla, decimal dinero)
    {
        ArgumentNullException.ThrowIfNull(espectador, nameof(espectador));
        ArgumentNullException.ThrowIfNull(silla, nameof(silla));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dinero, nameof(dinero));

        if (!silla.Estado.PuedeSeleccionarse())
            throw new InvalidOperationException(
                $"La silla {silla.Fila}{silla.Numero} no está disponible.");

        // Para venta con función usa la sobrecarga VenderBoletaFuncion
        long id = FactoryBoleta.GenerarIdPublico();
        decimal precio = AplicarDescuento(espectador, dinero);
        return new BoletaGeneral(id, espectador, null!, silla, precio);
    }

    // ── Sobrecargas especializadas ───────────────────────────────────────────

    public IBoleta VenderBoletaFuncion(Espectador espectador, Silla silla, double precio, Funcion funcion)
    {
        ArgumentNullException.ThrowIfNull(funcion, nameof(funcion));

        // Validar edad según clasificación de la película
        if (!funcion.Pelicula.Clasificacion.ValidarEdad(espectador.Edad))
            throw new InvalidOperationException(
                $"El espectador {espectador.Nombre} no cumple la edad mínima para ver " +
                $"'{funcion.Pelicula.Nombre}' ({funcion.Pelicula.Clasificacion.ObtenerEtiqueta()}).");

        var factory = new FactoryBFuncion(funcion);
        IBoleta boleta = factory.CrearBoleta(espectador, silla, precio, funcion);

        // Agregar espectador a la función
        funcion.L_espectadores.Add(espectador);
        return boleta;
    }

    public IBoleta VenderBoletaEvento(Espectador espectador, Silla silla, double precio, ITipoEvento evento)
    {
        ArgumentNullException.ThrowIfNull(evento, nameof(evento));
        var factory = new FactoryBEvento(evento);
        return factory.CrearBoleta(espectador, silla, precio, evento);
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private static decimal AplicarDescuento(Espectador espectador, decimal precio)
    {
        INivel? nivel = espectador.Suscripcion?.Nivel;
        decimal final = nivel is not null ? nivel.AplicarDescuentoBoleta(precio) : precio;
        return final;
    }

    // ── Propiedades ──────────────────────────────────────────────────────────
    public byte Id
    {
        get => _id;
        set => _id = value;
    }

    // Propiedad de factory mantenida por compatibilidad
    public FactoryBoleta? FBoletas { get; set; }
}