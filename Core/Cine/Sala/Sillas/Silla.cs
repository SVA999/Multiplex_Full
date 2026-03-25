using System;
using MultiplexCinema;

namespace MultiplexFull.Core.Cine.Sala.Sillas;

/// <summary>
/// Silla - Abstract base class for seats
/// </summary>
public abstract class Silla
{
    private readonly string _fila = string.Empty;
    private readonly byte _numero;
    private readonly decimal _valor;

    public string Fila 
    { 
        get => _fila; 
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Fila));
            _fila = value;
        }
    }

    public byte Numero 
    { 
        get => _numero; 
        init
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(Numero));
            _numero = value;
        }
    }

    public decimal Valor 
    { 
        get => _valor; 
        init
        {
            ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(Valor));
            _valor = value;
        }
    }

    public IEstadoSilla Estado { get; protected set; }

    protected Silla(string fila, byte numero, IEstadoSilla estado, decimal valor)
    {
        ArgumentNullException.ThrowIfNull(estado, nameof(estado));
        
        Fila = fila;
        Numero = numero;
        Estado = estado;
        Valor = valor;
    }

    public virtual string CambiarEstado(IEstadoSilla nuevoEstado)
    {
        ArgumentNullException.ThrowIfNull(nuevoEstado, nameof(nuevoEstado));
        Estado = nuevoEstado;
        return $"El estado de la silla {Fila}{Numero} cambió a: {nuevoEstado.GetType().Name}";
    }
}
