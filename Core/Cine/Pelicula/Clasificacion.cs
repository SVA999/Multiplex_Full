using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiplexCinema;

namespace MultiplexFull.Core.Cine.Pelicula
{
    public class Clasificacion : IClasificacionPelicula
    {
        private string etiqueta;
        private string descripcion;
        private string informacion;
        private byte edad_minima;

        public Clasificacion(string etiqueta, string descripcion, string informacion, byte edad_minima)
        {
            Etiqueta = etiqueta;
            Descripcion = descripcion;
            Informacion = informacion;
            Edad_minima = edad_minima;
        }

        public string Etiqueta { get => etiqueta; set => etiqueta = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Informacion { get => informacion; set => informacion = value; }
        public byte Edad_minima { get => edad_minima; set => edad_minima = value; }

        public string ObtenerEtiqueta()
        {
            return Etiqueta;
        }

        string IClasificacionPelicula.Informacion()
        {
            return $"[{etiqueta}] {descripcion} — Edad mínima: {(edad_minima == 0 ? "Todas las edades" : edad_minima + " años")}";
        }

        public bool ValidarEdad(int edadEspectador)
        {
            return edadEspectador >= edad_minima;
        }
    }
}
