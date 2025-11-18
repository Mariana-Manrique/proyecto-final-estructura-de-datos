//namespace Listas.Servicios;

//using Listas.Dominio;
using System;

public class GestorPrincipal
{
    //[cite_start]// Colección principal de restaurantes (Lista Enlazada) [cite: 10]
    public ListaEnlazada<Restaurante> Restaurantes { get; private set; }

    public GestorPrincipal()
    {
        Restaurantes = new ListaEnlazada<Restaurante>();
    }

    // --- Métodos de Gestión de Restaurantes (RF-01) ---

    // La implementación de Agregar en ListaEnlazada no valida duplicados, 
    // por lo que la validación debe ir aquí.
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

    public void CrearRestaurante(Restaurante nuevoRestaurante)
    {
        if (ExisteRestaurante(nuevoRestaurante.Nit))
        {
            Console.WriteLine($"Error: Ya existe un restaurante con el NIT {nuevoRestaurante.Nit}."); // [cite: 19]
            return;
        }
        
        // Otras validaciones (campos vacíos, celular 10 dígitos)
        if (string.IsNullOrWhiteSpace(nuevoRestaurante.Nit) || nuevoRestaurante.Celular.Length != 10) // [cite: 19]
        {
             Console.WriteLine("Error de validación. Revise los campos (NIT no vacío, Celular 10 dígitos).");
             return;
        }


        Restaurantes.Agregar(nuevoRestaurante);
        Console.WriteLine($"Restaurante '{nuevoRestaurante.Nombre}' creado con éxito.");
    }
    
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
        // Usa el método ToString() del Cliente para una salida limpia
        Console.WriteLine($"{indice}. {actual.Valor}"); 
        actual = actual.Siguiente;
        indice++;
    }
}
    public void ListarRestaurantes()
{
    Console.WriteLine("\n--- LISTA DE RESTAURANTES REGISTRADOS ---");
    // Verificamos si la lista está vacía
    if (Restaurantes.Cabeza == null)
    {
        Console.WriteLine("No hay restaurantes registrados.");
        return;
    }

    var actual = Restaurantes.Cabeza;
    int indice = 1;
    // Recorremos la lista enlazada
    while (actual != null)
    {
        var r = actual.Valor;
        // Imprimimos la información clave del Restaurante
        Console.WriteLine($"{indice}. NIT: {r.Nit} | Nombre: {r.Nombre} | Dueño: {r.Dueno} | Teléfono: {r.Celular}");
        actual = actual.Siguiente; // Pasamos al siguiente nodo
        indice++;
    }
}
    // Aquí irían los métodos para Editar/Listar, que son similares
    public void EditarRestaurante(string nit, string nuevoNom, string nuevoDueno, string nuevoCel, string nuevaDir)
{
    var restaurante = ObtenerRestaurantePorNit(nit);
    
    // Aunque Program.cs ya valida si es null, este check es por seguridad.
    if (restaurante == null)
    {
        Console.WriteLine($"Error: Restaurante con NIT {nit} no encontrado.");
        return;
    }
    
    // Actualizar campos solo si el usuario ingresó un valor (no dejó vacío)
    if (!string.IsNullOrWhiteSpace(nuevoNom))
        restaurante.Nombre = nuevoNom;
        
    if (!string.IsNullOrWhiteSpace(nuevoDueno))
        restaurante.Dueno = nuevoDueno; // Asegúrate de que la propiedad en Restaurante.cs sea 'Dueno' o 'Dueño'
    
    // Validamos el Celular (10 dígitos) antes de actualizar
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

public void BorrarRestaurante(string nit)
{
    var restaurante = ObtenerRestaurantePorNit(nit);
    
    if (restaurante == null)
    {
        Console.WriteLine($"Error: Restaurante con NIT {nit} no encontrado.");
        return;
    }

    // ⭐ REGLA DE NEGOCIO (RF-06): Antes de borrar, se debe validar que
    // el restaurante no tenga pedidos PENDIENTES.
    // **Nota:** Como no tenemos acceso al GestorPedidos aquí de forma simple, 
    // asumiremos temporalmente que se puede borrar, o implementamos una validación simple 
    // si el GestorPrincipal tiene la lógica de Pedidos (lo cual no parece ser el caso 
    // según el patrón de diseño).
    
    // **Si tienes un GestorPedidos, la implementación ideal sería:**
    /*
    if (_gestorPedidos.TienePedidosPendientes(nit))
    {
        Console.WriteLine($"Error: El restaurante '{restaurante.Nombre}' no puede borrarse. Tiene pedidos pendientes.");
        return;
    }
    */
    
    // Asumiendo que tu ListaEnlazada<T> tiene un método para eliminar un nodo por su valor:
    // (Si no tienes este método, necesitas agregarlo a ListaEnlazada.cs)
    if (Restaurantes.EliminarPorValor(restaurante)) 
    {
        Console.WriteLine($"Restaurante '{restaurante.Nombre}' (NIT: {nit}) borrado con éxito.");
    }
    else
    {
        // Esto podría ocurrir si EliminarPorValor falla o la lista no está bien implementada.
        Console.WriteLine($"Error al intentar borrar el restaurante con NIT: {nit}.");
    }
}

}