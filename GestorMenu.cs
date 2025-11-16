
using System;

public class GestorMenu
{
    private GestorPrincipal _gestorPrincipal;
    
    public GestorMenu(GestorPrincipal gestorPrincipal)
    {
        _gestorPrincipal = gestorPrincipal;
    }

    
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

    public void ListarPlatos(Restaurante restaurante)
{
    Console.WriteLine($"\n--- MENÚ DE PLATOS ({restaurante.Menu.Cantidad} en total) ---");
    if (restaurante.Menu.Cabeza == null)
    {
        Console.WriteLine("El menú está vacío.");
        return;
    }
    
    var actual = restaurante.Menu.Cabeza;
    int indice = 1;
    while (actual != null)
    {
        Console.WriteLine($"{indice}. {actual.Valor}"); 
        actual = actual.Siguiente;
        indice++;
    }
}

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

    // 3. Aplicar cambios
    platoAEditar.Nombre = nuevoNombre;
    platoAEditar.Descripcion = nuevaDescripcion;
    platoAEditar.Precio = nuevoPrecio;

    Console.WriteLine($"Plato {codigoPlato} editado con éxito.");
    return true;
}
}