using b_Multiplex.Interfaces;
using MultiplexFull.Core.Cine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace b_Multiplex.Clases
{

    public class Cadena
    {
        private string nombre;
        private List<Multiplex> l_multiplex;

        public Cadena(string nombre)
        {
            Nombre = nombre;
            LMultiplex = new List<Multiplex>();
        }

        public string Nombre
        {
            get => nombre;
            set => nombre = value;
        }   

        public List<Multiplex> LMultiplex
        {
            get => l_multiplex;
            set => l_multiplex = value ?? throw new ArgumentNullException(nameof(value));
        }

        public void AgregarMultiplex(Multiplex multiplex)
        {
            LMultiplex.Add(multiplex);
        }
        
        public int TotalEspectadores()
        {
            return LMultiplex.Sum(m => Multiplex.L_espectadores.Count);
        }

        public decimal RecaudacionTotal()
        {
            return LMultiplex.Sum(m => m.Registro.CalcularTotalGeneral());
        }

        public Multiplex MultiplexEstrella()
        {
            return LMultiplex.OrderByDescending(m => m.Registro.CalcularTotalGeneral()).FirstOrDefault();
        }

        public List<Funcion> FuncionesActivasGlobal()
        {
            return LMultiplex.SelectMany(m => m.L_funciones).ToList();
        }

        public void MostrarResumen()
        {
            Console.WriteLine($"Cadena {Nombre} - Total Multiplex: {LMultiplex.Count}");
        }
    }
}

