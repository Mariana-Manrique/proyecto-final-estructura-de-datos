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
    
    // Aquí irían los métodos para Editar/Listar, que son similares
}