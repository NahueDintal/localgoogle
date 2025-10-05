static void AplicarBusquedaCombinada()
{
    Console.WriteLine(":: BUSQUEDA COMBINADA ::");
    Console.WriteLine("Seleccione el tipo de operación:");
    Console.WriteLine("1. Operadores lógicos básicos");
    Console.WriteLine("2. Cuantificadores lógicos");
    
    double tipoOperacion = LeerNumero(
        mensaje: "Tipo de operación: ",
        mensajeError: "Seleccione 1 o 2",
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
        mensaje: "Seleccione el cuantificador: ",
        mensajeError: "Seleccione una opción válida (1-4)",
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
        mensaje: "Opción: ",
        mensajeError: "Seleccione 1 o 2",
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

// CUANTIFICADOR UNIVERSAL (∀) - "Para todo x, P(x)"
static void AplicarCuantificadorUniversal(List<string> archivos)
{
    Console.WriteLine(":: CUANTIFICADOR UNIVERSAL (∀) ::");
    Console.WriteLine("Verificaremos si TODOS los archivos cumplen con una propiedad");
    
    Console.WriteLine("Seleccione la propiedad a verificar:");
    int propiedad = SeleccionarCriterio();
    
    List<string> archivosQueCumplen = RealizarBusquedaIndividual(propiedad);
    
    // Verificar si todos los archivos del conjunto están en los que cumplen la propiedad
    bool todosCumplen = archivos.All(archivo => archivosQueCumplen.Contains(archivo));
    
    if (todosCumplen)
    {
        Console.WriteLine($"✅ VERDADERO: Todos los {archivos.Count} archivos cumplen la propiedad seleccionada");
    }
    else
    {
        List<string> archivosQueNoCumplen = archivos.Except(archivosQueCumplen).ToList();
        Console.WriteLine($"❌ FALSO: No todos los archivos cumplen la propiedad");
        Console.WriteLine($"Archivos que NO cumplen ({archivosQueNoCumplen.Count}):");
        foreach (string archivo in archivosQueNoCumplen)
        {
            Console.WriteLine($"   - {archivo}");
        }
    }
}

// CUANTIFICADOR EXISTENCIAL (∃) - "Existe al menos un x tal que P(x)"
static void AplicarCuantificadorExistencial(List<string> archivos)
{
    Console.WriteLine(":: CUANTIFICADOR EXISTENCIAL (∃) ::");
    Console.WriteLine("Verificaremos si existe AL MENOS UN archivo que cumple con una propiedad");
    
    Console.WriteLine("Seleccione la propiedad a verificar:");
    int propiedad = SeleccionarCriterio();
    
    List<string> archivosQueCumplen = RealizarBusquedaIndividual(propiedad);
    
    // Verificar si existe al menos un archivo del conjunto que cumple la propiedad
    bool existeAlMenosUno = archivos.Any(archivo => archivosQueCumplen.Contains(archivo));
    
    if (existeAlMenosUno)
    {
        List<string> interseccion = archivos.Intersect(archivosQueCumplen).ToList();
        Console.WriteLine($"✅ VERDADERO: Existen {interseccion.Count} archivo(s) que cumplen la propiedad:");
        foreach (string archivo in interseccion)
        {
            Console.WriteLine($"   - {archivo}");
        }
    }
    else
    {
        Console.WriteLine($"❌ FALSO: No existe ningún archivo que cumpla la propiedad");
    }
}

// CUANTIFICADOR DE EXISTENCIA ÚNICA (∃!) - "Existe exactamente un x tal que P(x)"
static void AplicarCuantificadorExistenciaUnica(List<string> archivos)
{
    Console.WriteLine(":: CUANTIFICADOR DE EXISTENCIA ÚNICA (∃!) ::");
    Console.WriteLine("Verificaremos si existe EXACTAMENTE UN archivo que cumple con una propiedad");
    
    Console.WriteLine("Seleccione la propiedad a verificar:");
    int propiedad = SeleccionarCriterio();
    
    List<string> archivosQueCumplen = RealizarBusquedaIndividual(propiedad);
    
    // Encontrar la intersección entre el conjunto base y los que cumplen la propiedad
    List<string> interseccion = archivos.Intersect(archivosQueCumplen).ToList();
    
    if (interseccion.Count == 1)
    {
        Console.WriteLine($"✅ VERDADERO: Existe exactamente UN archivo que cumple la propiedad:");
        Console.WriteLine($"   - {interseccion[0]}");
    }
    else if (interseccion.Count > 1)
    {
        Console.WriteLine($"❌ FALSO: Existen {interseccion.Count} archivos que cumplen la propiedad, no exactamente uno");
        Console.WriteLine("Archivos que cumplen:");
        foreach (string archivo in interseccion)
        {
            Console.WriteLine($"   - {archivo}");
        }
    }
    else
    {
        Console.WriteLine($"❌ FALSO: No existe ningún archivo que cumpla la propiedad");
    }
}

// CUANTIFICADOR DE CONTEO - "Contar cuántos x cumplen P(x)"
static void AplicarCuantificadorConteo(List<string> archivos)
{
    Console.WriteLine(":: CUANTIFICADOR DE CONTEO ::");
    Console.WriteLine("Contaremos CUÁNTOS archivos cumplen con una propiedad");
    
    Console.WriteLine("Seleccione la propiedad a verificar:");
    int propiedad = SeleccionarCriterio();
    
    List<string> archivosQueCumplen = RealizarBusquedaIndividual(propiedad);
    
    // Contar la intersección entre el conjunto base y los que cumplen la propiedad
    List<string> interseccion = archivos.Intersect(archivosQueCumplen).ToList();
    
    Console.WriteLine($"📊 RESULTADO: {interseccion.Count} de {archivos.Count} archivos cumplen la propiedad");
    
    if (interseccion.Count > 0)
    {
        Console.WriteLine("Archivos que cumplen:");
        foreach (string archivo in interseccion)
        {
            Console.WriteLine($"   - {archivo}");
        }
        
        // Calcular porcentaje
        double porcentaje = (interseccion.Count * 100.0) / archivos.Count;
        Console.WriteLine($"Porcentaje: {porcentaje:F2}%");
    }
}

static int SeleccionarCriterio()
{
    Console.WriteLine("1. Por nombre");
    Console.WriteLine("2. Por tipo");
    Console.WriteLine("3. Por tamaño");
    Console.WriteLine("4. Por contenido");
    Console.WriteLine("5. Por fecha");

    double criterio = LeerNumero(
        mensaje: "Criterio: ",
        mensajeError: "Seleccione una opción válida (1-5)",
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
