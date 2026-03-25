using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MultiplexCinema;
using MultiplexFull.Core.Cine.Sala;
using MultiplexFull.Core.Cine.Sala.TiposSala;
using MultiplexFull.Suscripciones;

namespace MultiplexFull.Core.Cine
{
    public class Multiplex 
    {

        // Salas
        private byte numTotalSalas = 1;

        public int Id { get; set; }
        private string nombre = string.Empty;
        private string direccion = string.Empty;
        
        public static List<Espectador> L_espectadores = new List<Espectador>();
        private List<Funcion> l_funciones = new List<Funcion>();
        private List<Sala.Sala> l_salas = new List<Sala.Sala>();
        private List<MultiplexCinema.Pelicula> l_peliculas = new List<MultiplexCinema.Pelicula>();
        private List<MultiplexCinema.Pelicula> l_peliculasActivas = new List<MultiplexCinema.Pelicula>();

        public byte NumTotalSalas { get => numTotalSalas; set => numTotalSalas = value; }
        
        // Asumiendo que aún se mantienen estos como propiedades base.
        public string RutaArchivoClientes { get; set; } = string.Empty;
        public string RutaArchivoFunciones { get; set; } = string.Empty;
        public string RutaArchivoPeliculas { get; set; } = string.Empty;
        public string RutaArchivoTaquilleros { get; set; } = string.Empty;

        public Multiplex(string nombre, string direccion, byte TotalSalas)
        {
            Nombre = nombre;
            Direccion = direccion;

            NumTotalSalas = TotalSalas;

            l_salas = new List<Sala.Sala>();
            
            // Instanciar salas dinámicamente con las estrategias modernas:
            for(byte i = 1; i <= NumTotalSalas; i++)
            {
                // Ejemplo de combinación de salas, la última es VIP
                ITipoSala tipo = (i == NumTotalSalas) ? new SalaVip(i) : new SalaGeneral(i);
                l_salas.Add(new Sala.Sala(i, tipo));
            }
            
            L_funciones = new List<Funcion>();
            L_peliculas = new List<MultiplexCinema.Pelicula>();
            L_peliculasActivas = new List<MultiplexCinema.Pelicula>();
        }

        public string Nombre 
        { 
            get => nombre; 
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Nombre nulo o vacio");
                nombre = value;
            } 
        }

        public string Direccion 
        { 
            get => direccion; 
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Direccion nula o vacia");
                direccion = value;
            }
        }

        public List<Sala.Sala> L_salas 
        { 
            get => l_salas;
            set
            {
                if (value.Count > NumTotalSalas) throw new ArgumentException("Numero maximo de salas excedido");
                l_salas = value;
            }
        }

        public List<Funcion> L_funciones { get => l_funciones; set => l_funciones = value; }
        public List<MultiplexCinema.Pelicula> L_peliculas { get => l_peliculas; set => l_peliculas = value; }
        public List<MultiplexCinema.Pelicula> L_peliculasActivas { get => l_peliculasActivas; set => l_peliculasActivas = value; }
        
        public MultiplexCinema.Registro Registro { get; } = new MultiplexCinema.Registro();

        public void AgregarPelicula(MultiplexCinema.Pelicula p)
        {
            L_peliculas.Add(p);
            L_peliculasActivas.Add(p);
        }

        public void RegistrarEspectador(Espectador e)
        {
            if(!L_espectadores.Any(x => x.Id == e.Id))
            {
                L_espectadores.Add(e);
                EscibirDBCliente(e);
            }
        }

        public Funcion ProgramarFuncion(MultiplexCinema.Pelicula peli, DateTime hora, int salaId)
        {
            var sala = L_salas.First(s => s.Id == salaId);
            var f = new Funcion(hora, sala, peli);
            L_funciones.Add(f);
            return f;
        }

        public MultiplexCinema.IBoleta ComprarBoleta(Espectador e, Funcion f, Sala.Sillas.Silla s)
        {
            var boleteria = new MultiplexCinema.Boleteria(1);
            var boleta = boleteria.VenderBoletaFuncion(e, s, (double)s.Valor, f);

            var venta = new MultiplexCinema.Venta();
            venta.Espectador = e;
            venta.TipoVenta = new TipoVentaMultiplex();
            venta.AgregarBoleta(boleta);
            Registro.AgregarVenta(venta);
            return boleta;
        }

        public MultiplexCinema.IConsumible ComprarCombo(Espectador e, string tipoCombo)
        {
            var confiteria = new MultiplexCinema.Confiteria(1);
            var n = e.Suscripcion?.Nivel;
            if (n == null) n = new MultiplexCinema.NivelNormal();
            var combo = confiteria.Vender(30000, n, tipoCombo);

            var venta = new MultiplexCinema.Venta();
            venta.Espectador = e;
            venta.TipoVenta = new TipoVentaMultiplex();
            venta.AgregarConsumible(combo);
            Registro.AgregarVenta(venta);

            return combo;
        }

        public void FinalizarFuncion(string codigo)
        {
            var f = L_funciones.FirstOrDefault(x => x.Codigo == codigo);
            if (f != null && f.Sala.Estado is MultiplexCinema.SalaOcupado)
            {
                f.Sala.CambiarEstado(new MultiplexCinema.SalaDisponible());
            }
        }

        public List<Espectador> CargarEspectadoresCSV()
        {
            var lista = new List<Espectador>();
            if (System.IO.File.Exists(RutaArchivoClientes))
            {
                foreach(var line in System.IO.File.ReadAllLines(RutaArchivoClientes))
                {
                    var parts = line.Split(';');
                    if(parts.Length >= 4 && long.TryParse(parts[0], out long id) && byte.TryParse(parts[2], out byte edad))
                    {
                        int telefono = int.TryParse(parts[3], out int t) && t > 0 ? t : 1;
                        var e = new Espectador(id, parts[1], telefono, edad, null);
                        if (!L_espectadores.Any(x => x.Id == id)) L_espectadores.Add(e);
                        lista.Add(e);
                    }
                }
            }
            return lista;
        }

        public void EscibirDBCliente(Espectador espectador)
        {
            try
            {
                if(string.IsNullOrEmpty(RutaArchivoClientes)) return;
                
                using StreamWriter escritorbd = new StreamWriter(RutaArchivoClientes, true, Encoding.UTF8);
                escritorbd.Write($"{espectador.Id};{espectador.Nombre};{espectador.Edad};{espectador.Telefono}\n");
            }
            catch (Exception e)
            {
                throw new Exception("Error al escribir DB Cliente: " + e.Message);
            }
        }
    }

    public class TipoVentaMultiplex : MultiplexCinema.ITipoVenta { public string ObtenerTipoVenta() => "Multiplex"; }
}
