using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiplexCinema;

namespace MultiplexFull.Core.Cine
{
    public class Cartelera
    {
        private List<MultiplexCinema.Pelicula> l_peliculas;

        public Cartelera(List<MultiplexCinema.Pelicula> l_peliculas)
        {
            L_peliculas = l_peliculas;
        }

        public List<MultiplexCinema.Pelicula> L_peliculas { get => l_peliculas; set => l_peliculas = value; }

        //Metodo para manejar el evento
        internal void EventHandler() { }
    }
}
