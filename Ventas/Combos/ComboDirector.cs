using System;

namespace MultiplexCinema;

/// <summary>
/// ComboDirector — Orquesta a <see cref="ComboBuilder"/> para crear combos
/// predefinidos (Pequeño, Grande, Familiar) o combos personalizados.
/// </summary>
public class ComboDirector
{
    private ComboBuilder _builder;

    // Nivel sin descuento para cuando no se pasa uno explícito
    private static readonly INivel _sinDescuento = new NivelNormal();

    public ComboDirector(IConsumibleFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        _builder = new ComboBuilder { FactoryConsumibles = factory };
    }

    // ── Combos predefinidos ──────────────────────────────────────────────────

    /// <summary>
    /// Combo Pequeño: crispeta pequeña + bebida pequeña.
    /// </summary>
    public Combo ComboPequeno(IConsumibleFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        _builder.FactoryConsumibles = factory;

        _builder.NombrarCombo("Combo Pequeño");
        _builder.AgregarAlimento(factory.CrearCrispeta("Crispeta Pequeña", 5000));
        _builder.AgregarAlimento(factory.CrearBebida());
        _builder.CalcularValor(_sinDescuento);
        return _builder.EntregarCombo();
    }

    /// <summary>
    /// Combo Grande: crispeta grande + bebida + snack.
    /// </summary>
    public Combo ComboGrande(IConsumibleFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        _builder.FactoryConsumibles = factory;

        _builder.NombrarCombo("Combo Grande");
        _builder.AgregarAlimento(factory.CrearCrispeta("Crispeta Grande", 8000));
        _builder.AgregarAlimento(factory.CrearBebida());
        _builder.AgregarAlimento(factory.CrearSnack());
        _builder.CalcularValor(_sinDescuento);
        return _builder.EntregarCombo();
    }

    /// <summary>
    /// Combo Familiar: crispeta familiar + 2 bebidas + snack + accesorio.
    /// </summary>
    public Combo ComboFamiliar(IConsumibleFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory, nameof(factory));
        _builder.FactoryConsumibles = factory;

        _builder.NombrarCombo("Combo Familiar");
        _builder.AgregarAlimento(factory.CrearCrispeta("Crispeta Familiar", 12000));
        _builder.AgregarAlimento(factory.CrearBebida());
        _builder.AgregarAlimento(factory.CrearBebida());   // segunda bebida
        _builder.AgregarAlimento(factory.CrearSnack());
        _builder.AgregarAccesorio(factory.CrearAccesorio());
        _builder.CalcularValor(_sinDescuento);
        return _builder.EntregarCombo();
    }

    /// <summary>
    /// Combo Personalizado — usa el builder directamente sin ítems predefinidos.
    /// El llamador debe configurar el builder antes de invocar este método.
    /// </summary>
    public Combo ComboPersonalizado()
    {
        if (_builder.Combo is null)
            throw new InvalidOperationException(
                "Debe configurar el builder (NombrarCombo + AgregarXxx + CalcularValor) antes de ComboPersonalizado().");
        return _builder.EntregarCombo();
    }

    // ── Propiedades ──────────────────────────────────────────────────────────
    public ComboBuilder Builder
    {
        get => _builder;
        set => _builder = value ?? throw new ArgumentNullException(nameof(value));
    }
}