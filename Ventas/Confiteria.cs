using System;
using System.Collections.Generic;

namespace MultiplexCinema;

public class Confiteria : IVenta_Consumible
{
    private byte _id;
    private readonly ComboDirector _director;

    // Registro de factories disponibles por nombre de combo
    private readonly Dictionary<string, IConsumibleFactory> _lCombos;

    public Confiteria(byte id)
    {
        ArgumentOutOfRangeException.ThrowIfZero(id, nameof(id));
        _id = id;

        // Poblar con las tres factories estándar
        _lCombos = new Dictionary<string, IConsumibleFactory>(StringComparer.OrdinalIgnoreCase)
        {
            ["pequeño"] = new FactoryComboPequeno(),
            ["grande"] = new FactoryComboGrande(),
            ["familiar"] = new FactoryComboFamiliar()
        };

        // Director inicializado con la factory pequeña por defecto
        _director = new ComboDirector(_lCombos["pequeño"]);
    }

    // ── IVenta_Consumible ────────────────────────────────────────────────────

    public IConsumible Vender(int dinero, INivel descuento)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(dinero, nameof(dinero));
        ArgumentNullException.ThrowIfNull(descuento, nameof(descuento));

        // Ítem por defecto: crispeta pequeña con el nivel de precio correspondiente
        var factory = _lCombos["pequeño"];
        var crispeta = factory.CrearCrispeta("Crispeta pequeña", dinero);
        decimal precio = descuento.AplicarDescuentoComida(crispeta.ObtenerPrecio());

        Console.WriteLine($"[Confiteria {_id}] Vendido: {crispeta.ObtenerTipo()} — Precio final: {precio:C}");
        return crispeta;
    }
    public Combo Vender(int dinero, List<IConsumible> listaConsumibles, INivel descuento)
    {
        ArgumentNullException.ThrowIfNull(listaConsumibles, nameof(listaConsumibles));
        ArgumentNullException.ThrowIfNull(descuento, nameof(descuento));
        if (listaConsumibles.Count == 0)
            throw new ArgumentException("La lista de consumibles no puede estar vacía.", nameof(listaConsumibles));

        _director.Builder.NombrarCombo("Combo Personalizado");
        foreach (var item in listaConsumibles)
        {
            switch (item)
            {
                case Alimento a: _director.Builder.AgregarAlimento(a); break;
                case Accesorio ac: _director.Builder.AgregarAccesorio(ac); break;
                case Coleccionable c: _director.Builder.AgregarColeccionable(c); break;
                case Combo sub: _director.Builder.AgregarSubCombo(sub); break;
            }
        }
        _director.Builder.CalcularValor(descuento);
        Combo combo = _director.Builder.EntregarCombo();

        Console.WriteLine($"[Confiteria {_id}] {combo.ObtenerDetalle()}");
        return combo;
    }
    public Combo Vender(int dinero, INivel descuento, string nombre)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nombre, nameof(nombre));
        ArgumentNullException.ThrowIfNull(descuento, nameof(descuento));

        if (!_lCombos.TryGetValue(nombre, out IConsumibleFactory? factory))
            throw new KeyNotFoundException(
                $"No se encontró un combo con el nombre '{nombre}'. " +
                $"Disponibles: {string.Join(", ", _lCombos.Keys)}");

        Combo combo = nombre.ToLower() switch
        {
            "grande" => _director.ComboGrande(factory),
            "familiar" => _director.ComboFamiliar(factory),
            _ => _director.ComboPequeno(factory)
        };

        // Re-aplicar el descuento del nivel del espectador sobre el combo creado
        combo.RecalcularValor(descuento);
        Console.WriteLine($"[Confiteria {_id}] {combo.ObtenerDetalle()}");
        return combo;
    }

    // ── Propiedades ──────────────────────────────────────────────────────────

    public byte Id
    {
        get => _id;
        set => _id = value;
    }

    public ComboDirector Director => _director;

    public Dictionary<string, IConsumibleFactory> LCombos => _lCombos;
}