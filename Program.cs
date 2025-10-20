using System.Globalization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Localgoogle
{
  public class Localgoogle
  {
    // Clase que almacena la información que el programa usará de cada archivo.
    // Se usa para mantener los datos en memoria y aplicar filtros (propiedades).
    public class Archivo
    {
      // Nombre del archivo (ej: "documento.txt")
      public string? Nombre { get; set; }

      // Ruta completa del archivo (ej: "C:\proyecto\carpetadeprueba\documento.txt")
      public string? RutaCompleta { get; set; }

      // Extensión del archivo en minúsculas (ej: ".txt", ".jpg")
      public string? Extension { get; set; }

      // Tamaño en bytes (puede ser null si no se logra obtener)
      public long? Tamaño { get; set; }

      // Fecha de última modificación (útil para filtros por fecha)
      public DateTime FechaModificacion { get; set; }
    }

    // Enum que representa los distintos menús / estados en los que puede estar el programa
    public enum EstadoPrograma
    {
      MenuPrincipal,
      OperadoresLogicos,
      Cuantificadores,
      ArbolDeDirectorio,
      Salir
    }

    // Variable global que almacena el estado actual del programa (empieza en el menú principal)
    static EstadoPrograma estadoActual = EstadoPrograma.MenuPrincipal;

    // Punto de entrada del programa
    static void Main()
    {
      // Bucle principal que mantiene el programa vivo hasta que el usuario elija "Salir"
      while (estadoActual != EstadoPrograma.Salir)
      {
        // Se invoca el método correspondiente según el estado actual
        switch (estadoActual)
        {
          case EstadoPrograma.MenuPrincipal: MenuPrincipal(); break;
          case EstadoPrograma.OperadoresLogicos: AplicarOperadoresLogicos(); break;
          case EstadoPrograma.Cuantificadores: AplicarCuantificadores(); break;
          case EstadoPrograma.ArbolDeDirectorio: AplicarArbolDirectrorio(); break;
        }
      }
    }

    // Muestra el menú principal y cambia el estado según la opción elegida
    static void MenuPrincipal()
    {
      Console.Clear();
      Console.WriteLine("Seleccione la forma de buscar");
      Console.WriteLine("1. Operadores lógicos básicos");
      Console.WriteLine("2. Cuantificadores lógicos");
      Console.WriteLine("3. Arbol de Directorios");
      Console.WriteLine("4. Salir");

      // Lee un número validado entre 1 y 4 (ver función LeerNumero)
      double tipoOperacion = LeerNumero(
        Mensaje: "Tipo de operación: ",
        MensajeError: "Seleccione 1 al 4",
        min: 1,
        max: 4
      );

      // Asigna el nuevo estado para que el bucle Main invoque la función adecuada
      switch (tipoOperacion)
      {
        case 1: estadoActual = EstadoPrograma.OperadoresLogicos; break;
        case 2: estadoActual = EstadoPrograma.Cuantificadores; break;
        case 3: estadoActual = EstadoPrograma.ArbolDeDirectorio; break;
        case 4: estadoActual = EstadoPrograma.Salir; break;
      }
    }

    // Menú y flujo para las opciones de cuantificadores (∀, ∃, ∃!)
    static void AplicarCuantificadores()
    {
      Console.Clear();
      Console.WriteLine("Cuantidicadores Lógicos");
      Console.WriteLine("1. Cuantificador universal           (∀)    Verificar propiedad para todos");
      Console.WriteLine("2. Cuantificador existencial         (∃)    Verificar si existe alguno");
      Console.WriteLine("3. Cuantificador de existencia única (∃!)   Verificar si existe exactamente uno");
      Console.WriteLine("4. Volver al menu principal");

      // Lee opción y valida entre 1 y 4
      double opcion = LeerNumero(
        Mensaje: "Seleccione el cuantificador: ",
        MensajeError: "Seleccione una opción válida (1-5)",
        min: 1,
        max: 4
      );

      // Opción 4 regresa al menú principal
      if (opcion == 4)
      {
        estadoActual = EstadoPrograma.MenuPrincipal;
        return;
      }

      // Ruta base donde se buscarán los archivos (carpeta de prueba incluida en el proyecto)
      string rutaPrueba = @"./carpetadeprueba";

      // Lee todos los archivos encontrados (función que recorre la carpeta y devuelve List<Archivo>)
      List<Archivo> todosArchivos = LeerDirectorio(rutaPrueba);

      // Según la opción, llama al cuantificador correspondiente
      switch (opcion)
      {
        case 1: AplicarCuantificadorUniversal(todosArchivos); break;
        case 2: AplicarCuantificadorExistencial(todosArchivos); break;
        case 3: AplicarCuantificadorExistenciaUnica(todosArchivos); break;
      }
      Console.WriteLine("Presione cualquier tecla para continuar.");
      Console.ReadKey();
    }

    // Menú y flujo para operadores lógicos (AND, OR, NOT, ->, <->)
    static void AplicarOperadoresLogicos()
    {
      Console.Clear();
      Console.WriteLine("Operadores lógicos varios");
      Console.WriteLine("1. Conjunción (&&)          P(x)  ∧  Q(x)");
      Console.WriteLine("2. Disyunción (||)          P(x)  ∨  Q(x)");
      Console.WriteLine("3. Negación (!)           ¬ P(x)");
      Console.WriteLine("4. Implicación (! ||)       P(x)  -> Q(x)");
      Console.WriteLine("5. Doble implicación (==)   P(x) <-> Q(x)");
      Console.WriteLine("6. Volver al menu principal");


      // Lee la opción del usuario (1..6)
      double opcion = LeerNumero(
        Mensaje: "Seleccione operador: ",
        MensajeError: "Seleccione 1-6",
        min: 1,
        max: 6
      );
      if (opcion == 6)
      {
        // Volver al menú principal
        estadoActual = EstadoPrograma.MenuPrincipal;
        return;
      }
      string rutaPrueba = @"./carpetadeprueba";

      // Carga los archivos a evaluar
      List<Archivo> todosArchivos = LeerDirectorio(rutaPrueba);

      // Llama al método que aplica el operador elegido
      switch (opcion)
      {
        case 1: AplicarConjuncion(todosArchivos); break;
        case 2: AplicarDisyuncion(todosArchivos); break;
        case 3: AplicarNegacion(todosArchivos); break;
        case 4: AplicarImplicacion(todosArchivos); break;
        case 5: AplicarDobleImplicacion(todosArchivos); break;
      }
      Console.WriteLine("Presione cualquier tecla para continuar.");
      Console.ReadKey();
    }

    // Muestra las opciones de criterios de búsqueda y devuelve la opción seleccionada (1..4)
    static int SeleccionarCriterio()
    {
      Console.WriteLine("Seleccione criterio de búsqueda:");
      Console.WriteLine("1. Por nombre");
      Console.WriteLine("2. Por tipo/extensión");
      Console.WriteLine("3. Por tamaño");
      Console.WriteLine("4. Por fecha");

      double criterio = LeerNumero(
        Mensaje: "Criterio: ",
        MensajeError: "Seleccione 1-4",
        min: 1,
        max: 4
      );
      return (int)criterio;
    }

    // === Implementación de cuantificadores ===

    // Cuantificador universal: verifica si TODOS los archivos cumplen la propiedad P(x)
    static void AplicarCuantificadorUniversal(List<Archivo> archivos)
    {
      Console.WriteLine("Cuantificador Universal (∀)");
      Console.WriteLine("Verificar si TODOS los archivos cumplen la propiedad");

      // El usuario elige el criterio (nombre, extensión, tamaño o fecha)
      int criterio = SeleccionarCriterio();

      // DefinirPropiedad construye una función Func<Archivo,bool> según el criterio
      var propiedad = DefinirPropiedad(criterio);

      // Usa LINQ .All para verificar que todos los elementos cumplan
      bool todosCumplen = archivos.All(propiedad);

      Console.WriteLine($"\n Resultado de cuantificador Universal``:");
      Console.WriteLine($"∀x P(x) = {todosCumplen}");
      Console.WriteLine(todosCumplen
        ? "TODOS los archivos cumplen la propiedad P(x)"
        : "NO todos los archivos cumplen la propiedad P(x)");
    }

    // Cuantificador existencial: verifica si EXISTE al menos un archivo que cumpla P(x)
    static void AplicarCuantificadorExistencial(List<Archivo> archivos)
    {
      Console.WriteLine("Cuantificador existencial (∃)");
      Console.WriteLine("Verificar si EXISTE AL MENOS UN archivo que cumple la propiedad");

      int criterio = SeleccionarCriterio();
      var propiedad = DefinirPropiedad(criterio);

      // .Any determina si hay al menos uno que cumpla la propiedad
      bool existeAlMenosUno = archivos.Any(propiedad);

      // Se almacenan los que cumplen para mostrarlos en caso afirmativo
      var archivosQueCumplen = archivos.Where(propiedad).ToList();

      Console.WriteLine($"\n Resultado de cuantificador existencial:");
      Console.WriteLine($"∃x P(x) = {existeAlMenosUno}");
      Console.WriteLine(existeAlMenosUno
        ? $"EXISTEN {archivosQueCumplen.Count} archivos que cumplen P(x)"
        : "NO EXISTE ningún archivo que cumpla P(x)");

      // Si existe al menos uno, se listan las rutas completas
      if (existeAlMenosUno)
      {
        Console.WriteLine("Archivos que cumplen:");
        foreach (var archivo in archivosQueCumplen)
        {
          Console.WriteLine($" {archivo.RutaCompleta}");
        }
      }
    }

    // Cuantificador de existencia única: verifica si existe exactamente 1 archivo que cumpla P(x)
    static void AplicarCuantificadorExistenciaUnica(List<Archivo> archivos)
    {
      Console.WriteLine("Cuantificador de exitiancia único (∃!)");
      Console.WriteLine("Verificar si EXISTE EXACTAMENTE UN archivo que cumple la propiedad");

      int criterio = SeleccionarCriterio();
      var propiedad = DefinirPropiedad(criterio);

      // Filtra y cuenta los que cumplen
      var archivosQueCumplen = archivos.Where(propiedad).ToList();
      bool existeExactamenteUno = archivosQueCumplen.Count == 1;

      Console.WriteLine($"\n Resultado cuantificador existencia única:");
      Console.WriteLine($"∃!x P(x) = {existeExactamenteUno}");
      Console.WriteLine(existeExactamenteUno
        ? $"EXISTE EXACTAMENTE 1 archivo que cumple P(x)"
        : $"NO existe exactamente uno. Hay {archivosQueCumplen.Count} archivos que cumplen P(x)");

      // Si existe exactamente uno, se muestra la ruta de ese archivo
      if (existeExactamenteUno)
      {
        Console.WriteLine($"Archivo único: {archivosQueCumplen[0].RutaCompleta}");
      }
    }

    // === Implementación de operadores lógicos ===

    // Conjunción lógica: P(x) ∧ Q(x) -> archivos que cumplen ambas propiedades
    static void AplicarConjuncion(List<Archivo> archivos)
    {
      Console.WriteLine("Conjunción lógica (&&)");
      Console.WriteLine("Archivos que cumplen P(x) ∧ Q(x)");

      // Pide y construye la primera propiedad
      Console.WriteLine("Defina la primera propiedad P(x):");
      int criterio1 = SeleccionarCriterio();
      var propiedad1 = DefinirPropiedad(criterio1);

      // Pide y construye la segunda propiedad
      Console.WriteLine("Defina la segunda propiedad Q(x):");
      int criterio2 = SeleccionarCriterio();
      var propiedad2 = DefinirPropiedad(criterio2);

      // Filtra los archivos que cumplen ambas funciones (AND)
      var resultado = archivos.Where(a => propiedad1(a) && propiedad2(a)).ToList();

      Console.WriteLine($"\n Resultado conjunción:");
      Console.WriteLine($"|{{x : P(x) ∧ Q(x)}}| = {resultado.Count}");

      // Imprime las rutas completas de cada archivo resultado
      foreach (var archivo in resultado)
      {
        Console.WriteLine($" {archivo.RutaCompleta}");
      }
    }

    // Disyunción lógica: P(x) ∨ Q(x) -> archivos que cumplen al menos una propiedad
    static void AplicarDisyuncion(List<Archivo> archivos)
    {
      Console.WriteLine("Disyunción lógica (||)");
      Console.WriteLine("Archivos que cumplen P(x) ∨ Q(x)");

      Console.WriteLine("Defina la primera propiedad P(x):");
      int criterio1 = SeleccionarCriterio();
      var propiedad1 = DefinirPropiedad(criterio1);

      Console.WriteLine("Defina la segunda propiedad Q(x):");
      int criterio2 = SeleccionarCriterio();
      var propiedad2 = DefinirPropiedad(criterio2);

      // OR lógico usando || en la condición del Where
      var resultado = archivos.Where(a => propiedad1(a) || propiedad2(a)).ToList();

      Console.WriteLine($"\n Resultado disyunción:");
      Console.WriteLine($"|{{x : P(x) ∨ Q(x)}}| = {resultado.Count}");

      foreach (var archivo in resultado)
      {
        Console.WriteLine($" {archivo.RutaCompleta}");
      }
    }

    // Doble implicación / equivalencia: P(x) == Q(x) -> ambos son iguales (verdadero/verdadero o falso/falso)
    static void AplicarDobleImplicacion(List<Archivo> archivos)
    {
      Console.WriteLine("Doble Implicación lógica (==)");
      Console.WriteLine("Archivos que cumplen P(x) == Q(x)");

      Console.WriteLine("Defina la primera propiedad P(x):");
      int criterio1 = SeleccionarCriterio();
      var propiedad1 = DefinirPropiedad(criterio1);

      Console.WriteLine("Defina la segunda propiedad Q(x):");
      int criterio2 = SeleccionarCriterio();
      var propiedad2 = DefinirPropiedad(criterio2);

      // Compara igualdad de booleanos: sólo true si ambas propiedades coinciden
      var resultado = archivos.Where(a => propiedad1(a) == propiedad2(a)).ToList();

      Console.WriteLine($"\n Resultado doble Implicación:");
      Console.WriteLine($"|{{x : P(x) <-> Q(x)}}| = {resultado.Count}");

      foreach (var archivo in resultado)
      {
        Console.WriteLine($" {archivo.RutaCompleta}");
      }

    }

    // Implicación lógica: se usa la equivalencia (!P || Q) para representar P -> Q
    static void AplicarImplicacion(List<Archivo> archivos)
    {
      Console.WriteLine("Implicación lógica (! ||)");
      Console.WriteLine("Archivos que cumplen !P(x) || Q(x)");

      Console.WriteLine("Defina la primera propiedad P(x):");
      int criterio1 = SeleccionarCriterio();
      var propiedad1 = DefinirPropiedad(criterio1);

      Console.WriteLine("Defina la segunda propiedad Q(x):");
      int criterio2 = SeleccionarCriterio();
      var propiedad2 = DefinirPropiedad(criterio2);

      // Implementación práctica de la implicación lógica
      var resultado = archivos.Where(a => !propiedad1(a) || propiedad2(a)).ToList();

      Console.WriteLine($"\n Resultado Implicación:");
      Console.WriteLine($"|{{x : P(x) -> Q(x)}}| = {resultado.Count}");

      foreach (var archivo in resultado)
      {
        Console.WriteLine($" {archivo.RutaCompleta}");
      }
    }

    // Negación: devuelve los archivos que NO cumplen la propiedad P(x)
    static void AplicarNegacion(List<Archivo> archivos)
    {
      Console.WriteLine("Negación lógica (!)");
      Console.WriteLine("Archivos que cumplen ¬ P(x)");

      int criterio = SeleccionarCriterio();
      var propiedad = DefinirPropiedad(criterio);

      // Filtra los archivos que NO cumplen (operador ! delante de la función)
      var resultado = archivos.Where(a => !propiedad(a)).ToList();

      Console.WriteLine($"\n Resultado negación:");
      Console.WriteLine($"|{{x : ¬ P(x)}}| = {resultado.Count}");
      Console.WriteLine($"Archivos que NO cumplen la propiedad:");

      foreach (var archivo in resultado)
      {
        Console.WriteLine($" {archivo.RutaCompleta}");
      }
    }

    // Construye y devuelve una función (predicate) que representa la propiedad P(x)
    // según el criterio elegido por el usuario. Esa función recibe un Archivo y devuelve bool.
    static Func<Archivo, bool> DefinirPropiedad(int criterio)
    {
      switch (criterio)
      {
        case 1:
          // Criterio por nombre: el usuario escribe una cadena y se verifica Contains (case-insensitive)
          string? nombre = LeerLetras(
            Mensaje: "Ingrese nombre o parte de el: ",
            MensajeError: "El nombre no puede estar vacío"
            );
          // Retorna un lambda que verifica si el Nombre contiene la subcadena ingresada
          return a => a.Nombre?.Contains(nombre ?? "", StringComparison.OrdinalIgnoreCase) == true;

        case 2:
          // Criterio por extensión: el usuario debe escribir algo como ".txt"
          string? extension = LeerLetras(
              Mensaje: "Ingrese extensión (ej: .jpg, .txt): ",
              MensajeError: "Ingrese un valor adecuado y con el formato necesario."
              );
          // Retorna un lambda que compara la extensión (igualdad case-insensitive)
          return a => a.Extension?.Equals(extension ?? "", StringComparison.OrdinalIgnoreCase) == true;

        case 3:
          // Criterio por tamaño mínimo: el usuario ingresa un número (bytes)
          double tamañoDouble = LeerNumero(
              Mensaje: "Ingrese tamaño minimo en bytes: ",
              MensajeError: "Debe ingresar un numero mayor o igual a cero.",
              min: 0
              );
          // Se compara el tamaño del archivo con el número ingresado
          return a => a.Tamaño >= (long)tamañoDouble;

        case 4:
          // Criterio por fecha: el usuario ingresa una fecha y se verifica fecha >= ingresada
          DateTime fecha = LeerFecha("Ingrese fecha (dd/MM/yyyy): ", "Formato inválido");
          return a => a.FechaModificacion >= fecha;

        default:
          // Si hubiera un criterio inválido, devolver una propiedad que siempre sea true
          return a => true;
      }
    }

    // Permite realizar una búsqueda por criterio en una ruta base proporcionada
    // Este método no se usa en todos los flujos, pero es útil para búsquedas directas.
    static List<Archivo> RealizarBusquedaPorCriterio(string rutaBase)
    {
      int criterio = SeleccionarCriterio();
      List<Archivo> todosArchivos = LeerDirectorio(rutaBase);
      var propiedad = DefinirPropiedad(criterio);
      return todosArchivos.Where(propiedad).ToList();
    }

    // Lee el directorio dado, recorre todos los archivos (incluso subdirectorios) y devuelve la lista de Archivo.
    static List<Archivo> LeerDirectorio(string ruta)
    {
      var archivos = new List<Archivo>();
      try
      {
        // Si la ruta no existe, intenta usar una ruta relativa en el CurrentDirectory
        if (!Directory.Exists(ruta))
        {
          ruta = Path.Combine(Directory.GetCurrentDirectory(), "carpetadeprueba");
          if (!Directory.Exists(ruta))
          {
            // Si tampoco existe, informa y devuelve lista vacía
            Console.WriteLine($"No se encontró la carpeta: {ruta}");
            return archivos;
          }
        }

        // Obtiene todos los archivos en el directorio y subdirectorios
        string[] todosArchivos = Directory.GetFiles(ruta, "*.*", SearchOption.AllDirectories);

        // Por cada archivo crea un objeto Archivo con la información relevante
        foreach (string rutaCompleta in todosArchivos)
        {
          FileInfo info = new FileInfo(rutaCompleta);
          archivos.Add(new Archivo
          {
            Nombre = Path.GetFileName(rutaCompleta),
            RutaCompleta = rutaCompleta,
            Extension = Path.GetExtension(rutaCompleta).ToLower(),
            Tamaño = info.Length,
            FechaModificacion = info.LastWriteTime
          });
        }

        // Mensaje informativo que indica cuántos archivos se encontraron
        Console.WriteLine($"Se encontraron {archivos.Count} archivos en: {ruta}");
      }
      catch (Exception ex)
      {
        // Captura errores (permisos, rutas inválidas, archivos bloqueados, etc.)
        Console.WriteLine($"Error al leer directorio: {ex.Message}");
      }
      return archivos;
    }

    // Muestra de forma recursiva un árbol simple de directorios y archivos en consola
    static void AplicarArbolDirectrorio()
    {
      Console.Clear();
      Console.WriteLine("Árbol Directorios");

      string rutaBase = @"./carpetadeprueba";

      // Verifica existencia de la carpeta base y, si no existe, vuelve al menú
      if (!Directory.Exists(rutaBase))
      {
        Console.WriteLine("No se encontró la carpeta de prueba.");
        Console.WriteLine("Presione cualquier tecla para continuar.");
        Console.ReadKey();
        estadoActual = EstadoPrograma.MenuPrincipal;
        return;
      }

      // Llama a la función recursiva que imprime la jerarquía
      MostrarArbolSimpleRecursivo(rutaBase, "");

      Console.WriteLine("Presione cualquier tecla para continuar.");
      Console.ReadKey();
      estadoActual = EstadoPrograma.MenuPrincipal;
    }

    // Función recursiva que imprime nombre de directorio, luego archivos y luego subdirectorios.
    // `indentacion` se usa para dar formato y mostrar niveles (tabulado).
    // Lee una fecha con formato dd/MM/yyyy desde consola y valida el input
    static void MostrarArbolSimpleRecursivo(string directorio, string indentacion, bool esUltimo = true)
    {
      string nombreDirectorio = Path.GetFileName(directorio);
      if (string.IsNullOrEmpty(nombreDirectorio))
          nombreDirectorio = new DirectoryInfo(directorio).Name;
      
      // Imprimir el directorio actual con formato mejorado
      Console.Write(indentacion);
      Console.Write(esUltimo ? "└── " : "├── ");
      Console.ForegroundColor = ConsoleColor.Cyan;  // Color azul para carpetas
      Console.Write($"{nombreDirectorio}");
      Console.ResetColor();
      Console.WriteLine("/");
      
      try
      {
        var archivos = Directory.GetFiles(directorio);
        var subdirectorios = Directory.GetDirectories(directorio);
        
        string nuevaIndentacion = indentacion + (esUltimo ? "    " : "│   ");
        
        // Mostrar archivos primero
        for (int i = 0; i < archivos.Length; i++)
        {
          string nombreArchivo = Path.GetFileName(archivos[i]);
          bool esUltimoArchivo = (i == archivos.Length - 1) && (subdirectorios.Length == 0);
          
          Console.Write(nuevaIndentacion);
          Console.Write(esUltimoArchivo ? "└── " : "├── ");
          Console.ForegroundColor = ConsoleColor.White;  // Color blanco para archivos
          Console.WriteLine(nombreArchivo);
          Console.ResetColor();
        }
        
        // Mostrar subdirectorios (recursivo)
        for (int i = 0; i < subdirectorios.Length; i++)
        {
          bool esUltimoSubdir = (i == subdirectorios.Length - 1);
          MostrarArbolSimpleRecursivo(subdirectorios[i], nuevaIndentacion, esUltimoSubdir);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"{indentacion}    └── [Error: {ex.Message}]");
      }
    }
    static DateTime LeerFecha(string mensaje, string mensajeError)
    {
      while (true)
      {
        Console.Write(mensaje);
        string? input = Console.ReadLine()?.Trim();

        // TryParseExact obliga a que el formato sea exactamente dd/MM/yyyy
        if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
        {
          return fecha;
        }
        // Si falla la conversión, muestra mensaje de error y vuelve a pedir
        Console.WriteLine(mensajeError);
      }
    }

    // Lee y valida un número en rango [min, max]. Retorna double (se usa para leer opciones y tamaños).
    static double LeerNumero(string Mensaje, string MensajeError, int min = 0, int max = int.MaxValue)
    {
      while (true)
      {
        Console.Write(Mensaje);
        string? input = Console.ReadLine()?.Trim();
        if (!double.TryParse(input, out double valor))
        {
          // Si no se puede parsear a número, muestra error
          Console.WriteLine(MensajeError);
        }
        else if (valor > max || valor < min)
        {
          // Si el número está fuera del rango permitido, muestra error
          Console.WriteLine(MensajeError);
        }
        else
        {
          // Valor válido -> se retorna
          return valor;
        }
      }
    }

    // Lee una cadena de texto no vacía. Ideal para leer nombres o extensiones.
    static string LeerLetras(string Mensaje, string MensajeError)
    {
      while (true)
      {
        Console.Write(Mensaje);
        string? input = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(input))
        {
            // Si está vacío, repite la solicitud mostrando el mensaje de error
            Console.WriteLine(MensajeError);
        }
        else
        {
          return input;
        }
      }
    }
  }
}
