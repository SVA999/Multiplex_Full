using System;
using System.Collections.Generic;
using MultiplexCinema;
using MultiplexFull.Core.Cine.Sala;
using MultiplexFull.Suscripciones;

namespace MultiplexFull.Core.Cine
{
    public class Funcion
    {
        private static int numCodigo=1;

        private string codigo;
        private DateTime hora;
        private Sala.Sala sala;
        private MultiplexCinema.Pelicula pelicula;
        private List<Espectador> l_espectadores;

        public Funcion(DateTime hora, Sala.Sala sala, MultiplexCinema.Pelicula pelicula)
        {
            //Asignar codigo consecutivo a la funcion
            codigo = $"000{numCodigo}";
            numCodigo++;

            Hora = hora;
            Sala = sala;
            Pelicula = pelicula;
            l_espectadores = new List<Espectador>();
        }

        public string Codigo { get => codigo;}
        public DateTime Hora { get => hora; set => hora = value; }
        public Sala.Sala Sala { get => sala; set => sala = value; }
        public int SalaId => Sala.Id;

        public MultiplexCinema.Pelicula Pelicula { get => pelicula; set => pelicula = value; }
        public string PeliculaNombre => Pelicula.Nombre;

        public List<Espectador> L_espectadores { get => l_espectadores; set => l_espectadores = value; }
    }
}
