using System;
using System.Globalization;

namespace Localgoogle
{
  public class Localgoogle
  {
    static void Main()
    {
        Console.WriteLine(":: BUSQUEDA COMBINADA ::");
        Console.WriteLine("Seleccione el tipo de operación:");
        Console.WriteLine("1. Operadores lógicos básicos");
        Console.WriteLine("2. Cuantificadores lógicos");
        
        double tipoOperacion = LeerNumero(
            Mensaje: "Tipo de operación: ",
            MensajeError: "Seleccione 1 o 2",
            min: 1,
            max: 2
        );

        if (tipoOperacion == 1)
        {
            AplicarOperadoresLogicos();
        }
        else
        {
            AplicarCuantificadores();
        }
    }

    static void AplicarCuantificadores()
    {
        Console.WriteLine(":: CUANTIFICADORES LÓGICOS ::");
        Console.WriteLine("1. CUANTIFICADOR UNIVERSAL (∀) - Verificar propiedad para todos");
        Console.WriteLine("2. CUANTIFICADOR EXISTENCIAL (∃) - Verificar si existe alguno");
        Console.WriteLine("3. CUANTIFICADOR DE EXISTENCIA ÚNICA (∃!) - Verificar si existe exactamente uno");
        Console.WriteLine("4. CUANTIFICADOR DE CONTEO - Verificar cuántos cumplen");

        double opcion = LeerNumero(
            Mensaje: "Seleccione el cuantificador: ",
            MensajeError: "Seleccione una opción válida (1-4)",
            min: 1,
            max: 4
        );

        // Primero obtenemos un conjunto base de archivos
        Console.WriteLine("Seleccione el conjunto base de archivos:");
        List<string> conjuntoBase = ObtenerConjuntoBase();

        switch (opcion)
        {
            case 1:
                AplicarCuantificadorUniversal(conjuntoBase);
                break;
            case 2:
                AplicarCuantificadorExistencial(conjuntoBase);
                break;
            case 3:
                AplicarCuantificadorExistenciaUnica(conjuntoBase);
                break;
            case 4:
                AplicarCuantificadorConteo(conjuntoBase);
                break;
        }
    }

    static List<string> ObtenerConjuntoBase()
    {
        Console.WriteLine("¿Cómo desea definir el conjunto base?");
        Console.WriteLine("1. Todos los archivos del directorio");
        Console.WriteLine("2. Por criterio de búsqueda específico");
        
        double opcion = LeerNumero(
            Mensaje: "Opción: ",
            MensajeError: "Seleccione 1 o 2",
            min: 1,
            max: 2
        );

        if (opcion == 1)
        {
            // Simulación: todos los archivos
            return new List<string> { 
                "documento1.txt", "imagen1.jpg", "video1.mp4", 
                "programa1.exe", "datos1.csv", "config1.ini" 
            };
        }
        else
        {
            // Realizar búsqueda por criterio específico
            return RealizarBusquedaPorCriterio();
        }
    }

    static void RealizarBusquedaIndividual()
    {
    }
    static void AplicarOperadoresLogicos()
    {
    }
    // CUANTIFICADOR UNIVERSAL (∀) - "Para todo x, P(x)"
    static void AplicarCuantificadorUniversal(List<string> archivos)
    {
    }

    // CUANTIFICADOR EXISTENCIAL (∃) - "Existe al menos un x tal que P(x)"
    static void AplicarCuantificadorExistencial(List<string> archivos)
    {
    }

    // CUANTIFICADOR DE EXISTENCIA ÚNICA (∃!) - "Existe exactamente un x tal que P(x)"
    static void AplicarCuantificadorExistenciaUnica(List<string> archivos)
    {
    }

    // CUANTIFICADOR DE CONTEO - "Contar cuántos x cumplen P(x)"
    static void AplicarCuantificadorConteo(List<string> archivos)
    {
    }

    static int SeleccionarCriterio()
    {
        Console.WriteLine("1. Por nombre");
        Console.WriteLine("2. Por tipo");
        Console.WriteLine("3. Por tamaño");
        Console.WriteLine("4. Por contenido");
        Console.WriteLine("5. Por fecha");

        double criterio = LeerNumero(
            Mensaje: "Criterio: ",
            MensajeError: "Seleccione una opción válida (1-5)",
            min: 1,
            max: 5
        );

        return (int)criterio;
    }

    static List<string> RealizarBusquedaPorCriterio()
    {
        int criterio = SeleccionarCriterio();
        return RealizarBusquedaIndividual(criterio);
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
        else
        {
          Console.WriteLine(mensajeError);
        }
      }
    }
          //funcion para filtrar el input ingreso por teclado de las opciones numeros enteros
    static double LeerNumero( string Mensaje, string MensajeError, int min = 0 , int max = int.MaxValue)
    {
      while (true)
      {
        Console.Write(Mensaje);
        string? input = Console.ReadLine()?.Trim();

        if (!double.TryParse(input, out double valor) )
        {
          Console.WriteLine(MensajeError);
        } else if ( valor > max || valor < min){
          Console.WriteLine(MensajeError);
        } else {
          return valor;
        }
      }
    }
    //funcion para filtrar el nombre
    static string LeerLetras( string Mensaje, string MensajeError )
    {
      while (true)
      {
        Console.Write(Mensaje);
        string? input = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(input))
          {
            Console.WriteLine(MensajeError);
          } else {
            return input;
          }

      }
    }

    //Función para leer el directorio raíz y todos sus subdirectorios
    static List<string> LeerDirectorio()
    {
        string rutaDefinida = @"C:\TuCarpeta"; //  RECORDAR CAMBIAR POR LA RUTA QUE VAMOS A USAR
    
        try
        {
            // Obtener todos los archivos recursivamente
            string[] archivos = Directory.GetFiles(rutaDefinida, "*.*", SearchOption.AllDirectories);
            
            // Convertir a rutas relativas para mejor visualización
            List<string> archivosRelativos = new List<string>();
            foreach (string archivo in archivos)
            {
                // Obtener ruta relativa desde el directorio base
                string relativa = archivo.Replace(rutaDefinida, "").TrimStart(Path.DirectorySeparatorChar);
                archivosRelativos.Add(relativa);
            }
            
            Console.WriteLine($"✅ Se encontraron {archivosRelativos.Count} archivos en: {rutaDefinida}");
            return archivosRelativos;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al leer el directorio: {ex.Message}");
            return new List<string>();
        }
    }
    
  }
}
