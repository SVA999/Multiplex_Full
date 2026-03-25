using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplexCinema
{
    public class Pelicula
    {
        public int Id { get; init; }
        public string Nombre { get; init; }
        public TimeSpan Duracion { get; init; }
        public IClasificacionPelicula Clasificacion { get; init; }
        public bool Estado { get; set; }
        public IGenero Genero { get; init; }

        public Pelicula(int id, string nombre, TimeSpan duracion, IClasificacionPelicula clasificacion, bool estado, IGenero genero)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nombre, nameof(nombre));
            ArgumentNullException.ThrowIfNull(clasificacion, nameof(clasificacion));
            ArgumentNullException.ThrowIfNull(genero, nameof(genero));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id, nameof(id));

            Id = id;
            Nombre = nombre;
            Duracion = duracion;
            Clasificacion = clasificacion;
            Estado = estado;
            Genero = genero;
        }
    }
}
