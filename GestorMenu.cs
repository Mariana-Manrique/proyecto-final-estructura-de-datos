using System;

public class GestorMenu
{
    //Guardamos una referencia al gestor principal para poder acceder a los restaurantes y sus menús
    private GestorPrincipal _gestorPrincipal;
    
    public GestorMenu(GestorPrincipal gestorPrincipal)
    {
        _gestorPrincipal = gestorPrincipal;
    }

    // Método para borrar un plato de manera segura, verificando que no esté referenciado en pedidos pendientes
    public bool BorrarPlatoSeguro(string nitRestaurante, string codigoPlato)
    {
        var restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nitRestaurante);
        if (restaurante == null)
        {
            Console.WriteLine("Error: Restaurante no encontrado.");
            return false;
        }

        Plato platoABorrar = null;
        int posicion = -1;

        var actual = restaurante.Menu.Cabeza;
        int contador = 0;
        while (actual != null)
        {
            if (actual.Valor.Codigo == codigoPlato)
            {
                platoABorrar = actual.Valor;
                posicion = contador;
                break;
            }
            actual = actual.Siguiente;
            contador++;
        }

        if (platoABorrar == null)
        {
            Console.WriteLine($"Error: Plato con código {codigoPlato} no encontrado.");
            return false;
        }

        if (EstaReferenciadoEnPedidosPendientes(restaurante, codigoPlato))
        {
            Console.WriteLine("¡Borrado Bloqueado! El plato está referenciado en pedidos PENDIENTES.");
            return false;
        }

        restaurante.Menu.EliminarPosicion(posicion);

        Console.WriteLine($"Plato '{platoABorrar.Nombre}' borrado con éxito.");
        return true;
    }
    
    // Método auxiliar para verificar si un plato está referenciado en pedidos pendientes
    private bool EstaReferenciadoEnPedidosPendientes(Restaurante restaurante, string codigoPlato)
    {
       
        var nodoPedidoActual = restaurante.ColaPedidosPendientes.Cabeza; 
        
        while (nodoPedidoActual != null)
        {
            var pedido = nodoPedidoActual.Valor;
            
            var nodoPlatoActual = pedido.Platos.Cabeza;
            while (nodoPlatoActual != null)
            {
                if (nodoPlatoActual.Valor.CodigoPlato == codigoPlato)
                {
                    return true; 
                }
                nodoPlatoActual = nodoPlatoActual.Siguiente;
            }
            
            nodoPedidoActual = nodoPedidoActual.Siguiente;
        }
        
        return false;
    }

    // Método para listar los platos del menú de un restaurante
    public void ListarPlatos(Restaurante restaurante)
{
    Console.Clear();
    Console.WriteLine($"\n--- MENÚ DE PLATOS de {restaurante.Nombre} ---");
    
    if (restaurante.Menu.Cabeza == null)
    {
        Console.WriteLine("El menú está vacío. No hay platos registrados.");
        return;
    }
    
    var actual = restaurante.Menu.Cabeza;
    int indice = 1;
    while (actual != null)
    {
        var p = actual.Valor;

        Console.WriteLine($"{indice}. [{p.Codigo}] {p.Nombre} - ${p.Precio:N2}"); 
        Console.WriteLine($"   Descripción: {p.Descripcion}");
        actual = actual.Siguiente;
        indice++;
    }
}

// Método para editar un plato del menú
public bool EditarPlato(string nitRestaurante, string codigoPlato, string nuevoNombre, string nuevaDescripcion, decimal nuevoPrecio)
{
    var restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nitRestaurante);
    if (restaurante == null)
    {
        Console.WriteLine("Error: Restaurante no encontrado.");
        return false;
    }

    var actual = restaurante.Menu.Cabeza;
    Plato platoAEditar = null;
    while (actual != null)
    {
        if (actual.Valor.Codigo == codigoPlato)
        {
            platoAEditar = actual.Valor;
            break;
        }
        actual = actual.Siguiente;
    }

    if (platoAEditar == null)
    {
        Console.WriteLine($"Error: Plato con código {codigoPlato} no encontrado.");
        return false;
    }

    if (nuevoPrecio <= 0) 
    {
        Console.WriteLine("Error de validación. El precio debe ser mayor a 0."); //
        return false;
    }

    platoAEditar.Nombre = nuevoNombre;
    platoAEditar.Descripcion = nuevaDescripcion;
    platoAEditar.Precio = nuevoPrecio;

    Console.WriteLine($"Plato {codigoPlato} editado con éxito.");
    return true;
}

// Método para crear un nuevo plato desde consola
public void CrearPlatoDesdeConsola(Restaurante restaurante)
{
    Console.Clear();
    Console.WriteLine($"\n-- CREAR NUEVO PLATO para el menú de {restaurante.Nombre} --");

    Console.Write("Ingrese Código del plato (único): ");
    string codigo = Console.ReadLine()?.Trim();
    
    if (string.IsNullOrWhiteSpace(codigo) || ExistePlato(restaurante, codigo)) 
    {
        string mensaje = string.IsNullOrWhiteSpace(codigo) ? 
                         "Error: El código no puede estar vacío." : 
                         $"Error: Ya existe un plato con el código {codigo} en el menú.";
        Console.WriteLine(mensaje);
        return;
    }

    Console.Write("Ingrese Nombre del plato: ");
    string nombre = Console.ReadLine()?.Trim();
    
    Console.Write("Ingrese Descripción del plato: ");
    string descripcion = Console.ReadLine()?.Trim();
    
    Console.Write("Ingrese Precio del plato (debe ser > 0): ");
    string precioStr = Console.ReadLine()?.Trim();

    // Validar que el precio sea un número positivo
    if (!decimal.TryParse(precioStr, out decimal precio) || precio <= 0)
    {
        Console.WriteLine("Error de validación: El precio debe ser un número positivo (> 0).");
        return;
    }

    if (string.IsNullOrWhiteSpace(nombre))
    {
        Console.WriteLine("Error: El nombre del plato no puede estar vacío.");
        return;
    }
    
    var nuevoPlato = new Plato(codigo, nombre, descripcion, precio);
    restaurante.Menu.Agregar(nuevoPlato); 

    Console.WriteLine($"Plato '{nombre}' creado y agregado al menú con éxito.");
}

// Método para verificar si un plato con el código dado ya existe en el menú
public bool ExistePlato(Restaurante restaurante, string codigo)
{
    var actual = restaurante.Menu.Cabeza;
    while (actual != null)
    {
        if (actual.Valor.Codigo == codigo)
        {
            return true;
        }
        actual = actual.Siguiente;
    }
    return false;
}

}