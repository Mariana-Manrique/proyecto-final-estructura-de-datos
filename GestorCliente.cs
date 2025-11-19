
using System;

public class GestorCliente
{
    //Guardamos una referencia al gestor principal para poder acceder a los restaurantes y sus listas de clientes
    private GestorPrincipal _gestorPrincipal;

    //Constructor que recibe el gestor principal y lo asigna
    
    public GestorCliente(GestorPrincipal gestorPrincipal)
    {
        _gestorPrincipal = gestorPrincipal;
    }

    // este metodo elimina un cliente si solo cumple con la regla de que no debe tener pedidos pendientes
    public bool BorrarClienteSeguro(string nitRestaurante, string cedulaCliente)
    {
        var restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nitRestaurante);
        if (restaurante == null)
        {
            Console.WriteLine("Error: Restaurante no encontrado.");
            return false;
        }
        // variable para guardar el cliente que vamos a borrar y su posición en la lista enlazada
        // recorremos la lista enlazada de clientes del restaurante
        Cliente clienteABorrar = null;
        int posicion = -1;
        
        // este metodo ayuda a buscar el cliente y su posición en la lista enlazada
        var actual = restaurante.Clientes.Cabeza;
        int contador = 0;
        while (actual != null)
        {
            // si encontramos el cliente con la cédula buscada
            if (actual.Valor.Cedula == cedulaCliente)
            {
                clienteABorrar = actual.Valor;
                posicion = contador;
                break;
            }
            actual = actual.Siguiente;
            contador++;
        }
        // si no se encontro el cliente, mostramos un mensaje de error
        if (clienteABorrar == null)
        {
            Console.WriteLine($"Error: Cliente con cédula {cedulaCliente} no encontrado.");
            return false;
        }

        // regal de borrado seguro, no se puede borrar un cliente si tiene de pedidos pendientes
        if (clienteABorrar.TienePedidosPendientes()) 
        {
            Console.WriteLine("¡Borrado Bloqueado! El cliente tiene pedidos PENDIENTES.");
            return false;
        }

        // si paso las validaciones borramos el cliente usando el metetodo de la losta elnzada que elimina por posicion 
        
        restaurante.Clientes.EliminarPosicion(posicion);

        Console.WriteLine($"Cliente con cédula {cedulaCliente} borrado con éxito.");
        return true;
    }

// metodo para editar un cliente 
//permite cambiar los datos basicos del cliente con validaciones

public bool EditarCliente(string nitRestaurante, string cedulaCliente, string nuevoNombre, string nuevoCelular, string nuevoEmail)
{
    // busca el resturante 
    var restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nitRestaurante);
    if (restaurante == null)
    {
        Console.WriteLine("Error: Restaurante no encontrado.");
        return false;
    }

    // Buscar el cliente dentro de la lista elanzada
    var actual = restaurante.Clientes.Cabeza;
    Cliente clienteAEditar = null;
    while (actual != null)
    {
        if (actual.Valor.Cedula == cedulaCliente)
        {
            clienteAEditar = actual.Valor;
            break;
        }
        actual = actual.Siguiente;
    }

    if (clienteAEditar == null)

    // si no se encontro el cliente, mostramos un mensaje de error
    {
        Console.WriteLine($"Error: Cliente con cédula {cedulaCliente} no encontrado.");
        return false;
    }

    // las validaciones basicas deben ser que el nombre no este vacio y el celular tenga 10 digitos
    if (string.IsNullOrWhiteSpace(nuevoNombre) || nuevoCelular.Length != 10) 
    {
        Console.WriteLine("Error de validación. Nombre no puede ser vacío y Celular debe tener 10 dígitos.");
        return false;
    }

    // asignar los nuevos datos al cliente
    clienteAEditar.NombreCompleto = nuevoNombre;
    clienteAEditar.Celular = nuevoCelular;
    clienteAEditar.Email = nuevoEmail;

    Console.WriteLine($"Cliente {cedulaCliente} editado con éxito.");
    return true;
}

// Método para crear un nuevo cliente usando el NIT del restaurante
public void CrearClienteDesdeConsola(string nitRestaurante)
{
    Console.Clear();
    Console.WriteLine("\n-- CREAR NUEVO CLIENTE --");
    
    var restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nitRestaurante);
    if (restaurante == null)
    {
        Console.WriteLine("Error interno: Restaurante no encontrado.");
        
        return;
    }

    
    Console.Write("Ingrese Cédula del cliente (única): ");
    string cedula = Console.ReadLine()?.Trim();
    
    // Validacion de la cedula si esta vacía o ya existente
    if (string.IsNullOrWhiteSpace(cedula))
    {
        Console.WriteLine("Error: La cédula no puede estar vacía.");
        return;
    }
    
    if (ExisteCliente(restaurante, cedula)) 
    {
        Console.WriteLine($"Error: Ya existe un cliente con la cédula {cedula} en este restaurante.");
        return;
    }

    Console.Write("Ingrese Nombre completo: ");
    string nombre = Console.ReadLine()?.Trim();
    
    Console.Write("Ingrese Celular (10 dígitos): ");
    string celular = Console.ReadLine()?.Trim();
    
    Console.Write("Ingrese Email: ");
    string email = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(celular) || celular.Length != 10)
    {
        Console.WriteLine("Error: Nombre no puede ser vacío, y Celular debe tener 10 dígitos.");
        return;
    }
    
    // se crea y edita los cliente
    var nuevoCliente = new Cliente(cedula, nombre, celular, email);
    restaurante.Clientes.Agregar(nuevoCliente); 

    Console.WriteLine($"Cliente '{nombre}' con cédula {cedula} creado y asociado a {restaurante.Nombre} con éxito.");
}
// Método para verificar si un cliente con la cédula dada ya existe en el restaurante
public bool ExisteCliente(Restaurante restaurante, string cedula)
{
    var actual = restaurante.Clientes.Cabeza;
    while (actual != null)
    {
        if (actual.Valor.Cedula == cedula)
        {
            return true;
        }
        actual = actual.Siguiente;
    }
    return false;
}
// Método para crear un nuevo cliente desde consola 
public void CrearClienteDesdeConsola(Restaurante restaurante)
{
    Console.Clear();
    Console.WriteLine($"\n-- CREAR NUEVO CLIENTE para {restaurante.Nombre} --");
    
    Console.Write("Ingrese Cédula del cliente (única): ");
    string cedula = Console.ReadLine()?.Trim();
    
    if (string.IsNullOrWhiteSpace(cedula) || ExisteCliente(restaurante, cedula)) 
    {
        string mensaje = string.IsNullOrWhiteSpace(cedula) ? 
                         "Error: La cédula no puede estar vacía." : 
                         $"Error: Ya existe un cliente con la cédula {cedula} en este restaurante.";
        Console.WriteLine(mensaje);
        return;
    }

    Console.Write("Ingrese Nombre completo: ");
    string nombre = Console.ReadLine()?.Trim();
    
    Console.Write("Ingrese Celular (10 dígitos): ");
    string celular = Console.ReadLine()?.Trim();
    
    Console.Write("Ingrese Email: ");
    string email = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(celular) || celular.Length != 10)
    {
        Console.WriteLine("Error: Nombre no puede ser vacío, y Celular debe tener 10 dígitos.");
        return;
    }
    
    var nuevoCliente = new Cliente(cedula, nombre, celular, email);
    restaurante.Clientes.Agregar(nuevoCliente); 

    Console.WriteLine($"Cliente '{nombre}' creado y asociado con éxito.");
}
// Método para listar clientes de un restaurante
public void ListarClientes(Restaurante restaurante)
{
    Console.Clear();
    Console.WriteLine($"\n--- LISTA DE CLIENTES de {restaurante.Nombre} ({restaurante.Clientes.Cantidad} en total) ---");
    
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

}