
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
}