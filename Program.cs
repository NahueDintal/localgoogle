using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Localgoogle
{
    public class Localgoogle
    {
        // Clase para representar archivos con metadata
        public class Archivo
        {
            public string Nombre { get; set; }
            public string RutaCompleta { get; set; }
            public string Extension { get; set; }
            public long Tamaño { get; set; }
            public DateTime FechaModificacion { get; set; }
        }

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

            Console.WriteLine("Seleccione el conjunto base de archivos:");
            List<Archivo> conjuntoBase = ObtenerConjuntoBase();

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

        static List<Archivo> ObtenerConjuntoBase()
        {
            Console.WriteLine("¿Cómo desea definir el conjunto base?");
            Console.WriteLine("1. Todos los archivos del directorio de prueba");
            Console.WriteLine("2. Por criterio de búsqueda específico");
            
            double opcion = LeerNumero(
                Mensaje: "Opción: ",
                MensajeError: "Seleccione 1 o 2",
                min: 1,
                max: 2
            );

            // Ruta relativa a la carpeta de prueba
            string rutaPrueba = @"./carpetadeprueba";

            if (opcion == 1)
            {
                return LeerDirectorio(rutaPrueba);
            }
            else
            {
                return RealizarBusquedaPorCriterio(rutaPrueba);
            }
        }

        // CUANTIFICADOR UNIVERSAL (∀) - "Para todo x, P(x)"
        static void AplicarCuantificadorUniversal(List<Archivo> archivos)
        {
            Console.WriteLine(":: CUANTIFICADOR UNIVERSAL (∀) ::");
            Console.WriteLine("Verificar si TODOS los archivos cumplen una propiedad");
            
            int criterio = SeleccionarCriterio();
            var propiedad = DefinirPropiedad(criterio);
            
            bool todosCumplen = archivos.All(propiedad);
            
            Console.WriteLine($"\n📊 RESULTADO CUANTIFICADOR UNIVERSAL:");
            Console.WriteLine($"∀x P(x) = {todosCumplen}");
            Console.WriteLine(todosCumplen 
                ? "✅ TODOS los archivos cumplen la propiedad P(x)" 
                : "❌ NO todos los archivos cumplen la propiedad P(x)");
        }

        // CUANTIFICADOR EXISTENCIAL (∃) - "Existe al menos un x tal que P(x)"
        static void AplicarCuantificadorExistencial(List<Archivo> archivos)
        {
            Console.WriteLine(":: CUANTIFICADOR EXISTENCIAL (∃) ::");
            Console.WriteLine("Verificar si EXISTE AL MENOS UN archivo que cumple una propiedad");
            
            int criterio = SeleccionarCriterio();
            var propiedad = DefinirPropiedad(criterio);
            
            bool existeAlMenosUno = archivos.Any(propiedad);
            var archivosQueCumplen = archivos.Where(propiedad).ToList();
            
            Console.WriteLine($"\n📊 RESULTADO CUANTIFICADOR EXISTENCIAL:");
            Console.WriteLine($"∃x P(x) = {existeAlMenosUno}");
            Console.WriteLine(existeAlMenosUno 
                ? $"✅ EXISTEN {archivosQueCumplen.Count} archivos que cumplen P(x)" 
                : "❌ NO EXISTE ningún archivo que cumpla P(x)");
            
            if (existeAlMenosUno)
            {
                Console.WriteLine("Archivos que cumplen:");
                foreach (var archivo in archivosQueCumplen)
                {
                    Console.WriteLine($"  📄 {archivo.RutaCompleta}");
                }
            }
        }

        // CUANTIFICADOR DE EXISTENCIA ÚNICA (∃!) - "Existe exactamente un x tal que P(x)"
        static void AplicarCuantificadorExistenciaUnica(List<Archivo> archivos)
        {
            Console.WriteLine(":: CUANTIFICADOR EXISTENCIA ÚNICA (∃!) ::");
            Console.WriteLine("Verificar si EXISTE EXACTAMENTE UN archivo que cumple una propiedad");
            
            int criterio = SeleccionarCriterio();
            var propiedad = DefinirPropiedad(criterio);
            
            var archivosQueCumplen = archivos.Where(propiedad).ToList();
            bool existeExactamenteUno = archivosQueCumplen.Count == 1;
            
            Console.WriteLine($"\n📊 RESULTADO CUANTIFICADOR EXISTENCIA ÚNICA:");
            Console.WriteLine($"∃!x P(x) = {existeExactamenteUno}");
            Console.WriteLine(existeExactamenteUno 
                ? $"✅ EXISTE EXACTAMENTE 1 archivo que cumple P(x)" 
                : $"❌ NO existe exactamente uno. Hay {archivosQueCumplen.Count} archivos que cumplen P(x)");
            
            if (existeExactamenteUno)
            {
                Console.WriteLine($"Archivo único: {archivosQueCumplen[0].RutaCompleta}");
            }
        }

        // CUANTIFICADOR DE CONTEO - "Contar cuántos x cumplen P(x)"
        static void AplicarCuantificadorConteo(List<Archivo> archivos)
        {
            Console.WriteLine(":: CUANTIFICADOR DE CONTEO ::");
            Console.WriteLine("Contar CUÁNTOS archivos cumplen una propiedad");
            
            int criterio = SeleccionarCriterio();
            var propiedad = DefinirPropiedad(criterio);
            
            var archivosQueCumplen = archivos.Where(propiedad).ToList();
            int conteo = archivosQueCumplen.Count;
            
            Console.WriteLine($"\n📊 RESULTADO CUANTIFICADOR DE CONTEO:");
            Console.WriteLine($"|{{x : P(x)}}| = {conteo}");
            Console.WriteLine($"Hay {conteo} archivos que cumplen la propiedad P(x)");
            
            if (conteo > 0)
            {
                Console.WriteLine("Archivos que cumplen:");
                foreach (var archivo in archivosQueCumplen)
                {
                    Console.WriteLine($"  📄 {archivo.RutaCompleta}");
                }
            }
        }

        static void AplicarOperadoresLogicos()
        {
            Console.WriteLine(":: OPERADORES LÓGICOS BÁSICOS ::");
            Console.WriteLine("1. CONJUNCIÓN (AND) - P(x) ∧ Q(x)");
            Console.WriteLine("2. DISYUNCIÓN (OR) - P(x) ∨ Q(x)");
            Console.WriteLine("3. NEGACIÓN (NOT) - ¬P(x)");
            
            double opcion = LeerNumero(
                Mensaje: "Seleccione operador: ",
                MensajeError: "Seleccione 1-3",
                min: 1,
                max: 3
            );

            string rutaPrueba = @"./carpetadeprueba";
            List<Archivo> todosArchivos = LeerDirectorio(rutaPrueba);

            switch (opcion)
            {
                case 1: // AND
                    AplicarConjuncion(todosArchivos);
                    break;
                case 2: // OR
                    AplicarDisyuncion(todosArchivos);
                    break;
                case 3: // NOT
                    AplicarNegacion(todosArchivos);
                    break;
            }
        }

        static void AplicarConjuncion(List<Archivo> archivos)
        {
            Console.WriteLine(":: CONJUNCIÓN LÓGICA (AND) ::");
            Console.WriteLine("Archivos que cumplen P(x) ∧ Q(x)");
            
            Console.WriteLine("Defina la primera propiedad P(x):");
            int criterio1 = SeleccionarCriterio();
            var propiedad1 = DefinirPropiedad(criterio1);
            
            Console.WriteLine("Defina la segunda propiedad Q(x):");
            int criterio2 = SeleccionarCriterio();
            var propiedad2 = DefinirPropiedad(criterio2);
            
            // P(x) ∧ Q(x) = P(x) AND Q(x)
            var resultado = archivos.Where(a => propiedad1(a) && propiedad2(a)).ToList();
            
            Console.WriteLine($"\n📊 RESULTADO CONJUNCIÓN:");
            Console.WriteLine($"|{{x : P(x) ∧ Q(x)}}| = {resultado.Count}");
            
            foreach (var archivo in resultado)
            {
                Console.WriteLine($"  📄 {archivo.RutaCompleta}");
            }
        }

        static void AplicarDisyuncion(List<Archivo> archivos)
        {
            Console.WriteLine(":: DISYUNCIÓN LÓGICA (OR) ::");
            Console.WriteLine("Archivos que cumplen P(x) ∨ Q(x)");
            
            Console.WriteLine("Defina la primera propiedad P(x):");
            int criterio1 = SeleccionarCriterio();
            var propiedad1 = DefinirPropiedad(criterio1);
            
            Console.WriteLine("Defina la segunda propiedad Q(x):");
            int criterio2 = SeleccionarCriterio();
            var propiedad2 = DefinirPropiedad(criterio2);
            
            // P(x) ∨ Q(x) = P(x) OR Q(x)
            var resultado = archivos.Where(a => propiedad1(a) || propiedad2(a)).ToList();
            
            Console.WriteLine($"\n📊 RESULTADO DISYUNCIÓN:");
            Console.WriteLine($"|{{x : P(x) ∨ Q(x)}}| = {resultado.Count}");
            
            foreach (var archivo in resultado)
            {
                Console.WriteLine($"  📄 {archivo.RutaCompleta}");
            }
        }

        static void AplicarNegacion(List<Archivo> archivos)
        {
            Console.WriteLine(":: NEGACIÓN LÓGICA (NOT) ::");
            Console.WriteLine("Archivos que cumplen ¬P(x)");
            
            int criterio = SeleccionarCriterio();
            var propiedad = DefinirPropiedad(criterio);
            
            // ¬P(x) = NOT P(x)
            var resultado = archivos.Where(a => !propiedad(a)).ToList();
            
            Console.WriteLine($"\n📊 RESULTADO NEGACIÓN:");
            Console.WriteLine($"|{{x : ¬P(x)}}| = {resultado.Count}");
            Console.WriteLine($"Archivos que NO cumplen la propiedad:");
            
            foreach (var archivo in resultado)
            {
                Console.WriteLine($"  📄 {archivo.RutaCompleta}");
            }
        }

        // FUNCIONES AUXILIARES PARA PROPIEDADES
        static Func<Archivo, bool> DefinirPropiedad(int criterio)
        {
            switch (criterio)
            {
                case 1: // Por nombre
                    Console.Write("Ingrese nombre o parte del nombre: ");
                    string nombre = Console.ReadLine();
                    return a => a.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase);
                
                case 2: // Por tipo
                    Console.Write("Ingrese extensión (ej: .jpg, .txt): ");
                    string extension = Console.ReadLine().ToLower();
                    return a => a.Extension.Equals(extension, StringComparison.OrdinalIgnoreCase);
                
                case 3: // Por tamaño
                    Console.Write("Ingrese tamaño mínimo en bytes: ");
                    long tamaño = long.Parse(Console.ReadLine());
                    return a => a.Tamaño >= tamaño;
                
                case 4: // Por contenido (simulado)
                    Console.Write("Ingrese texto a buscar en nombre: ");
                    string texto = Console.ReadLine();
                    return a => a.Nombre.Contains(texto, StringComparison.OrdinalIgnoreCase);
                
                case 5: // Por fecha
                    DateTime fecha = LeerFecha("Ingrese fecha (dd/MM/yyyy): ", "Formato inválido");
                    return a => a.FechaModificacion >= fecha;
                
                default:
                    return a => true;
            }
        }

        static int SeleccionarCriterio()
        {
            Console.WriteLine("Seleccione criterio de búsqueda:");
            Console.WriteLine("1. Por nombre");
            Console.WriteLine("2. Por tipo/extensión");
            Console.WriteLine("3. Por tamaño");
            Console.WriteLine("4. Por contenido (en nombre)");
            Console.WriteLine("5. Por fecha");

            double criterio = LeerNumero(
                Mensaje: "Criterio: ",
                MensajeError: "Seleccione 1-5",
                min: 1,
                max: 5
            );

            return (int)criterio;
        }

        static List<Archivo> RealizarBusquedaPorCriterio(string rutaBase)
        {
            int criterio = SeleccionarCriterio();
            List<Archivo> todosArchivos = LeerDirectorio(rutaBase);
            var propiedad = DefinirPropiedad(criterio);
            
            return todosArchivos.Where(propiedad).ToList();
        }

        // FUNCIÓN MEJORADA PARA LEER DIRECTORIO
        static List<Archivo> LeerDirectorio(string ruta)
        {
            var archivos = new List<Archivo>();
            
            try
            {
                if (!Directory.Exists(ruta))
                {
                    // Intentar con ruta absoluta si la relativa no funciona
                    ruta = Path.Combine(Directory.GetCurrentDirectory(), "carpetadeprueba");
                    if (!Directory.Exists(ruta))
                    {
                        Console.WriteLine($"❌ No se encontró la carpeta: {ruta}");
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
                
                Console.WriteLine($"✅ Se encontraron {archivos.Count} archivos en: {ruta}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al leer directorio: {ex.Message}");
            }
            
            return archivos;
        }

        // FUNCIONES DE UTILIDAD (las que ya tenías)
        static DateTime LeerFecha(string mensaje, string mensajeError)
        {
            while (true)
            {
                Console.Write(mensaje);
                string input = Console.ReadLine()?.Trim();
                
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
                string input = Console.ReadLine()?.Trim();

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
                string input = Console.ReadLine()?.Trim();
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
