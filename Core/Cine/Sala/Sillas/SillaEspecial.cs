using System;
using MultiplexCinema;

namespace MultiplexFull.Core.Cine.Sala.Sillas;

/// <summary>
/// SillaEspecial - Special seat class for accessibility or special requirements
/// </summary>
public class SillaEspecial : Silla
{
    private readonly string _requerimientoEspecial = string.Empty;

    public string RequerimientoEspecial
    {
        get => _requerimientoEspecial;
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(RequerimientoEspecial));
            _requerimientoEspecial = value;
        }
    }

    public SillaEspecial(string fila, byte numero, IEstadoSilla estado, decimal valor, string requerimientoEspecial)
        : base(fila, numero, estado, valor)
    {
        RequerimientoEspecial = requerimientoEspecial;
    }
}
