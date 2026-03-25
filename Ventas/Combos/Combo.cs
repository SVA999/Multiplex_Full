using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiplexCinema;

/// <summary>
/// Combo — Producto final construido por <see cref="ComboBuilder"/>.
/// Agrega una lista de <see cref="IConsumible"/> y calcula su valor total.
/// </summary>
public class Combo : IConsumible
{
    private string _descripcion = string.Empty;
    private decimal _valor;
    private readonly List<IConsumible> _consumibles = [];

    // ── Constructor ──────────────────────────────────────────────────────────
    public Combo(string descripcion, decimal valor)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(descripcion, nameof(descripcion));
        ArgumentOutOfRangeException.ThrowIfNegative(valor, nameof(valor));
        _descripcion = descripcion;
        _valor = valor;
    }

    // ── IConsumible ──────────────────────────────────────────────────────────
    public string ObtenerTipo() => $"Combo [{_descripcion}]";

    public decimal ObtenerPrecio() => _valor;

    // ── Mutación ─────────────────────────────────────────────────────────────
    /// <summary>Agrega un consumible a la lista del combo.</summary>
    public void AgregarConsumible(IConsumible consumible)
    {
        ArgumentNullException.ThrowIfNull(consumible, nameof(consumible));
        _consumibles.Add(consumible);
    }

    /// <summary>
    /// Recalcula el valor del combo sumando los precios de todos los consumibles
    /// y aplicando el descuento del nivel recibido.
    /// </summary>
    public void RecalcularValor(INivel nivel)
    {
        ArgumentNullException.ThrowIfNull(nivel, nameof(nivel));
        decimal subtotal = _consumibles.Sum(c => c.ObtenerPrecio());
        _valor = nivel.AplicarDescuentoComida(subtotal);
    }

    /// <summary>Devuelve un resumen con todos los ítems del combo.</summary>
    public string ObtenerDetalle()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"=== {ObtenerTipo()} — Valor: {_valor:C} ===");
        foreach (var c in _consumibles)
            sb.AppendLine($"  · {c.ObtenerTipo()} — {c.ObtenerPrecio():C}");
        return sb.ToString();
    }

    // ── Properties ───────────────────────────────────────────────────────────
    public string Descripcion
    {
        get => _descripcion;
        set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Descripcion));
            _descripcion = value;
        }
    }

    public decimal Valor
    {
        get => _valor;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(Valor));
            _valor = value;
        }
    }

    public IReadOnlyList<IConsumible> Consumibles => _consumibles.AsReadOnly();
}