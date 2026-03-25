# Multiplex Cinema System — CLI de Pruebas v2

## Requisitos Previos
- **.NET 10 SDK** o superior (Descargar desde [dot.net/download](https://dot.net/download)).
- Terminal/Consola que soporte codificación UTF-8 para visualizar correctamente el mapa de asientos y los caracteres especiales.

---

## 🚀 Compilar y Ejecutar

El proyecto cuenta con un CLI extenso y robusto que se ejecuta directamente desde la línea de comandos usando las herramientas de .NET, o directamente desde un IDE.

### Ejecución en Visual Studio Community (Interfaz Gráfica)
Si prefieres utilizar el entorno de Visual Studio, el proceso es muy sencillo:
1. Abre el archivo de la solución (`.sln`) o el del proyecto (`.csproj`) directamente en **Visual Studio Community**.
2. Asegúrate de que el proyecto funcione de consola sea el **Proyecto de Inicio** (clic derecho sobre el proyecto en el Explorador de Soluciones > *Establecer como proyecto de inicio*).
3. Presiona la tecla **F5** (ejecutar con depuración) o **Ctrl + F5** (sin depuración), o haz clic en el botón verde de **Iniciar** en la barra superior superior.
   > **Nota:** Se abrirá una ventana de consola automáticamente mostrando la UI del menú principal interactivo.

### Ejecución Estándar (Interactiva por CLI)
1. Abre tu terminal y navega al directorio del proyecto `MultiplexFull`.
2. Compila el ensamblado completo para garantizar que todas las bibliotecas anexas y dependencias estén correctas:
   ```bash
   dotnet build
   ```
3. Inicia la aplicación interactiva de consola:
   ```bash
   dotnet run
   ```
   > **Nota:** Al ejecutarlo, se mostrará en pantalla la UI del menú principal interactivo. Escribe el número del módulo que deseas probar (por ejemplo, `6`), presiona la tecla `A` para lanzar la suite de pruebas completa en cadena, o `0` para salir adecuadamente.

### Automatización (Scripts de Ejecución Rápida)
Para agilizar el desarrollo, puedes crear pequeños scripts automatizados en la raíz de tu proyecto para asegurar una compilación y ejecución rápida en una única pasada:

**En Windows (`run.bat`):**
```bat
@echo off
echo Compilando Multiplex Cinema System...
dotnet build
if %errorlevel% neq 0 (
    echo Error durante la compilacion. Revisa los errores.
    exit /b %errorlevel%
)
echo.
echo Ejecutando...
dotnet run
pause
```

---

## 🧩 Detalle de Módulos de Prueba

El CLI organiza las pruebas dividiéndolas en áreas de interés técnico (reglas de dominio y patrones de diseño implementados). A continuación se explica para qué funciona cada módulo y qué se pone a prueba internamente:

### Patrones de Diseño Centrales
| # | Módulo | ¿Qué prueba? (Propósito Técnico y Funcional) |
|---|--------|----------------------------------------------|
| **1** | **Sillas & Estados** | El patrón *State*. Verifica las transiciones seguras de estado de una silla (Disponible ↔ Ocupado ↔ Mantenimiento). Muestra la correcta respuesta polimórfica en sillas General, VIP y Especial. |
| **2** | **Suscripciones & Niveles** | El patrón *State + Strategy*. Comprueba que la suscripción de un espectador pueda activarse, suspenderse o expirar; así como su capacidad de otorgar descuentos dinámicos y escalables en compras (Normal → Oro → Platino). |
| **3** | **Consumibles & Combos** | El patrón *Builder + Abstract Factory*. Prueba el empaquetado de productos individuales (crispetas, gaseosas, etc.) en un *Combo* usando la flexibilidad manual del `ComboBuilder` y métodos preensamblados de un `FactoryDirector`. |
| **4** | **Boletas & Boletería** | El patrón *Factory Method*. Evalúa la venta de boletas individuales, en áreas VIP o eventos especiales. Comprueba estrictamente las barreras restrictivas de negocio: validaciones de edad requerida (PG-13, R, G) y rechazo en sillas que ya figuren como 'Ocupadas'. |
| **5** | **Observer Cartelera** | El patrón *Observer*. Muestra cómo los agregados externos (ej. la clase `Cartelera`) y el gestor de eventos (`Publisher_CambioCartelera`) actualizan en cadena masiva a clientes o suscriptores cuando se quitan o añaden películas. |

### Integraciones y Flujos de Sistema
| # | Módulo | ¿Qué prueba? (Propósito Técnico y Funcional) |
|---|--------|----------------------------------------------|
| **6** | **Escenario End-to-End** | Une todos los patrones funcionales. Simula el flujo completo del sistema como si fuera en tiempo real: construir catálogos de películas, estructurar salas multiplex, vender combos y boletos mixtos al público, y la limpieza post-función. |
| **7** | **Sala IMAX** | Patrón *Strategy*. Despliega de forma visual un mapa ASCII en consola mostrando 3 diferentes tipos estructurales (General, VIP Premium y Sofá). Valida la estrategia de cómo se llena espacialmente una estructura cinematográfica. |
| **8** | **Cadena de Multiplex** | Escalabilidad Multi-sistemas. Vincula simultáneamente múltiples multiplex físicos en una macro `Cadena`. Prueba cómo se cruza la información, permitiendo centralizar métricas consolidadas (Ventas Totales, Mejor Cine de la Franquicia). |
| **9** | **Persistencia CSV** | Interacción con Base de Datos / Sistema de Archivos. Escribe de forma real en un directorio temporal (`.csv`) las variables de los usuarios. Prueba un escenario de borrado del sistema en RAM e invoca a una lectura para recuperar e instanciar clientes pre-cargados. Evita duplicados. |
| **10** | **Dashboard de Estadísticas** | Motor de Analítica y consultas *LINQ*. Genera tablas de resumen contables sobre recaudaciones brutas, categorizaciones sociodemográficas (gastos por perfiles de Platino/Oro/Normal), ocupación actual y conteo de boletos activos. |
| **11** | **Alquiler de Salas** | Prueba de Integración *Generic Interfaces*. Valida la correcta implementación y acople asimétrico de entidades (`IServicio`, `ITipoEvento`). Comprueba la instanciación de un Alquiler de sala total (Corporativos y Privados) operando entre fechas preasignadas y costos base. |

---

## 🛠️ Solución de Problemas Comunes (Troubleshooting)

- **Aparecen caracteres extraños en la consola (Ej: `â”‚` en lugar de líneas visuales):**
  La terminal de su entorno no está logrando asimilar el *Encoding UTF-8* explícito del programa.
  - *Solución (Windows):* Configure su powershell tipeando `chcp 65001` antes de arrancar.
  - *Solución (VS Code / Visual Studio):* Asegúrese de usar la terminal estandar "Developer PowerShell".
  
- **Logs de aviso amarillo / rojizo sobre fallos en compra de boletería (`InvalidOperationException`):**
  **¡No es un defecto, es un diseño provocado!** El módulo #4 realiza internamente un simulacro de una persona menor de edad intentando comprar una boleta para material restrigido de 18+. Esto solo avisa y confirma que la guardia robusta del sistema abortó correctamente la operación indeseada.
  
- **Error durante `TestPersistenciaCSV()` porque no se encuentra la ruta de los archivos temp:**
  Pudo originarse en una carencia de permisos de tu usuario al SO `/tmp` o `%TEMP%`.
  - *Solución:* Verifique que tiene permisos de Administrador/Superusuario y espacio en el disco central C: para creación de un archivo de muy bajo peso; o cambie manualmente la ruta en `Program.cs (Módulo 9)`.

- **Exigencia de un nuevo .NET al usar `dotnet run`:**
  El archivo `.csproj` exige de base **.NET 10**, puede comprobarlo invocando el meta-comando `dotnet --info`. Si lo lanza con un SDK viejo, descárguelo o bien modifique las cabeceras XML en el `.csproj` para permitir el *Roll-back / Downgrade* de framework a uno viejo.
