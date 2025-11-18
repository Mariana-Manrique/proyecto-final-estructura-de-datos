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

   // Implementación corregida en GestorPrincipal.cs para que retorne bool
public bool CrearRestaurante(Restaurante nuevoRestaurante)
{
    // 1. Validar si ya existe
    if (ExisteRestaurante(nuevoRestaurante.Nit))
    {
        Console.WriteLine($"Error: Ya existe un restaurante con el NIT {nuevoRestaurante.Nit}.");
        return false; // Retorna FALSE si hay NIT duplicado
    }
    
    // 2. Otras validaciones (campos vacíos, celular 10 dígitos)
    // Asumo que la propiedad del dueño es 'Dueno' o 'Dueño'
    if (string.IsNullOrWhiteSpace(nuevoRestaurante.Nit) || 
        string.IsNullOrWhiteSpace(nuevoRestaurante.Nombre) || 
        string.IsNullOrWhiteSpace(nuevoRestaurante.Dueno) || 
        nuevoRestaurante.Celular == null || nuevoRestaurante.Celular.Length != 10)
    {
         Console.WriteLine("Error de validación. Revise los campos (NIT, Nombre y Dueño no vacíos, Celular 10 dígitos).");
         return false; // Retorna FALSE si la validación falla
    }

    // 3. Crear el restaurante
    Restaurantes.Agregar(nuevoRestaurante);
    
    // NOTA: Los mensajes de éxito/error se muestran ahora en Program.cs,
    // pero se deja este mensaje por si la función se llama desde otro lugar.
    // Console.WriteLine($"Restaurante '{nuevoRestaurante.Nombre}' creado con éxito."); 
    
    return true; // Retorna TRUE si la creación fue exitosa
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

// Sustituir el método BorrarRestaurante actual en GestorPrincipal.cs
public bool BorrarRestaurante(string nit)
{
    // Usamos BuscarRestaurante (o ObtenerRestaurantePorNit) para encontrar el objeto
    var restaurante = BuscarRestaurante(nit); 
    
    if (restaurante == null)
    {
        // El restaurante no existe, no se puede borrar
        // El mensaje de error lo imprime Program.cs ("No se pudo borrar...")
        return false; 
    }

    // ⭐ REGLA DE NEGOCIO (RF-06): Validar si tiene pedidos pendientes.
    // Esto requiere acceder a la lista de pedidos del restaurante (ColaPedidosPendientes).
    // Asumo que la propiedad es pública y accesible.
    if (!restaurante.ColaPedidosPendientes.EstaVacia()) 
    {
        // El restaurante tiene pedidos pendientes (no vacío), no se puede borrar.
        // El mensaje de error lo imprime Program.cs.
        return false;
    }

    // Si no tiene pedidos pendientes y existe, procedemos a borrarlo de la lista principal.
    // Asumimos que la clase ListaEnlazada<T> tiene un método para eliminar un nodo por su valor.
    if (Restaurantes.EliminarPorValor(restaurante)) 
    {
        // Borrado exitoso
        return true; 
    }
    
    // Falló por alguna razón (e.g., error en la lista enlazada)
    return false;
}

// Añadir este método a la clase GestorPrincipal.cs
public Restaurante BuscarRestaurante(string nit)
{
    // Este método simplemente llama al método ObtenerRestaurantePorNit,
    // que ya contiene la lógica de búsqueda. Esto resuelve el error de
    // compilación en Program.cs sin duplicar la lógica de recorrido.
    return ObtenerRestaurantePorNit(nit);
}

}