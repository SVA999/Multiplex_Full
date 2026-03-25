using System;

namespace MultiplexCinema
{
    /// <summary>
    /// Configuración central de reglas de negocio para el Multiplex
    /// </summary>
    public static class ConfiguracionMultiplex
    {
        // Precios base por defecto (Reemplazan los literales hardcodeados)
        public const decimal PrecioSillaGeneral = 10000m;
        public const decimal PrecioSillaVip = 15000m;
        public const decimal PrecioSillaImax = 18000m; 
        public const decimal PrecioSillaImaxVip = 25000m;

        //Salas
        public static byte numeroSala = 1;
        private const byte numTotalSalas = 4;

        // Dimensiones dinámicas estándar
        public const int FilasDefault = 16;
        public const int ColumnasDefault = 10;
        
        // Dimensiones dinámicas Especiales
        public const int FilasVipDefault = 10;
        public const int ColumnasVipDefault = 8;
        
        public const int FilasImaxDefault = 20;
        public const int ColumnasImaxDefault = 15;

        //Combos
        /*
        public const int PrecioCombo1 = 25000;
        public const int PrecioCombo2 = 30000;
        public const int PrecioCombo3 = 40000;
        public const int PrecioCombo4 = 50000;
        public const int PrecioCombo5 = 60000;
        */
    }
}