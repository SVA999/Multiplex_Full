using System.Collections.Generic;
using System.Linq;

namespace MultiplexCinema;

public class Registro
{
    private readonly List<Venta> _ventas = [];

    // ── Escritura ────────────────────────────────────────────────────────────

    public void AgregarVenta(Venta venta)
    {
        ArgumentNullException.ThrowIfNull(venta, nameof(venta));
        _ventas.Add(venta);
    }

    // ── Consultas ────────────────────────────────────────────────────────────
    public List<IBoleta> ObtenerVentaBoletas()
        => _ventas.SelectMany(v => v.ObtenerVentaBoletas()).ToList();

    public List<IConsumible> ObtenerVentaConsumibles()
        => _ventas.SelectMany(v => v.ObtenerVentaConsumibles()).ToList();

    public int TotalVentas() => _ventas.Count;

    public decimal CalcularTotalGeneral() => _ventas.Sum(v => v.CalcularTotal());

    public List<Venta> ObtenerVentasPorEspectador(long espectadorId)
        => _ventas.Where(v => v.Espectador?.Id == espectadorId).ToList();

    public IReadOnlyList<Venta> Ventas => _ventas.AsReadOnly();
}