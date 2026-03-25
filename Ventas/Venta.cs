using System;
using System.Collections.Generic;
using System.Linq;
using MultiplexFull.Suscripciones;

namespace MultiplexCinema;

public class Venta
{
    private ITipoVenta? _tipoVenta;
    private DateTime _fecha;
    private Espectador? _espectador;

    private readonly List<IBoleta> _boletas = [];
    private readonly List<IConsumible> _consumibles = [];

    // ── Constructor ──────────────────────────────────────────────────────────
    public Venta()
    {
        _fecha = DateTime.Now;
    }

    public Venta(Espectador espectador, ITipoVenta tipoVenta)
    {
        ArgumentNullException.ThrowIfNull(espectador, nameof(espectador));
        ArgumentNullException.ThrowIfNull(tipoVenta, nameof(tipoVenta));
        _espectador = espectador;
        _tipoVenta = tipoVenta;
        _fecha = DateTime.Now;
    }

    // ── Registro de ítems ────────────────────────────────────────────────────

    public void AgregarBoleta(IBoleta boleta)
    {
        ArgumentNullException.ThrowIfNull(boleta, nameof(boleta));
        _boletas.Add(boleta);
    }
    public void AgregarConsumible(IConsumible consumible)
    {
        ArgumentNullException.ThrowIfNull(consumible, nameof(consumible));
        _consumibles.Add(consumible);
    }

    // ── Consultas ────────────────────────────────────────────────────────────
    public List<IBoleta> ObtenerVentaBoletas() => [.. _boletas];
    public List<IConsumible> ObtenerVentaConsumibles() => [.. _consumibles];
    public decimal CalcularTotal()
    {
        decimal totalBoletas = _boletas.Sum(b => b.ObtenerValor());
        decimal totalConsumibles = _consumibles.Sum(c => c.ObtenerPrecio());
        return totalBoletas + totalConsumibles;
    }
    public string ObtenerResumen()
    {
        return $"[Venta] Fecha: {_fecha:g} | " +
               $"Espectador: {_espectador?.Nombre ?? "N/A"} | " +
               $"Tipo: {_tipoVenta?.ObtenerTipoVenta() ?? "N/A"} | " +
               $"Total: {CalcularTotal():C}";
    }
    public ITipoVenta? TipoVenta
    {
        get => _tipoVenta;
        set => _tipoVenta = value;
    }

    public DateTime Fecha
    {
        get => _fecha;
        set => _fecha = value;
    }

    public Espectador? Espectador
    {
        get => _espectador;
        set => _espectador = value;
    }
}