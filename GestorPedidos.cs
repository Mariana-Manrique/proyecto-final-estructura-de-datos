using System;

public class GestorPedidos
{
    //Guardamos una referencia al gestor principal para poder acceder a los restaurantes y sus pedidos
    private GestorPrincipal _gestorPrincipal; 
    
    public GestorPedidos(GestorPrincipal gestorPrincipal)
    {
        _gestorPrincipal = gestorPrincipal; 
    }

    // Método para tomar y confirmar un nuevo pedido
    public bool TomarYConfirmarPedido(string nitRestaurante, string cedulaCliente, ListaEnlazada<PlatoPedido> itemsPedido)
    {
        var restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nitRestaurante); 
        if (restaurante == null)
        {
            Console.WriteLine("Error: Restaurante no encontrado."); 
            return false;
        }

        var cliente = ObtenerCliente(restaurante, cedulaCliente); 
        if (cliente == null)
        {
            Console.WriteLine("Error: Cliente no registrado."); 
            return false;
        }

        var nuevoPedido = new Pedido(cedulaCliente); 
        var actualItem = itemsPedido.Cabeza; 
        while (actualItem != null)
        {
            nuevoPedido.Platos.Agregar(actualItem.Valor);
            actualItem = actualItem.Siguiente; 
        }

        nuevoPedido.CalcularTotal();  
        restaurante.ColaPedidosPendientes.Agregar(nuevoPedido); 
        cliente.HistorialPedidos.Agregar(nuevoPedido); 
        
        Console.WriteLine($"Pedido #{nuevoPedido.IdPedido} confirmado y encolado. Total: ${nuevoPedido.Total:N2}"); 
        return true;
    }
    
    // Método auxiliar para obtener un cliente por su cédula
    private Cliente ObtenerCliente(Restaurante restaurante, string cedula)
    {
        var actual = restaurante.Clientes.Cabeza; 
        while (actual != null)
        {
            if (actual.Valor.Cedula == cedula) 
            {
                return actual.Valor; 
            }
            actual = actual.Siguiente; 
        }
        return null; 
    }

    // Método para despachar el siguiente pedido del cliente en la cola
    public bool DespacharSiguientePedido(string nitRestaurante)
    {
        var restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nitRestaurante); 
        if (restaurante == null)
        {
            Console.WriteLine("Error: Restaurante no encontrado.");
            return false;
        }
        
       if (restaurante.ColaPedidosPendientes.EstaVacia())  
        {
            Console.WriteLine("La cola de pedidos está vacía. No hay nada que despachar.");
            return false;
        }

        var pedidoADespachar = restaurante.ColaPedidosPendientes.Primero(); 
        restaurante.ColaPedidosPendientes.Eliminar(); 
        
        pedidoADespachar.Estado = Pedido.ESTADO_DESPACHADO;  
        restaurante.SumarGanancia(pedidoADespachar.Total);  

        var actualPlatoPedido = pedidoADespachar.Platos.Cabeza; 
        while (actualPlatoPedido != null)
        {
            // Buscamos la información del plato en el menú del restaurante.
            var plato = ObtenerPlato(restaurante, actualPlatoPedido.Valor.CodigoPlato); 
            if (plato != null)
            {
                // Por cada unidad del plato servido, lo agregamos al historial.
                for (int i = 0; i < actualPlatoPedido.Valor.Cantidad; i++)
                {
                    restaurante.HistorialPlatosServidos.AgregarElemento(plato);
                }
            }
            actualPlatoPedido = actualPlatoPedido.Siguiente; 
        }

        Console.WriteLine($"Pedido #{pedidoADespachar.IdPedido} DESPACHADO. Ganancia sumada: ${pedidoADespachar.Total:N2}"); // Mensaje final.
        return true;
    }
    
    // Método auxiliar para obtener un plato por su código
    private Plato ObtenerPlato(Restaurante restaurante, string codigo)
    {
        var actual = restaurante.Menu.Cabeza; 
        while (actual != null)
        {
            if (actual.Valor.Codigo == codigo) 
            {
                return actual.Valor; 
            }
            actual = actual.Siguiente; 
        }
        return null; 
    }
    
    // Método para generar un reporte de ganancias del día
    public void ReporteGananciasDelDia(string nitRestaurante)
    {
        var restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nitRestaurante); 
        if (restaurante == null)
        {
            Console.WriteLine("Error: Restaurante no encontrado.");
            return;
        }
        Console.WriteLine($"Ganancias totales del día para {restaurante.Nombre}: ${restaurante.GananciasDelDia:N2}"); 
    }
    
    // Método para generar un reporte de platos servidos recientemente
    public void ReportePlatosServidosRecientes(string nitRestaurante)
    {
        var restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nitRestaurante); 
        if (restaurante == null)
        {
            Console.WriteLine("Error: Restaurante no encontrado.");
            return;
        }
        
        Console.WriteLine($"Platos Servidos Recientemente ({restaurante.HistorialPlatosServidos.Tamano} ítems):"); 
        restaurante.HistorialPlatosServidos.ImprimirPila(); 
    }


}
 