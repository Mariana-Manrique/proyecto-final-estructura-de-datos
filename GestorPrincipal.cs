using System;

public class GestorPrincipal
{
    // Lista principal de restaurantes
    public ListaEnlazada<Restaurante> Restaurantes { get; private set; }

    // Constructor que inicializa la lista de restaurantes
    public GestorPrincipal()
    {
        Restaurantes = new ListaEnlazada<Restaurante>();
    }

    // Método para verificar si un restaurante con el NIT dado ya existe
    public bool ExisteRestaurante(string nit)
    {
        var actual = Restaurantes.Cabeza;
        while (actual != null)
        {
            if (actual.Valor.Nit == nit)
            {
                return true;
            }
            actual = actual.Siguiente;
        }
        return false;
    }

    // Método para crear un nuevo restaurante 
    public bool CrearRestaurante(Restaurante nuevoRestaurante)
{
    // Validar si ya existe
    if (ExisteRestaurante(nuevoRestaurante.Nit))
    {
        Console.WriteLine($"Error: Ya existe un restaurante con el NIT {nuevoRestaurante.Nit}.");
        return false; // Retorna FALSE si hay NIT duplicado
    }
    
    if (string.IsNullOrWhiteSpace(nuevoRestaurante.Nit) || 
        string.IsNullOrWhiteSpace(nuevoRestaurante.Nombre) || 
        string.IsNullOrWhiteSpace(nuevoRestaurante.Dueno) || 
        nuevoRestaurante.Celular == null || nuevoRestaurante.Celular.Length != 10)
    {
         Console.WriteLine("Error de validación. Revise los campos (NIT, Nombre y Dueño no vacíos, Celular 10 dígitos).");
         return false; 
    }

    // Crear el restaurante
    Restaurantes.Agregar(nuevoRestaurante);
    return true; 
}

    // Método para obtener un restaurante por su NIT
    public Restaurante ObtenerRestaurantePorNit(string nit)
    {
        var actual = Restaurantes.Cabeza;
        while (actual != null)
        {
            if (actual.Valor.Nit == nit)
            {
                return actual.Valor;
            }
            actual = actual.Siguiente;
        }
        return null;
    }

    // Método para listar todos los clientes de un restaurante
    public void ListarClientes(Restaurante restaurante)
{
    Console.WriteLine($"\n--- LISTA DE CLIENTES ({restaurante.Clientes.Cantidad} en total) ---");
    if (restaurante.Clientes.Cabeza == null)
    {
        Console.WriteLine("No hay clientes registrados.");
        return;
    }
    
    var actual = restaurante.Clientes.Cabeza;
    int indice = 1;
    while (actual != null)
    {
        Console.WriteLine($"{indice}. {actual.Valor}"); 
        actual = actual.Siguiente;
        indice++;
    }
}
    public void ListarRestaurantes()
{
    Console.WriteLine("\n--- LISTA DE RESTAURANTES REGISTRADOS ---");
    
    if (Restaurantes.Cabeza == null)
    {
        Console.WriteLine("No hay restaurantes registrados.");
        return;
    }

    var actual = Restaurantes.Cabeza;
    int indice = 1;
    
    while (actual != null)
    {
        var r = actual.Valor;
    
        Console.WriteLine($"{indice}. NIT: {r.Nit} | Nombre: {r.Nombre} | Dueño: {r.Dueno} | Teléfono: {r.Celular}");
        actual = actual.Siguiente; // Pasamos al siguiente nodo
        indice++;
    }
}
    // Método para editar los datos de un restaurante
    public void EditarRestaurante(string nit, string nuevoNom, string nuevoDueno, string nuevoCel, string nuevaDir)
{
    var restaurante = ObtenerRestaurantePorNit(nit);
    
    if (restaurante == null)
    {
        Console.WriteLine($"Error: Restaurante con NIT {nit} no encontrado.");
        return;
    }
    
    if (!string.IsNullOrWhiteSpace(nuevoNom))
        restaurante.Nombre = nuevoNom;
        
    if (!string.IsNullOrWhiteSpace(nuevoDueno))
        restaurante.Dueno = nuevoDueno; 
    
    if (!string.IsNullOrWhiteSpace(nuevoCel))
    {
        if (nuevoCel.Length != 10)
        {
            Console.WriteLine("Error de validación: El nuevo celular debe tener 10 dígitos. Valor no actualizado.");
        }
        else
        {
            restaurante.Celular = nuevoCel;
        }
    }
        
    if (!string.IsNullOrWhiteSpace(nuevaDir))
        restaurante.Direccion = nuevaDir;

    Console.WriteLine($"Restaurante con NIT {nit} actualizado con éxito.");
}

    // Método para borrar un restaurante por su NIT
    public bool BorrarRestaurante(string nit)
{
    var restaurante = BuscarRestaurante(nit); 
    
    if (restaurante == null)
    {
        return false; 
    }

    if (!restaurante.ColaPedidosPendientes.EstaVacia()) 
    {
        return false;
    }

    if (Restaurantes.EliminarPorValor(restaurante)) 
    {
        return true; 
    }
    
    return false;
}

// Método auxiliar para buscar un restaurante por NIT
public Restaurante BuscarRestaurante(string nit)
{
    return ObtenerRestaurantePorNit(nit);
}

}