using System;
using System.Globalization;

namespace Localgoogle
{
  public class Localgoogle
  {
    static void Main()
    {
      // interface para ingresar el tipo de busqueda
      // por nombre
      // por contenido
      // por tamaño
      // tipo de archivo
      // busqueda combinada

      while(true) 
      {
        Console.WriteLine(":: Bienvenido, ingrese como desea buscar");
        Console.WriteLine(":: 1. Por nombre de archivo.");
        Console.WriteLine(":: 2. Por tipo de archivo.");
        Console.WriteLine(":: 3. Por tamaño.");
        Console.WriteLine(":: 4. Por contenido.");
        Console.WriteLine(":: 5. Busqueda combinada.");
        Console.WriteLine(":: 6. Salir.");

        double opcion = LeerNumero(
            Mensaje: ":: Opcion: ",
            MensajeError: "Se espera un numero dentro de las opciones de busqueda, del 1 al 5."
            );

        if ( opcion == 1 )
        {
          string nombreArchivoBuscado = LeerLetras(
          Mensaje: ":: Ingrese nombre archivo.",
          MensajeError: "no se que signos no pueden ser..."
          );
        }

        if ( opcion == 2 )
        {
          Console.WriteLine("Tipos de archivos.");
          Console.WriteLine("1. texto");
          Console.WriteLine("2. imagen");
          Console.WriteLine("3. video");
          Console.WriteLine("4. programas");
          Console.WriteLine("5. procesos");

          double opcion2 = LeerNumero(
              Mensaje: "Opcion elejida: ",
              MensajeError: "Ingrese un valor dentro de las opciones del tipo de archivo."
              );
          switch ( opcion2 )
          {
            case 1: BuscarTexto(); break;
            case 2: BuscarImagen(); break;
            case 3: BuscarVideo(); break;
            case 4: BuscarPrograma(); break;
            case 5: return;
          }
        }

        if ( opcion == 3 )
        {
          double tamañoArchivo = LeerNumero(
              Mensaje: "Indique el tamaño del archivo: ",
              MensajeError: "Ingrese un numero Natural por favor."
              );




        }

        if ( opcion == 5)
        {
          Console.WriteLine("1. DISYUNCIÓN (OR) - Unir con otro conjunto");
          Console.WriteLine("2. CONJUNCIÓN (AND) - Intersectar con otro conjunto");
          Console.WriteLine("3. DISYUNCIÓN EXCLUSIVA (XOR) - Solo en uno u otro");
          Console.WriteLine("4. NEGACIÓN (NOT) - Excluir criterio");
          Console.WriteLine("5. CUANTIFICADOR UNIVERSAL - Verificar propiedad para todos");
          Console.WriteLine("6. CUANTIFICADOR EXISTENCIAL - Verificar si existe alguno");
        

          double opcion5 = LeerNumero(
              Mensaje: "Opcion: ",
              MensajeError: "Ingrese un valor dentro de las opciones del tipo de busqueda combinada."
              );

          switch (opcion)
          {
              case 1: AplicarDisyuncion(); break;
              case 2: AplicarConjuncion(); break;
              case 3: AplicarDisyuncionExclusiva(); break;
              case 4: AplicarNegacion(); break;
              case 5: AplicarCuantificadorUniversal(); break;
              case 6: AplicarCuantificadorExistencial(); break;
          }
        }
        if ( opcion == 6)
        {
            Console.WriteLine("Saliendo...");
            return;
        }
      } //Fin while
      //proceso de discriminacion de los resultados por disyuncion, conjuncion
      //disyuncion exclusiva, negacion o implicacion, y cuantificadores universales y
      //existencial. con los cuantificadores se me ocurre que puede ser la diferencia
      //entre saber si el archivo existe o no estar seguro que así sea!
      //
      //
      // imprimir resultados

    }
      //funcion para filtrar el input ingreso por teclado de las opciones numeros enteros
    static double LeerNumero( string Mensaje, string MensajeError )
    {
      while (true)
      {
        Console.Write(Mensaje);
        string? input = Console.ReadLine()?.Trim();

        if (!double.TryParse(input, out double valor) || valor <= 0 )
        {
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
  }
}
//
//funcion para leer directorio y generar un vectores de los mismos
//
//funcion para buscar coincidencia del input en el vector 
//
