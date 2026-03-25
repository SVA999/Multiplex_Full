using System;

namespace MultiplexCinema;

/// <summary>
/// ComboBuilder — Construye un <see cref="Combo"/> paso a paso (Builder Pattern).
/// El <see cref="ComboDirector"/> lo orquesta según el tipo de combo solicitado.
/// </summary>
public class ComboBuilder
{
    public Combo? Combo { get; private set; }
    public IConsumibleFactory? FactoryConsumibles { get; set; }
    public FactoryExtras? FactoryExtra { get; set; }

    // ── Inicialización ───────────────────────────────────────────────────────
    /// <summary>Crea un combo nuevo con la descripción indicada y valor inicial 0.</summary>
    public void NombrarCombo(string nombre)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nombre, nameof(nombre));
        Combo = new Combo(nombre, 0m);
    }

    // ── Agregar ítems ────────────────────────────────────────────────────────
    /// <summary>Agrega un <see cref="Alimento"/> (crispeta, bebida o snack) al combo.</summary>
    public void AgregarAlimento(Alimento alimento)
    {
        EnsureComboCreado();
        ArgumentNullException.ThrowIfNull(alimento, nameof(alimento));
        Combo!.AgregarConsumible(alimento);
    }

    /// <summary>Añade un sub-combo como ítem del combo principal.</summary>
    public void AgregarSubCombo(Combo combo)
    {
        EnsureComboCreado();
        ArgumentNullException.ThrowIfNull(combo, nameof(combo));
        Combo!.AgregarConsumible(combo);
    }

    /// <summary>Agrega un <see cref="Accesorio"/> (lentes 3D, etc.) al combo.</summary>
    public void AgregarAccesorio(Accesorio accesorio)
    {
        EnsureComboCreado();
        ArgumentNullException.ThrowIfNull(accesorio, nameof(accesorio));
        Combo!.AgregarConsumible(accesorio);
    }

    /// <summary>Agrega un <see cref="Coleccionable"/> al combo.</summary>
    public void AgregarColeccionable(Coleccionable coleccionable)
    {
        EnsureComboCreado();
        ArgumentNullException.ThrowIfNull(coleccionable, nameof(coleccionable));
        Combo!.AgregarConsumible(coleccionable);
    }

    // ── Valor ────────────────────────────────────────────────────────────────
    /// <summary>
    /// Recalcula el valor total del combo aplicando el descuento del nivel
    /// al precio de todos sus ítems acumulados.
    /// </summary>
    public void CalcularValor(INivel descuentos)
    {
        EnsureComboCreado();
        ArgumentNullException.ThrowIfNull(descuentos, nameof(descuentos));
        Combo!.RecalcularValor(descuentos);
    }

    // ── Entrega ──────────────────────────────────────────────────────────────
    /// <summary>Devuelve el combo construido y resetea el builder para reutilización.</summary>
    public Combo EntregarCombo()
    {
        EnsureComboCreado();
        Combo resultado = Combo!;
        Combo = null;   // reset para próxima construcción
        return resultado;
    }

    // ── Guard ────────────────────────────────────────────────────────────────
    private void EnsureComboCreado()
    {
        if (Combo is null)
            throw new InvalidOperationException("Debe llamar a NombrarCombo() antes de agregar ítems.");
    }
}