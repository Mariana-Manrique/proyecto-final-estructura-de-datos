using System;

public class GestorPedidos
{
    private GestorPrincipal _gestorPrincipal; // Atributo que guarda una referencia al gestor principal del sistema, para acceder a restaurantes y otros datos.
    
    public GestorPedidos(GestorPrincipal gestorPrincipal)
    {
        _gestorPrincipal = gestorPrincipal; // Se inicializa el gestor principal que será usado por esta clase.
    }

    public bool TomarYConfirmarPedido(string nitRestaurante, string cedulaCliente, ListaEnlazada<PlatoPedido> itemsPedido)
    {
        var restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nitRestaurante); // Buscamos el restaurante usando el NIT.
        if (restaurante == null)
        {
            Console.WriteLine("Error: Restaurante no encontrado."); // Mensaje si el restaurante no existe.
            return false;
        }

        var cliente = ObtenerCliente(restaurante, cedulaCliente); // Buscamos el cliente dentro del restaurante.
        if (cliente == null)
        {
            Console.WriteLine("Error: Cliente no registrado."); // Si no existe, se informa.
            return false;
        }

        var nuevoPedido = new Pedido(cedulaCliente); // Se crea un nuevo pedido asociado al cliente.
        
        var actualItem = itemsPedido.Cabeza; // Se inicia el recorrido por la lista de platos del pedido.
        while (actualItem != null)
        {
            // Agregamos cada elemento (plato pedido) al pedido que estamos formando.
            nuevoPedido.Platos.Agregar(actualItem.Valor);
            actualItem = actualItem.Siguiente; // Avanzamos al siguiente nodo de la lista.
        }

        nuevoPedido.CalcularTotal();  // Calculamos el total del pedido una vez agregados los platos.
        
        restaurante.ColaPedidosPendientes.Agregar(nuevoPedido); // Se agrega el pedido a la cola de pedidos pendientes del restaurante.
        
        cliente.HistorialPedidos.Agregar(nuevoPedido); // Guardamos el pedido en el historial del cliente.
        
        Console.WriteLine($"Pedido #{nuevoPedido.IdPedido} confirmado y encolado. Total: ${nuevoPedido.Total:N2}"); // Confirmación en pantalla.
        return true;
    }
    
    private Cliente ObtenerCliente(Restaurante restaurante, string cedula)
    {
        var actual = restaurante.Clientes.Cabeza; // Iniciamos recorrido por la lista de clientes del restaurante.
        while (actual != null)
        {
            if (actual.Valor.Cedula == cedula) // Si encontramos coincidencia por cédula...
            {
                return actual.Valor; // Devolvemos el cliente encontrado.
            }
            actual = actual.Siguiente; // Continuamos recorriendo la lista.
        }
        return null; // Si no se encontró el cliente, retornamos null.
    }


    
    public bool DespacharSiguientePedido(string nitRestaurante)
    {
        var restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nitRestaurante); // Buscamos el restaurante.
        if (restaurante == null)
        {
            Console.WriteLine("Error: Restaurante no encontrado.");
            return false;
        }
        
       if (restaurante.ColaPedidosPendientes.EstaVacia())  // Revisamos si hay pedidos en espera.
        {
            Console.WriteLine("La cola de pedidos está vacía. No hay nada que despachar.");
            return false;
        }

        
        var pedidoADespachar = restaurante.ColaPedidosPendientes.Primero(); // Obtenemos el primer pedido en la cola.
        restaurante.ColaPedidosPendientes.Eliminar(); // Lo eliminamos de la cola porque ya será despachado.
        
        pedidoADespachar.Estado = Pedido.ESTADO_DESPACHADO;  // Cambiamos su estado a despachado.

        restaurante.SumarGanancia(pedidoADespachar.Total);  // Sumamos el valor del pedido a las ganancias del día.

        var actualPlatoPedido = pedidoADespachar.Platos.Cabeza; // Recorremos los platos del pedido.
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

            actualPlatoPedido = actualPlatoPedido.Siguiente; // Avanzamos en la lista.
        }

        Console.WriteLine($"Pedido #{pedidoADespachar.IdPedido} DESPACHADO. Ganancia sumada: ${pedidoADespachar.Total:N2}"); // Mensaje final.
        return true;
    }
    
    private Plato ObtenerPlato(Restaurante restaurante, string codigo)
    {
        var actual = restaurante.Menu.Cabeza; // Recorremos la lista de platos del menú.
        while (actual != null)
        {
            if (actual.Valor.Codigo == codigo) // Si el código coincide...
            {
                return actual.Valor; // Devolvemos el plato.
            }
            actual = actual.Siguiente; // Continuamos buscando.
        }
        return null; // Si no se encontró el plato.
    }
    
    public void ReporteGananciasDelDia(string nitRestaurante)
    {
        var restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nitRestaurante); // Buscamos el restaurante.
        if (restaurante == null)
        {
            Console.WriteLine("Error: Restaurante no encontrado.");
            return;
        }
        Console.WriteLine($"Ganancias totales del día para {restaurante.Nombre}: ${restaurante.GananciasDelDia:N2}"); // Mostramos total ganado.
    }
    
    public void ReportePlatosServidosRecientes(string nitRestaurante)
    {
        var restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nitRestaurante); // Buscamos restaurante.
        if (restaurante == null)
        {
            Console.WriteLine("Error: Restaurante no encontrado.");
            return;
        }
        
        Console.WriteLine($"Platos Servidos Recientemente ({restaurante.HistorialPlatosServidos.Tamano} ítems):"); // Encabezado del reporte.
        restaurante.HistorialPlatosServidos.ImprimirPila(); // Se imprime la pila de platos servidos.
    }


}
 