using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Localgoogle
{
  public class Localgoogle
  {
    public class Archivo
    {
      public string? Nombre { get; set; }
      public string? RutaCompleta { get; set; }
      public string? Extension { get; set; }
      public long? Tamaño { get; set; }
      public DateTime FechaModificacion { get; set; }
    }

    public enum EstadoPrograma
    {
      MenuPrincipal,
      OperadoresLogicos,
      Cuantificadores,
      ArbolDeDirectorio,
      Salir
    }

    static EstadoPrograma estadoActual = EstadoPrograma.MenuPrincipal;

    static void Main()
    {
      while( estadoActual != EstadoPrograma.Salir )
      {
        switch( estadoActual )
        {
          case EstadoPrograma.MenuPrincipal: MenuPrincipal(); break;
          case EstadoPrograma.OperadoresLogicos: AplicarOperadoresLogicos(); break;
          case EstadoPrograma.Cuantificadores: AplicarCuantificadores(); break;
          case EstadoPrograma.ArbolDeDirectorio:  AplicarArbolDirectrorio(); break;
        }
      }
    }

    static void MenuPrincipal()
    {
      Console.Clear();
      Console.WriteLine("Seleccione la forma de buscar");
      Console.WriteLine("1. Operadores lógicos básicos");
      Console.WriteLine("2. Cuantificadores lógicos");
      Console.WriteLine("3. Arbol de Directorios");
      Console.WriteLine("4. Salir");
      
      double tipoOperacion = LeerNumero(
        Mensaje: "Tipo de operación: ",
        MensajeError: "Seleccione 1 al 4",
        min: 1,
        max: 4
      );

      switch( tipoOperacion )
      {
        case 1: estadoActual = EstadoPrograma.OperadoresLogicos; break;
        case 2: estadoActual = EstadoPrograma.Cuantificadores; break;
        case 3: estadoActual = EstadoPrograma.ArbolDeDirectorio; break;
        case 4: estadoActual = EstadoPrograma.Salir; break;
      }
    }

    static void AplicarCuantificadores()
    {
      Console.Clear();
      Console.WriteLine("Cuantidicadores Lógicos");
      Console.WriteLine("1. Cuantificador universal           (∀)    Verificar propiedad para todos");
      Console.WriteLine("2. Cuantificador existencial         (∃)    Verificar si existe alguno");
      Console.WriteLine("3. Cuantificador de existencia única (∃!)   Verificar si existe exactamente uno");
      Console.WriteLine("4. Volver al menu principal");

      double opcion = LeerNumero(
        Mensaje: "Seleccione el cuantificador: ",
        MensajeError: "Seleccione una opción válida (1-5)",
        min: 1,
        max: 4
      );

      if ( opcion == 4 )
      {
        estadoActual = EstadoPrograma.MenuPrincipal;
        return;
      }

      string rutaPrueba = @"./carpetadeprueba";
      List<Archivo> todosArchivos = LeerDirectorio(rutaPrueba);

        switch (opcion)
      {
        case 1: AplicarCuantificadorUniversal(todosArchivos); break;
        case 2: AplicarCuantificadorExistencial(todosArchivos); break;
        case 3: AplicarCuantificadorExistenciaUnica(todosArchivos); break;
      }
      Console.WriteLine("Presione cualquier tecla para continuar.");
      Console.ReadKey();
    }

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

      
      double opcion = LeerNumero(
        Mensaje: "Seleccione operador: ",
        MensajeError: "Seleccione 1-6",
        min: 1,
        max: 6
      );
      if ( opcion == 6 )
      {
        estadoActual = EstadoPrograma.MenuPrincipal;
        return;
      }
      string rutaPrueba = @"./carpetadeprueba";
      List<Archivo> todosArchivos = LeerDirectorio(rutaPrueba);

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

    static void AplicarCuantificadorUniversal(List<Archivo> archivos)
    {
      Console.WriteLine("Cuantificador Universal (∀)");
      Console.WriteLine("Verificar si TODOS los archivos cumplen la propiedad");
      
      int criterio = SeleccionarCriterio();
      var propiedad = DefinirPropiedad(criterio);
      
      bool todosCumplen = archivos.All(propiedad);
      
      Console.WriteLine($"\n Resultado de cuantificador Universal``:");
      Console.WriteLine($"∀x P(x) = {todosCumplen}");
      Console.WriteLine(todosCumplen 
        ? "TODOS los archivos cumplen la propiedad P(x)" 
        : "NO todos los archivos cumplen la propiedad P(x)");
    }

    static void AplicarCuantificadorExistencial(List<Archivo> archivos)
    {
      Console.WriteLine("Cuantificador existencial (∃)");
      Console.WriteLine("Verificar si EXISTE AL MENOS UN archivo que cumple la propiedad");
      
      int criterio = SeleccionarCriterio();
      var propiedad = DefinirPropiedad(criterio);
      
      bool existeAlMenosUno = archivos.Any(propiedad);
      var archivosQueCumplen = archivos.Where(propiedad).ToList();
      
      Console.WriteLine($"\n Resultado de cuantificador existencial:");
      Console.WriteLine($"∃x P(x) = {existeAlMenosUno}");
      Console.WriteLine(existeAlMenosUno 
        ? $"EXISTEN {archivosQueCumplen.Count} archivos que cumplen P(x)" 
        : "NO EXISTE ningún archivo que cumpla P(x)");
      
      if (existeAlMenosUno)
      {
        Console.WriteLine("Archivos que cumplen:");
        foreach (var archivo in archivosQueCumplen)
        {
          Console.WriteLine($" {archivo.RutaCompleta}");
        }
      }
    }

    static void AplicarCuantificadorExistenciaUnica(List<Archivo> archivos)
    {
      Console.WriteLine("Cuantificador de exitiancia único (∃!)");
      Console.WriteLine("Verificar si EXISTE EXACTAMENTE UN archivo que cumple la propiedad");
      
      int criterio = SeleccionarCriterio();
      var propiedad = DefinirPropiedad(criterio);
      
      var archivosQueCumplen = archivos.Where(propiedad).ToList();
      bool existeExactamenteUno = archivosQueCumplen.Count == 1;
      
      Console.WriteLine($"\n Resultado cuantificador existencia única:");
      Console.WriteLine($"∃!x P(x) = {existeExactamenteUno}");
      Console.WriteLine(existeExactamenteUno 
        ? $"EXISTE EXACTAMENTE 1 archivo que cumple P(x)" 
        : $"NO existe exactamente uno. Hay {archivosQueCumplen.Count} archivos que cumplen P(x)");
      
      if (existeExactamenteUno)
      {
        Console.WriteLine($"Archivo único: {archivosQueCumplen[0].RutaCompleta}");
      }
    }

    static void AplicarConjuncion(List<Archivo> archivos)
    {
      Console.WriteLine("Conjunción lógica (&&)");
      Console.WriteLine("Archivos que cumplen P(x) ∧ Q(x)");
      
      Console.WriteLine("Defina la primera propiedad P(x):");
      int criterio1 = SeleccionarCriterio();
      var propiedad1 = DefinirPropiedad(criterio1);
      
      Console.WriteLine("Defina la segunda propiedad Q(x):");
      int criterio2 = SeleccionarCriterio();
      var propiedad2 = DefinirPropiedad(criterio2);
      
      var resultado = archivos.Where(a => propiedad1(a) && propiedad2(a)).ToList();
      
      Console.WriteLine($"\n Resultado conjunción:");
      Console.WriteLine($"|{{x : P(x) ∧ Q(x)}}| = {resultado.Count}");
      
      foreach (var archivo in resultado)
      {
        Console.WriteLine($" {archivo.RutaCompleta}");
      }
    }

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
      
      var resultado = archivos.Where(a => propiedad1(a) || propiedad2(a)).ToList();
      
      Console.WriteLine($"\n Resultado disyunción:");
      Console.WriteLine($"|{{x : P(x) ∨ Q(x)}}| = {resultado.Count}");
      
      foreach (var archivo in resultado)
      {
        Console.WriteLine($" {archivo.RutaCompleta}");
      }
    }

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
      
      var resultado = archivos.Where(a => propiedad1(a) == propiedad2(a)).ToList();
      
      Console.WriteLine($"\n Resultado disyunción:");
      Console.WriteLine($"|{{x : P(x) <-> Q(x)}}| = {resultado.Count}");
      
      foreach (var archivo in resultado)
      {
        Console.WriteLine($" {archivo.RutaCompleta}");
      }
      
    }
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
      
      var resultado = archivos.Where(a => !propiedad1(a) || propiedad2(a)).ToList();
      
      Console.WriteLine($"\n Resultado disyunción:");
      Console.WriteLine($"|{{x : P(x) -> Q(x)}}| = {resultado.Count}");
      
      foreach (var archivo in resultado)
      {
        Console.WriteLine($" {archivo.RutaCompleta}");
      }
    }
    static void AplicarNegacion(List<Archivo> archivos)
    {
      Console.WriteLine("Negación lógica (!)");
      Console.WriteLine("Archivos que cumplen ¬ P(x)");
      
      int criterio = SeleccionarCriterio();
      var propiedad = DefinirPropiedad(criterio);
      
      var resultado = archivos.Where(a => !propiedad(a)).ToList();
      
      Console.WriteLine($"\n Resultado negación:");
      Console.WriteLine($"|{{x : ¬ P(x)}}| = {resultado.Count}");
      Console.WriteLine($"Archivos que NO cumplen la propiedad:");
      
      foreach (var archivo in resultado)
      {
        Console.WriteLine($" {archivo.RutaCompleta}");
      }
    }

    static Func<Archivo, bool> DefinirPropiedad(int criterio)
    {
      switch (criterio)
      {
        case 1:
          string? nombre = LeerLetras(
              Mensaje: "Ingrese nombre o parte de el: ",
              MensajeError: "El nombre no puede estar vacío"
              );
          return a => a.Nombre?.Contains(nombre ?? "", StringComparison.OrdinalIgnoreCase) == true;
        
        case 2:
          string? extension = LeerLetras(
              Mensaje: "Ingrese extensión (ej: .jpg, .txt): ",
              MensajeError: "Ingrese un valor adecuado y con el formato necesario."
              );
          return a => a.Extension?.Equals(extension ?? "", StringComparison.OrdinalIgnoreCase) == true;
        
        case 3:
          double tamañoDouble = LeerNumero(
              Mensaje: "Ingrese tamaño minimo en bytes: ",
              MensajeError: "Debe ingresar un numero mayor o igual a cero.",
              min: 0
              );
          return a => a.Tamaño >= (long)tamañoDouble;
        
        case 4:
          DateTime fecha = LeerFecha("Ingrese fecha (dd/MM/yyyy): ", "Formato inválido");
          return a => a.FechaModificacion >= fecha;
        
        default:
            return a => true;
      }
    }

    static List<Archivo> RealizarBusquedaPorCriterio(string rutaBase)
    {
      int criterio = SeleccionarCriterio();
      List<Archivo> todosArchivos = LeerDirectorio(rutaBase);
      var propiedad = DefinirPropiedad(criterio);
      return todosArchivos.Where(propiedad).ToList();
    }

    static List<Archivo> LeerDirectorio(string ruta)
    {
      var archivos = new List<Archivo>();
      try
      {
        if (!Directory.Exists(ruta))
        {
          ruta = Path.Combine(Directory.GetCurrentDirectory(), "carpetadeprueba");
          if (!Directory.Exists(ruta))
          {
            Console.WriteLine($"No se encontró la carpeta: {ruta}");
            return archivos;
          }
        }
        string[] todosArchivos = Directory.GetFiles(ruta, "*.*", SearchOption.AllDirectories);
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
        Console.WriteLine($"Se encontraron {archivos.Count} archivos en: {ruta}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error al leer directorio: {ex.Message}");
      }
      return archivos;
    }

    static void AplicarArbolDirectrorio()
    {
      Console.Clear();
      Console.WriteLine("Árbol Directorios");
      
      string rutaBase = @"./carpetadeprueba";
      
      if (!Directory.Exists(rutaBase))
      {
        Console.WriteLine("No se encontró la carpeta de prueba.");
        Console.WriteLine("Presione cualquier tecla para continuar.");
        Console.ReadKey();
        estadoActual = EstadoPrograma.MenuPrincipal;
        return;
      }
      
      MostrarArbolSimpleRecursivo(rutaBase, "");
      
      Console.WriteLine("Presione cualquier tecla para continuar.");
      Console.ReadKey();
      estadoActual = EstadoPrograma.MenuPrincipal;
    }

    static void MostrarArbolSimpleRecursivo(string directorio, string indentacion)
    {
      string nombreDirectorio = Path.GetFileName(directorio);
      if (string.IsNullOrEmpty(nombreDirectorio))
        nombreDirectorio = new DirectoryInfo(directorio).Name;
      
      Console.WriteLine($"{indentacion}{nombreDirectorio}/");
      
      try
      {
        var archivos = Directory.GetFiles(directorio);
        var subdirectorios = Directory.GetDirectories(directorio);
        
        // Mostrar archivos primero
        foreach (var archivo in archivos)
        {
          string nombreArchivo = Path.GetFileName(archivo);
          Console.WriteLine($"{indentacion}  └── {nombreArchivo}");
        }
        
        // Luego subdirectorios (recursivo)
        foreach (var subdir in subdirectorios)
        {
          MostrarArbolSimpleRecursivo(subdir, indentacion + "  ");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"{indentacion}  └── [Error: {ex.Message}]");
      }
    }

    static DateTime LeerFecha(string mensaje, string mensajeError)
    {
      while (true)
      {
        Console.Write(mensaje);
        string? input = Console.ReadLine()?.Trim();
        if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
        {
          return fecha;
        }
        Console.WriteLine(mensajeError);
      }
    }

    static double LeerNumero(string Mensaje, string MensajeError, int min = 0, int max = int.MaxValue)
    {
      while (true)
      {
        Console.Write(Mensaje);
        string? input = Console.ReadLine()?.Trim();
        if (!double.TryParse(input, out double valor))
        {
          Console.WriteLine(MensajeError);
        }
        else if (valor > max || valor < min)
        {
          Console.WriteLine(MensajeError);
        }
        else
        {
          return valor;
        }
      }
    }

    static string LeerLetras(string Mensaje, string MensajeError)
    {
      while (true)
      {
        Console.Write(Mensaje);
        string? input = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(input))
        {
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
