using b_Multiplex.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using b_Multiplex.Interfaces;
using MultiplexCinema;

namespace b_Multiplex.Interfaces
{
	public interface IRegalos : IBeneficios
	{
		string obtener_regalo();
    }
}
