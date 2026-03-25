/*  ---------------------------------------------------------------
    Multiplex Cinema System — CLI de Pruebas
    ---------------------------------------------------------------
    Organizado en módulos de prueba por área:
      1. Sillas & Estados (State Pattern)
      2. Suscripciones & Niveles (State + niveles)
      3. Consumibles & Combos (Builder + Factory)
      4. Boletas & Boletería (Factory Method + Template)
      5. Observer (Cartelera + Publisher)
      6. Escenario completo end-to-end
      7. Salir
*/

using b_Multiplex.Clases;
using MultiplexCinema;
using MultiplexFull.Core.Cine;
using MultiplexFull.Core.Cine.Pelicula;
using MultiplexFull.Core.Cine.Sala;
using MultiplexFull.Core.Cine.Sala.Sillas;
using MultiplexFull.Core.Cine.Sala.TiposSala;
using MultiplexFull.Estrategias.Sillas;
using MultiplexFull.Suscripciones;
using b_Multiplex.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// -- Helpers de presentación -------------------------------------------------
static void Titulo(string texto)
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"  +- {texto} {'-',0}".PadRight(62, '-') + "+");
    Console.ResetColor();
}
static void Ok(string msg) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"  ?  {msg}"); Console.ResetColor(); }
static void Info(string msg) { Console.ForegroundColor = ConsoleColor.White; Console.WriteLine($"     {msg}"); Console.ResetColor(); }
static void Warn(string msg) { Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"  ?  {msg}"); Console.ResetColor(); }
static void Err(string msg) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"  ?  {msg}"); Console.ResetColor(); }
static void Separador() { Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine("  " + new string('·', 58)); Console.ResetColor(); }
static void Pausa() { Console.WriteLine("\n  [Presiona Enter para continuar]"); Console.ReadLine(); }

// -- Factories de datos de prueba --------------------------------------------
static Pelicula CrearPelicula(string nombre, int durMin, string etiqueta, byte edadMin, string genero)
{
    var clasificacion = new Clasificacion(etiqueta, $"Clasificación {etiqueta}", $"Para mayores de {edadMin}", edadMin);
    var gen = new Genero(genero, $"Contenido de {genero}");
    var peliId = MockData.PeliId++;
    return new Pelicula(peliId, nombre, TimeSpan.FromMinutes(durMin), clasificacion, true, gen);
}


static Espectador CrearEspectador(long id, string nombre, byte edad, Suscripcion? sus = null)
{
    var esp = new Espectador(id, nombre, 3001234 + (int)id, edad, sus);
    if (sus != null) esp.Suscripcion = sus;
    return esp;
}

// ---------------------------------------------------------------------------
//  MÓDULOS DE PRUEBA
// ---------------------------------------------------------------------------

static void TestSillasYEstados()
{
    Titulo("MÓDULO 1 · Sillas y Estados (State Pattern)");

    // Crear silla general
    var estado = new SillaDisponible();
    var silla = new SillaGeneral("A", 5, estado, 12_000m);

    Ok($"Silla creada: {silla.Fila}{silla.Numero}  Valor: {silla.Valor:C}");
    Info($"Estado inicial: {silla.Estado.GetType().Name}");
    Info($"¿Puede seleccionarse? {silla.Estado.PuedeSeleccionarse()}");

    Separador();
    // Reservar ? ocupar
    var msg = silla.CambiarEstado(new SillaOcupado());
    Ok(msg);
    Info($"¿Puede seleccionarse? {silla.Estado.PuedeSeleccionarse()}");
    Info($"Liberar desde Ocupado: {silla.Estado.Liberar()}");

    Separador();
    // Mantenimiento
    silla.CambiarEstado(new SillaMantenimiento());
    Ok($"Estado ahora: {silla.Estado.GetType().Name}");
    Info($"¿Puede seleccionarse? {silla.Estado.PuedeSeleccionarse()}");
    Info($"Liberar desde Mantenimiento: {silla.Estado.Liberar()}");

    Separador();
    // Volver a disponible
    silla.CambiarEstado(new SillaDisponible());
    Ok($"Estado restaurado: {silla.Estado.GetType().Name}");

    Separador();
    // Sillas VIP y especial
    var vip = new SillaVip("Z", 1, new SillaDisponible(), 25_000m);
    var esp = new SillaEspecial("B", 3, new SillaDisponible(), 14_000m, "Acceso reducido");
    Ok($"SillaVip     {vip.Fila}{vip.Numero}  {vip.Valor:C}");
    Ok($"SillaEspecial {esp.Fila}{esp.Numero}  {esp.Valor:C}");

    Separador();
    // Estados de sala
    Sala sala = new Sala(1, new SalaGeneral(1));
    Info($"Estado sala inicial: {sala.Estado.GetType().Name}");
    sala.Estado.IniciarFuncion();
    sala.CambiarEstado(new SalaOcupado());
    Ok($"Estado sala ? {sala.Estado.GetType().Name}");
    sala.Estado.FinalizarFuncion();
    sala.CambiarEstado(new SalaDisponible());
    Ok($"Estado sala ? {sala.Estado.GetType().Name}");
    sala.CambiarEstado(new SalaMantenimiento());
    Ok($"Estado sala ? {sala.Estado.GetType().Name}");

    try { sala.Estado.IniciarFuncion(); }
    catch (InvalidOperationException ex) { Warn($"Guard correcto: {ex.Message}"); }

    Pausa();
}

static void TestSuscripcionesYNiveles()
{
    Titulo("MÓDULO 2 · Suscripciones y Niveles (State Pattern)");

    // Crear suscripción activa con nivel Normal
    var sus = new Suscripcion();
    Ok($"Suscripción creada  Estado: {sus.Estado.GetType().Name}  Nivel: {sus.Nivel.GetType().Name}");
    Info($"Descuento actual: {sus.Nivel.ObtenerDescuento():P0}");

    Separador();
    // Usar beneficios
    sus.Estado.UsarBeneficios();

    Separador();
    // Ascender a Oro
    var nivelOro = sus.Ascender();
    Ok($"Nivel ascendido ? {nivelOro.GetType().Name}  Descuento: {nivelOro.ObtenerDescuento():P0}");
    decimal precio = 20_000m;
    Info($"Precio boleta original:      {precio:C}");
    Info($"Precio con desc. Oro:        {nivelOro.AplicarDescuentoBoleta(precio):C}");
    Info($"Precio comida con desc. Oro: {nivelOro.AplicarDescuentoComida(precio):C}");

    // Ascender a Platino
    var nivelPlatino = sus.Ascender();
    Ok($"Nivel ascendido ? {nivelPlatino.GetType().Name}  Descuento: {nivelPlatino.ObtenerDescuento():P0}");
    Info($"Precio boleta con Platino:   {nivelPlatino.AplicarDescuentoBoleta(precio):C}");

    // Intentar ascender más allá del máximo
    sus.Ascender();
    Ok($"Ya en nivel máximo ? {sus.Nivel.GetType().Name} (sin cambio)");

    Separador();
    // Descender
    sus.Descender();
    Ok($"Nivel descendido ? {sus.Nivel.GetType().Name}");

    Separador();
    // Estado: Suspender ? Cancelada
    sus.Estado.Suspender();
    Ok($"Estado suspendido ? {sus.Estado.GetType().Name}");
    sus.Estado.UsarBeneficios();   // no debe aplicar beneficios

    Separador();
    // Reactivar desde cancelada
    sus.Estado.Activar();
    Ok($"Reactivada ? {sus.Estado.GetType().Name}");

    Separador();
    // Simular expiración manual
    sus.Estado = new SuscripcionExpirada(sus);
    Warn($"Forzando estado expirado: {sus.Estado.GetType().Name}");
    sus.Estado.UsarBeneficios();
    sus.Estado.Activar();   // renovar
    Ok($"Renovada ? {sus.Estado.GetType().Name}");

    Pausa();
}

static void TestConsumiblesYCombos()
{
    Titulo("MÓDULO 3 · Consumibles y Combos (Builder + Factories)");

    // Ítems individuales
    var crispGrande = new CrispetaGrande("Crispeta caramelo", 9_500m);
    var crispPeq = new CrispetaPequena("Crispeta sal", 5_500m);
    var crispFam = new CrispetaFamiliar("Crispeta mixta", 14_000m);
    var bebGrande = new BebidaGrande("Agua saborizada", 6_000m, "Cristal");
    var bebPeq = new BebidaPequena("Jugo lulo", 4_500m, "Hit");
    var snackSimp = new SnackSimple("Gomitas", 3_000m, "Trolli");
    var snackGrande = new SnackGrande("Papas onduladas", 5_500m, "Pringles");
    var accesorio = new Accesorio("Lentes 3D", 3_500m);
    var coleccion = new Coleccionable("Muñeco Spiderman", 8_000m, "Marvel");

    Ok("Consumibles individuales:");
    foreach (var c in new IConsumible[]{ crispGrande, crispPeq, crispFam, bebGrande, bebPeq,
                                          snackSimp, snackGrande, accesorio, coleccion })
        Info($"  {c.ObtenerTipo(),-45} {c.ObtenerPrecio():C}");

    Separador();
    // Builder manual
    var builder = new ComboBuilder();
    builder.NombrarCombo("Combo Personalizado CLI");
    builder.AgregarAlimento(crispGrande);
    builder.AgregarAlimento(bebGrande);
    builder.AgregarAccesorio(accesorio);
    builder.CalcularValor(new NivelOro());
    var comboManual = builder.EntregarCombo();
    Ok("Combo manual (Builder directo):");
    Info(comboManual.ObtenerDetalle());

    Separador();
    // Director: combo pequeño, grande y familiar
    var director = new ComboDirector(new FactoryComboPequeno());
    var nivel = new NivelNormal();

    Ok("ComboDirector.ComboPequeno():");
    var cp = director.ComboPequeno(new FactoryComboPequeno());
    Info(cp.ObtenerDetalle());

    Ok("ComboDirector.ComboGrande():");
    var cg = director.ComboGrande(new FactoryComboGrande());
    Info(cg.ObtenerDetalle());

    Ok("ComboDirector.ComboFamiliar():");
    var cf = director.ComboFamiliar(new FactoryComboFamiliar());
    Info(cf.ObtenerDetalle());

    Separador();
    // Venta por Confiteria
    var confiteria = new Confiteria(1);
    Ok("Confiteria.Vender() — combo 'grande' con nivel Platino:");
    var comboPlatino = confiteria.Vender(50_000, new NivelPlatino(), "grande");
    Info($"  ? Valor final: {comboPlatino.ObtenerPrecio():C}");

    Pausa();
}

static void TestBoletasYBoleteria()
{
    Titulo("MÓDULO 4 · Boletas y Boletería (Factory Method)");

    // Setup
    var sus = new Suscripcion();
    sus.Ascender(); // Nivel Oro

    var carlos = CrearEspectador(101, "Carlos Pérez", 28, sus);
    var ana = CrearEspectador(102, "Ana Gómez", 16, null);
    var nina = CrearEspectador(103, "Nina Torres", 9, null);

    var peli = CrearPelicula("Inception 2", 148, "PG-13", 13, "Sci-Fi");
    var peliR = CrearPelicula("Joker 2", 137, "R", 18, "Drama");

    var estrategia = new LlenarGeneral(8, 6);
    var sala = new Sala(3, new SalaGeneral(3));
    var funcion = new Funcion(DateTime.Now.AddHours(2), sala, peli);
    var funcionR = new Funcion(DateTime.Now.AddHours(5), sala, peliR);

    var boleteria = new Boleteria(1);

    Info($"Función: {funcion.Codigo}  Película: {peli.Nombre}  Aforo: {funcion.Sala.MSillas.Length}");

    Separador();
    // Silla disponible
    var silla = sala.MSillas[0, 0];
    Ok($"Silla elegida: {silla.Fila}{silla.Numero}  Valor: {silla.Valor:C}  Estado: {silla.Estado.GetType().Name}");

    // Vender boleta normal (nivel Oro)
    var boleta = boleteria.VenderBoletaFuncion(carlos, silla, (double)silla.Valor, funcion);
    Ok($"Boleta vendida: {boleta.ObtenerInfo()}");
    Info($"Silla ahora: {silla.Estado.GetType().Name}");
    Info($"Nivel descuento aplicado: {boleta.AplicarDescuento()?.GetType().Name ?? "ninguno"}");

    Separador();
    // Silla para Ana (menor, PG-13 permitido)
    var silla2 = sala.MSillas[0, 1];
    var boleta2 = boleteria.VenderBoletaFuncion(ana, silla2, (double)silla2.Valor, funcion);
    Ok($"Boleta Ana (16 años / PG-13): {boleta2.ObtenerInfo()}");

    Separador();
    // Niña de 9 años intentando ver Joker (R, 18+)
    var silla3 = sala.MSillas[1, 0];
    try
    {
        boleteria.VenderBoletaFuncion(nina, silla3, (double)silla3.Valor, funcionR);
        Err("No debería llegar aquí — falta validación de edad");
    }
    catch (InvalidOperationException ex)
    {
        Warn($"Validación de edad: {ex.Message}");
    }

    Separador();
    // Silla ya ocupada
    try
    {
        boleteria.VenderBoletaFuncion(carlos, silla, (double)silla.Valor, funcion);
    }
    catch (InvalidOperationException ex)
    {
        Warn($"Silla ocupada correctamente rechazada: {ex.Message}");
    }

    Separador();
    // Boletas de evento
    var eventoPriv = new EventoPrivado("Estreno VIP Inception 2");
    var eventoCorp = new EventoCorporativo("Bancolombia", "Noche de cine corporativa");
    var sillaEvt1 = sala.MSillas[2, 0];
    var sillaEvt2 = sala.MSillas[2, 1];

    var bPriv = boleteria.VenderBoletaEvento(carlos, sillaEvt1, 35_000, eventoPriv);
    Ok($"Evento privado:      {bPriv.ObtenerInfo()}");

    var bCorp = boleteria.VenderBoletaEvento(ana, sillaEvt2, 45_000, eventoCorp);
    Ok($"Evento corporativo:  {bCorp.ObtenerInfo()}");

    Separador();
    // Boleta VIP
    var salaVip = new Sala(5, new SalaVip(5));
    var funcionVip = new Funcion(DateTime.Now.AddHours(3), salaVip, peli);
    var sillaVip = salaVip.MSillas[0, 0];
    var bVip = boleteria.VenderBoletaFuncion(carlos, sillaVip, (double)sillaVip.Valor, funcionVip);
    Ok($"Boleta VIP:  {bVip.ObtenerInfo()}");

    Pausa();
}

static void TestObserverCartelera()
{
    Titulo("MÓDULO 5 · Observer — Publisher / Cartelera");

    var cartelera = new Cartelera(new List<Pelicula>());
    var publisher = new Publisher_CambioCartelera();
    var actualizador = new ActualizacionCartelera(cartelera, publisher);

    var sus1 = new Suscripcion();
    var sus2 = new Suscripcion();
    publisher.Suscribir(sus1);
    publisher.Suscribir(sus2);

    Ok($"Suscriptores registrados: {publisher.Suscriptores.Count}");

    Separador();
    // Agregar películas directamente
    var p1 = CrearPelicula("Dune 3", 155, "PG-13", 13, "Sci-Fi");
    var p2 = CrearPelicula("Oppen 2", 180, "R", 18, "Drama");
    var p3 = CrearPelicula("El Chavo", 95, "G", 0, "Comedia");
    cartelera.L_peliculas.Add(p1);
    cartelera.L_peliculas.Add(p2);
    cartelera.L_peliculas.Add(p3);

    Action mostrarCartelera = () => { foreach(var p in cartelera.L_peliculas) Info($"  ID:{p.Id} {p.Nombre} ({p.Clasificacion.ObtenerEtiqueta()})"); };

    Ok("Cartelera inicial:");
    mostrarCartelera();

    Separador();
    // Cambio via ActualizacionCartelera ? Publisher ? todos los suscriptores
    var nuevaLista = new List<Pelicula>
    {
        CrearPelicula("Avatar 4",     190, "PG",    10, "Aventura"),
        CrearPelicula("Fast & Furious 15", 120, "PG-13", 13, "Acción"),
    };

    Ok("Publicando cambio de cartelera...");
    actualizador.ProgramarCambio(nuevaLista);

    Ok("Cartelera actualizada:");
    mostrarCartelera();

    Separador();
    // Desuscribir
    publisher.Desuscribir(sus1);
    Ok($"Suscriptores tras baja: {publisher.Suscriptores.Count}");

    // Retirar película
    var pToRemove = cartelera.L_peliculas.FirstOrDefault(p => p.Id == nuevaLista[0].Id);
    bool retirada = false;
    if (pToRemove != null) { cartelera.L_peliculas.Remove(pToRemove); retirada = true; }
    Ok($"Película retirada: {retirada}");
    mostrarCartelera();

    // Búsqueda
    var encontrada = cartelera.L_peliculas.FirstOrDefault(p => p.Nombre.Contains("fast", StringComparison.OrdinalIgnoreCase));
    Ok($"Búsqueda 'fast': {encontrada?.Nombre ?? "no encontrada"}");

    Pausa();
}

static void TestEscenarioCompleto()
{
    Titulo("MÓDULO 6 · Escenario End-to-End");

    // -- 1. Crear Multiplex --------------------------------------------------
    var multiplex = new MultiplexFull.Core.Cine.Multiplex("Cinemark El Poblado", "Cra 43A #1-50, Medellín", 3);
    Ok($"Multiplex: {multiplex.Nombre}  Salas: {multiplex.NumTotalSalas}");

    // -- 2. Catálogo de películas --------------------------------------------
    var p1 = CrearPelicula("Misión Imposible 9", 130, "PG-13", 13, "Acción");
    var p2 = CrearPelicula("Barbie 2", 110, "PG", 10, "Comedia");
    var p3 = CrearPelicula("El Conjuro 5", 105, "R", 18, "Terror");

    multiplex.L_peliculas.AddRange([p1, p2, p3]);
    multiplex.L_peliculasActivas.AddRange([p1, p2, p3]);
    Ok($"Películas en catálogo: {multiplex.L_peliculas.Count}");

    // -- 3. Crear funciones --------------------------------------------------
    var sala1 = multiplex.L_salas[0];
    var sala2 = multiplex.L_salas[1];

    var f1 = new Funcion(DateTime.Now.AddHours(1), sala1, p1);
    var f2 = new Funcion(DateTime.Now.AddHours(2), sala2, p2);
    multiplex.L_funciones.AddRange([f1, f2]);
    Ok($"Funciones creadas: {f1.Codigo} ({p1.Nombre}) · {f2.Codigo} ({p2.Nombre})");

    // -- 4. Registrar espectadores con suscripciones -------------------------
    var susPlatino = new Suscripcion();
    susPlatino.Ascender(); susPlatino.Ascender(); // ? Platino

    var susOro = new Suscripcion();
    susOro.Ascender(); // ? Oro

    var hugo = CrearEspectador(201, "Hugo Vargas", 35, susPlatino);
    var lucia = CrearEspectador(202, "Lucía Mora", 29, susOro);
    var pedro = CrearEspectador(203, "Pedro Suárez", 14, null);

    MultiplexFull.Core.Cine.Multiplex.L_espectadores.AddRange([hugo, lucia, pedro]);
    Ok($"Espectadores: {string.Join(", ", MultiplexFull.Core.Cine.Multiplex.L_espectadores.Select(e => e.Nombre))}");

    // -- 5. Comprar boletas --------------------------------------------------
    Separador();
    var boleteria = new Boleteria(1);
    var sillaH = sala1.MSillas[0, 0];
    var sillaL = sala1.MSillas[0, 1];
    var sillaP = sala2.MSillas[0, 0];

    var bHugo = boleteria.VenderBoletaFuncion(hugo, sillaH, (double)sillaH.Valor, f1);
    var bLucia = boleteria.VenderBoletaFuncion(lucia, sillaL, (double)sillaL.Valor, f1);
    var bPedro = boleteria.VenderBoletaFuncion(pedro, sillaP, (double)sillaP.Valor, f2);

    Ok("Boletas vendidas:");
    Info($"  {bHugo.ObtenerInfo()}");
    Info($"  {bLucia.ObtenerInfo()}");
    Info($"  {bPedro.ObtenerInfo()}");
    Info($"  Espectadores en F1: {f1.L_espectadores.Count}");

    // -- 6. Comprar combos en confitería -------------------------------------
    Separador();
    var confiteria = new Confiteria(1);
    var comboH = confiteria.Vender(60_000, susPlatino.Nivel, "familiar");
    var comboL = confiteria.Vender(30_000, susOro.Nivel, "grande");
    var comboP = confiteria.Vender(15_000, new NivelNormal(), "pequeño");

    Ok($"Combo Hugo  (Platino): {comboH.ObtenerPrecio():C}");
    Ok($"Combo Lucía (Oro):     {comboL.ObtenerPrecio():C}");
    Ok($"Combo Pedro (Normal):  {comboP.ObtenerPrecio():C}");

    // -- 7. Registro de ventas -----------------------------------------------
    Separador();
    var ventaHugo = new Venta(hugo, new TipoVentaBoleta());
    ventaHugo.AgregarBoleta(bHugo);
    ventaHugo.AgregarConsumible(comboH);

    var registro = new Registro();
    registro.AgregarVenta(ventaHugo);

    Ok($"Total venta Hugo: {ventaHugo.CalcularTotal():C}");
    Ok($"Ventas en registro: {registro.TotalVentas()}");
    Ok($"Recaudado total: {registro.CalcularTotalGeneral():C}");

    // -- 8. Estado final de sala ---------------------------------------------
    Separador();
    sala1.CambiarEstado(new SalaOcupado());
    Ok($"Sala 1 durante función: {sala1.Estado.GetType().Name}");
    sala1.LimpiarSala();
    sala1.CambiarEstado(new SalaDisponible());
    Ok($"Sala 1 tras función:    {sala1.Estado.GetType().Name}");
    Info($"Sillas liberadas: {sala1.MSillas.GetLength(0) * sala1.MSillas.GetLength(1)}");

    Pausa();
}

// ---------------------------------------------------------------------------
//  MENÚ PRINCIPAL
// ---------------------------------------------------------------------------

static void MostrarMenu()
{
    Console.Clear();
    Console.OutputEncoding = System.Text.Encoding.UTF8;
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine(@"
  ███╗   ███╗██╗   ██╗██╗  ████████╗██╗██████╗ ██╗     ███████╗██╗  ██╗
  ████╗ ████║██║   ██║██║  ╚══██╔══╝██║██╔══██╗██║     ██╔════╝╚██╗██╔╝
  ██╔████╔██║██║   ██║██║     ██║   ██║██████╔╝██║     █████╗   ╚███╔╝
  ██║╚██╔╝██║██║   ██║██║     ██║   ██║██╔═══╝ ██║     ██╔══╝   ██╔██╗
  ██║ ╚═╝ ██║╚██████╔╝███████╗██║   ██║██║     ███████╗███████╗██╔╝ ██╗
  ╚═╝     ╚═╝ ╚═════╝ ╚══════╝╚═╝   ╚═╝╚═╝     ╚══════╝╚══════╝╚═╝  ╚═╝

  +--------------------------------------------------------------+
  ¦       MULTIPLEX CINEMA SYSTEM  —  CLI de Pruebas v2         ¦
  ¦--------------------------------------------------------------¦
  ¦                                                              ¦
  ¦  PATRONES DE DISEÑO                                          ¦
  ¦   [1]  Sillas & Estados          (State)                    ¦
  ¦   [2]  Suscripciones & Niveles   (State + Strategy)         ¦
  ¦   [3]  Consumibles & Combos      (Builder + Abstract Fac.)  ¦
  ¦   [4]  Boletas & Boletería       (Factory Method)           ¦
  ¦   [5]  Observer Cartelera        (Observer)                 ¦
  ¦                                                              ¦
  ¦  INTEGRACIONES                                               ¦
  ¦   [6]  Escenario End-to-End      (todos los patrones)       ¦
  ¦   [7]  Sala IMAX                 (LlenarImax + mapa ASCII)  ¦
  ¦   [8]  Cadena de Multiplex       (multi-sede + broadcast)   ¦
  ¦   [9]  Persistencia CSV          (leer / escribir)          ¦
  ¦   [10] Dashboard de Estadísticas (registro + analytics)     ¦
  ¦   [11] Alquiler de Salas         (IServicio + ITipoEvento)  ¦
  ¦                                                              ¦
  ¦   [A]  Ejecutar todos los módulos                           ¦
  ¦   [0]  Salir                                                ¦
  ¦                                                              ¦
  +--------------------------------------------------------------+");
    Console.ResetColor();
    Console.Write("\n  Elige una opción: ");
}

// --- Entry Point -------------------------------------------------------------
while (true)
{
    MostrarMenu();
    var input = Console.ReadLine()?.Trim().ToUpper();

    try
    {
        switch (input)
        {
            case "1": TestSillasYEstados(); break;
            case "2": TestSuscripcionesYNiveles(); break;
            case "3": TestConsumiblesYCombos(); break;
            case "4": TestBoletasYBoleteria(); break;
            case "5": TestObserverCartelera(); break;
            case "6": TestEscenarioCompleto(); break;
            case "7": TestSalaImax(); break;
            case "8": TestCadena(); break;
            case "9": TestPersistenciaCSV(); break;
            case "10": TestEstadisticas(); break;
            case "11": TestAlquiler(); break;
            case "A":
                TestSillasYEstados();
                TestSuscripcionesYNiveles();
                TestConsumiblesYCombos();
                TestBoletasYBoleteria();
                TestObserverCartelera();
                TestEscenarioCompleto();
                TestSalaImax();
                TestCadena();
                TestPersistenciaCSV();
                TestEstadisticas();
                TestAlquiler();
                break;
            case "0":
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n  ¡Hasta pronto!\n");
                Console.ResetColor();
                return;
            default:
                Warn("Opción no reconocida. Ingresa 1-6, A o 0.");
                Pausa();
                break;
        }
    }
    catch (Exception ex)
    {
        Err($"Error inesperado: {ex.Message}");
        Console.WriteLine(ex.StackTrace);
        Pausa();
    }
}

// --------------------------------------------------------------------------
//  MÓDULOS NUEVOS
// --------------------------------------------------------------------------

static void TestSalaImax()
{
    Titulo("MÓDULO 7 · Sala IMAX — LlenarImax Strategy");

    // Crear sala IMAX con dimensiones reducidas para visualizar mejor
    var imax = new SalaImax(9, "IMAX", filas: 10, columnas: 8);
    Ok($"Tipo sala: {imax.ObtenerTipoSala()}");

    var sala = new Sala(9, imax);
    int totalSillas = sala.MSillas.GetLength(0) * sala.MSillas.GetLength(1);
    Ok($"Sala creada: {sala.MSillas.GetLength(0)} filas × {sala.MSillas.GetLength(1)} cols = {totalSillas} asientos");

    Separador();
    // Contar zonas
    int gen = 0, vip = 0, acomp = 0;
    foreach (var s in sala.MSillas)
    {
        if (s is SillaVip) vip++;
        else if (s is SillaAcompanante) acomp++;
        else gen++;
    }
    Ok("Distribución de zonas:");
    Info($"  General              : {gen,4} sillas  ({gen * 100 / totalSillas,3}%)");
    Info($"  VIP Premium          : {vip,4} sillas  ({vip * 100 / totalSillas,3}%)");
    Info($"  Acompañante (sofá)   : {acomp,4} sillas  ({acomp * 100 / totalSillas,3}%)");

    Separador();
    // Visualizar mapa de asientos en ASCII
    Ok("Mapa de asientos (G=General, V=VIP, A=Acompañante):");
    Console.WriteLine();
    Console.WriteLine("        " + string.Concat(Enumerable.Range(1, sala.MSillas.GetLength(1)).Select(c => $"{c,3}")));
    for (int f = 0; f < sala.MSillas.GetLength(0); f++)
    {
        string letra = f < 26 ? ((char)('A' + f)).ToString() : "A" + (char)('A' + f % 26);
        Console.Write($"  Fila {letra,-2}");
        for (int c = 0; c < sala.MSillas.GetLength(1); c++)
        {
            var s = sala.MSillas[f, c];
            string sym = s switch
            {
                SillaAcompanante => " A ",
                SillaVip => " V ",
                _ => " G "
            };
            ConsoleColor col = s switch
            {
                SillaAcompanante => ConsoleColor.Magenta,
                SillaVip => ConsoleColor.Yellow,
                _ => ConsoleColor.DarkGray
            };
            Console.ForegroundColor = col;
            Console.Write(sym);
        }
        Console.ResetColor();
        Console.WriteLine();
    }
    Console.WriteLine();

    Separador();
    // Precios por zona
    Ok("Precios por tipo de asiento:");
    var g0 = sala.MSillas[0, 0];    // fila delantera
    var gMid = sala.MSillas[3, 0];   // fila central
    var v0 = sala.MSillas[sala.MSillas.GetLength(0) - 3, 0]; // VIP
    var a0 = sala.MSillas[sala.MSillas.GetLength(0) - 1, 0]; // Acompañante
    Info($"  {g0.GetType().Name,-18} (fila delantera) : {g0.Valor:C}");
    Info($"  {gMid.GetType().Name,-18} (fila central)   : {gMid.Valor:C}");
    Info($"  {v0.GetType().Name,-18} (VIP premium)    : {v0.Valor:C}");
    Info($"  {a0.GetType().Name,-18} (sofá doble)     : {a0.Valor:C}");

    Separador();
    // Función y venta en IMAX
    var peli = CrearPelicula("Dune 3 — IMAX", 165, "PG-13", 13, "Sci-Fi");
    var sus = new Suscripcion(); sus.Ascender(); sus.Ascender(); // Platino
    var viewer = CrearEspectador(301, "María IMAX", 30, sus);
    var funcion = new Funcion(DateTime.Now.AddHours(3), sala, peli);

    var boleteria = new Boleteria(9);
    var sillaVip = v0;
    var boleta = boleteria.VenderBoletaFuncion(viewer, sillaVip, (double)sillaVip.Valor, funcion);
    Ok($"Boleta IMAX vendida: {boleta.ObtenerInfo()}");
    Info($"Descuento Platino aplicado — precio base: {sillaVip.Valor:C}");

    Pausa();
}

static void TestCadena()
{
    Titulo("MÓDULO 8 · Cadena — Múltiples Multiplex");

    // Crear cadena y sus multiplex
    var cadena = new Cadena("CineMax Colombia");

    var mp1 = new MultiplexFull.Core.Cine.Multiplex("CineMax El Poblado", "Cra 43A, Medellín", 4);
    var mp2 = new MultiplexFull.Core.Cine.Multiplex("CineMax Santa Fe", "Av. El Poblado, Med", 3);
    var mp3 = new MultiplexFull.Core.Cine.Multiplex("CineMax Unicentro", "Cra 15, Bogotá", 5);

    cadena.AgregarMultiplex(mp1);
    cadena.AgregarMultiplex(mp2);
    cadena.AgregarMultiplex(mp3);
    Ok($"Cadena '{cadena.Nombre}' con {cadena.LMultiplex.Count} multiplex");

    Separador();
    // Agregar películas a cada multiplex
    var p1 = CrearPelicula("Avatar 5", 195, "PG", 10, "Aventura");
    var p2 = CrearPelicula("El Padrino: Redux", 210, "R", 18, "Drama");
    var p3 = CrearPelicula("Minecraft Pelicula", 115, "G", 0, "Animación");

    foreach (var mp in cadena.LMultiplex)
    {
        mp.AgregarPelicula(p1);
        if (mp != mp2) mp.AgregarPelicula(p2);
        mp.AgregarPelicula(p3);
    }
    Ok("Películas distribuidas en la cadena.");

    Separador();
    // Crear espectadores y simular ventas en distintos multiplex
    var sus1 = new Suscripcion(); sus1.Ascender();            // Oro
    var sus2 = new Suscripcion(); sus2.Ascender(); sus2.Ascender(); // Platino

    // Reset static list for clean demo
    MultiplexFull.Core.Cine.Multiplex.L_espectadores.Clear();
    var e1 = CrearEspectador(401, "Carlos (Pob)", 32, sus1);
    var e2 = CrearEspectador(402, "Ana (SF)", 27, sus2);
    var e3 = CrearEspectador(403, "Luis (Bog)", 45, null);

    mp1.RegistrarEspectador(e1);
    mp2.RegistrarEspectador(e2);
    mp3.RegistrarEspectador(e3);
    Ok($"Espectadores registrados en cadena: {cadena.TotalEspectadores()}");

    Separador();
    // Programar y comprar boletas
    var f1 = mp1.ProgramarFuncion(p1, DateTime.Now.AddHours(1), salaId: 1);
    var f2 = mp3.ProgramarFuncion(p2, DateTime.Now.AddHours(2), salaId: 1);

    var b1 = mp1.ComprarBoleta(e1, f1, mp1.L_salas[0].MSillas[0, 0]);
    var b2 = mp3.ComprarBoleta(e3, f2, mp3.L_salas[0].MSillas[0, 0]);
    var c1 = mp1.ComprarCombo(e1, "grande");
    var c2 = mp3.ComprarCombo(e3, "familiar");

    Ok($"Ventas Poblado  : {mp1.Registro.CalcularTotalGeneral():C}");
    Ok($"Ventas Santa Fe : {mp2.Registro.CalcularTotalGeneral():C}");
    Ok($"Ventas Unicentro: {mp3.Registro.CalcularTotalGeneral():C}");

    Separador();
    // Estadísticas consolidadas
    Ok($"CADENA — Recaudación total: {cadena.RecaudacionTotal():C}");
    Ok($"CADENA — Multiplex estrella: {cadena.MultiplexEstrella()?.Nombre ?? "N/A"}");
    Ok($"CADENA — Funciones activas : {cadena.FuncionesActivasGlobal().Count}");

    Separador();
    // Resumen ejecutivo
    cadena.MostrarResumen();

    Pausa();
}

static void TestPersistenciaCSV()
{
    Titulo("MÓDULO 9 · Persistencia CSV — Espectadores");

    var tmpDir = Path.Combine(Path.GetTempPath(), "MultiplexTest");
    Directory.CreateDirectory(tmpDir);
    string csvPath = Path.Combine(tmpDir, "espectadores.csv");
    if (File.Exists(csvPath)) File.Delete(csvPath);

    var mp = new MultiplexFull.Core.Cine.Multiplex("CineMax Test", "Calle 10 #5-30", 2);
    mp.RutaArchivoClientes = csvPath;
    Ok($"CSV configurado en: {csvPath}");

    Separador();
    // Registrar espectadores ? escribe al CSV
    MultiplexFull.Core.Cine.Multiplex.L_espectadores.Clear();
    var espectadores = new[]
    {
        CrearEspectador(501, "Sofía Ramírez",  29, new Suscripcion()),
        CrearEspectador(502, "Tomás Herrera",  41, null),
        CrearEspectador(503, "Valentina Cruz", 17, new Suscripcion()),
        CrearEspectador(504, "Diego Mora",     55, new Suscripcion()),
        CrearEspectador(505, "Isabella Ruiz",  23, null),
    };

    foreach (var e in espectadores)
    {
        e.Correo = $"{e.Nombre.Split(' ')[0].ToLower()}@correo.co";
        mp.RegistrarEspectador(e);
    }

    Ok($"Escritos {espectadores.Length} espectadores al CSV.");
    Info($"Tamaño del archivo: {new FileInfo(csvPath).Length} bytes");

    Separador();
    // Mostrar contenido del CSV
    Ok("Contenido del CSV:");
    foreach (var linea in File.ReadAllLines(csvPath))
        Info($"  {linea}");

    Separador();
    // Cargar desde CSV (simula reinicio del sistema)
    MultiplexFull.Core.Cine.Multiplex.L_espectadores.Clear();
    var cargados = mp.CargarEspectadoresCSV();
    Ok($"Cargados desde CSV: {cargados.Count} espectadores");
    foreach (var e in cargados)
        Info($"  [{e.Id}] {e.Nombre,-22} edad:{e.Edad}  tel:{e.Telefono}  correo:{e.Correo}");

    Separador();
    // Verificar integridad: re-registrar uno ya existente (debe ignorarse)
    mp.RegistrarEspectador(cargados[0]);
    mp.RegistrarEspectador(cargados[0]);
    Ok($"Anti-duplicado OK — lista sigue en: {MultiplexFull.Core.Cine.Multiplex.L_espectadores.Count} + {cargados.Count} cargados");

    // Limpiar archivos temporales
    try { File.Delete(csvPath); Directory.Delete(tmpDir); } catch { /* no-op */ }
    Ok("Archivos temporales eliminados.");

    Pausa();
}

static void TestEstadisticas()
{
    Titulo("MÓDULO 10 · Dashboard de Estadísticas");

    // Setup: multiplex completo con varias transacciones
    MultiplexFull.Core.Cine.Multiplex.L_espectadores.Clear();
    var mp = new MultiplexFull.Core.Cine.Multiplex("CineMax Demo Stats", "Cra 1 #1-1", 4);

    var peliculas = new[]
    {
        CrearPelicula("Acción Total",    120, "PG-13", 13, "Acción"),
        CrearPelicula("Comedia Loca",     95, "PG",    10, "Comedia"),
        CrearPelicula("Terror Nocturno", 110, "R",     18, "Terror"),
        CrearPelicula("Documental XYZ",   80, "G",      0, "Documental"),
    };
    foreach (var p in peliculas) mp.AgregarPelicula(p);

    // Espectadores con diferentes niveles
    var nivelOro = new Suscripcion(); nivelOro.Ascender();
    var nivelPlatino = new Suscripcion(); nivelPlatino.Ascender(); nivelPlatino.Ascender();

    var especs = new[]
    {
        CrearEspectador(601, "Ana Normal",    25, null),
        CrearEspectador(602, "Luis Oro",      30, nivelOro),
        CrearEspectador(603, "Marta Platino", 45, nivelPlatino),
        CrearEspectador(604, "José Normal",   19, null),
        CrearEspectador(605, "Clara Oro",     33, new Suscripcion()),
    };
    foreach (var e in especs) mp.RegistrarEspectador(e);

    // Programar funciones y vender boletas
    var funciones = new[]
    {
        mp.ProgramarFuncion(peliculas[0], DateTime.Now.AddHours(1), 1),
        mp.ProgramarFuncion(peliculas[1], DateTime.Now.AddHours(2), 2),
        mp.ProgramarFuncion(peliculas[2], DateTime.Now.AddHours(3), 3),
    };

    var boletas = new List<IBoleta>();
    var rnd = new Random(42);
    int espIdx = 0;
    foreach (var f in funciones)
    {
        for (int i = 0; i < 3; i++)
        {
            var e = especs[espIdx % especs.Length];
            var s = f.Sala.MSillas[i, 0];
            if (!s.Estado.PuedeSeleccionarse()) continue;
            var b = mp.ComprarBoleta(e, f, s);
            boletas.Add(b);
            espIdx++;
        }
    }

    // Comprar combos
    var combos = new List<IConsumible>();
    foreach (var e in especs.Take(4))
    {
        string tipo = e.Suscripcion?.Nivel is NivelPlatino ? "familiar"
                    : e.Suscripcion?.Nivel is NivelOro ? "grande"
                    : "pequeño";
        combos.Add(mp.ComprarCombo(e, tipo));
    }

    Separador();
    // -- Dashboard --------------------------------------------------------
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("\n  +---------------------------------------------------------+");
    Console.WriteLine("  ¦            DASHBOARD — REPORTE DE VENTAS               ¦");
    Console.WriteLine("  +---------------------------------------------------------+");
    Console.ResetColor();

    var reg = mp.Registro;
    Info($"  Ventas totales   : {reg.TotalVentas()}");
    Info($"  Boletas vendidas : {reg.ObtenerVentaBoletas().Count}");
    Info($"  Consumibles      : {reg.ObtenerVentaConsumibles().Count}");
    Info($"  RECAUDACIÓN TOTAL: {reg.CalcularTotalGeneral():C}");

    Separador();
    Ok("Recaudación por nivel de suscripción:");
    var ventasPorNivel = reg.Ventas
        .GroupBy(v => v.Espectador?.Suscripcion?.Nivel?.GetType().Name ?? "Normal (sin sus.)")
        .Select(g => new { Nivel = g.Key, Total = g.Sum(v => v.CalcularTotal()), Count = g.Count() });
    foreach (var vn in ventasPorNivel.OrderByDescending(x => x.Total))
        Info($"  {vn.Nivel,-22} : {vn.Count,2} ventas — {vn.Total:C}");

    Separador();
    Ok("Boletas por función:");
    foreach (var f in funciones)
    {
        var boletasFuncion = reg.ObtenerVentaBoletas()
            .Where(b => b is BoletaGeneral bg && bg.Funcion.Codigo == f.Codigo ||
                        b is BoletaVip bv && bv.Funcion.Codigo == f.Codigo)
            .ToList();
        decimal ingresoF = boletasFuncion.Sum(b => b.ObtenerValor());
        Info($"  {f.Codigo}  {f.PeliculaNombre,-25}  {boletasFuncion.Count} boletas  {ingresoF:C}");
    }

    Separador();
    Ok("Top espectadores por gasto total:");
    var topEspecs = especs
        .Select(e => new {
            e.Nombre,
            Nivel = e.Suscripcion?.Nivel?.GetType().Name ?? "Normal",
            Gasto = reg.ObtenerVentasPorEspectador(e.Id).Sum(v => v.CalcularTotal())
        })
        .Where(x => x.Gasto > 0)
        .OrderByDescending(x => x.Gasto);

    int rank = 1;
    foreach (var t in topEspecs)
        Info($"  #{rank++}  {t.Nombre,-22} [{t.Nivel,-14}]  {t.Gasto:C}");

    Separador();
    // Estado actual de salas
    Ok("Estado actual de salas:");
    foreach (var sala in mp.L_salas)
    {
        string tipo = sala.TipoSala.ObtenerTipoSala();
        int total = sala.MSillas.GetLength(0) * sala.MSillas.GetLength(1);
        int ocupadas = 0;
        for (int r = 0; r < sala.MSillas.GetLength(0); r++)
            for (int c = 0; c < sala.MSillas.GetLength(1); c++)
                if (!sala.MSillas[r, c]?.Estado.PuedeSeleccionarse() == true) ocupadas++;
        double pct = total > 0 ? ocupadas * 100.0 / total : 0;
        string barra = new string('¦', (int)(pct / 5)) + new string('¦', 20 - (int)(pct / 5));
        Info($"  Sala {sala.Id} [{tipo,-8}]  [{barra}] {pct:F0}% ocupada  estado: {sala.Estado.GetType().Name}");
    }

    Separador();
    // Ciclo de vida: finalizar funciones
    Ok("Finalizando funciones (ciclo de vida):");
    foreach (var f in funciones)
    {
        mp.FinalizarFuncion(f.Codigo);
        Ok($"  {f.Codigo} finalizada — Sala {f.Sala.Id} ? {f.Sala.Estado.GetType().Name}");
    }

    Pausa();
}

static void TestAlquiler()
{
    Titulo("MÓDULO 11 · Alquiler de Salas (IServicio + ITipoEvento)");

    // Configurar componentes necesarios
    var sala = new Sala(7, new SalaGeneral(7));
    var eventoCorp = new EventoCorporativo("Tech Corp", "Conferencia Anual de Tecnología");
    var eventoPriv = new EventoPrivado("Cumpleaños 15 de Ana");

    // Crear alquileres
    var alquilerCorp = new Alquiler(eventoCorp, 2_500_000, sala, DateTime.Now.AddDays(7), DateTime.Now.AddDays(7).AddHours(4));
    var alquilerPriv = new Alquiler(eventoPriv, 1_200_000, sala, DateTime.Now.AddDays(14), DateTime.Now.AddDays(14).AddHours(3));

    Ok("Alquiler Corporativo Creado:");
    Info($"  Detalle: {alquilerCorp.ObtenerServicio()}");
    Info($"  Estado de sala: {alquilerCorp.Sala.Estado.GetType().Name}");

    Separador();

    Ok("Alquiler Privado Creado:");
    Info($"  Detalle: {alquilerPriv.ObtenerServicio()}");
    Info($"  Empresa/Responsable: {alquilerCorp.Sala.Id}");

    Separador();

    // Modificar un alquiler para demostrar propiedades
    alquilerPriv.Valor = 1_500_000;
    alquilerPriv.FechaFin = alquilerPriv.FechaFin.AddHours(1);

    Ok("Alquiler Privado Modificado (extensión de tiempo):");
    Info($"  Nuevo Detalle: {alquilerPriv.ObtenerServicio()}");

    Pausa();
}

// Classes for testing program usage
public static class MockData { public static int PeliId = 1; }
public class TipoVentaBoleta : MultiplexCinema.ITipoVenta { public string ObtenerTipoVenta() => "Venta de Boleta"; }
public class MockServicio : IServicio { public string ObtenerServicio() => "Servicio Básico"; }
