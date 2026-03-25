using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplexCinema
{
    public interface IClasificacionPelicula
    {
        public string ObtenerEtiqueta();
        public string Informacion();
        public  bool ValidarEdad(int edadEspectador);
    }
}
