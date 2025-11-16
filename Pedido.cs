//namespace Listas.Dominio;

using System;
//Esta es la clase pedido que contiene los atributos y metodos necesarios para manejar los pedidos de los clientes
public class Pedido
{
    // Constantes para el estado del pedido
    public const string ESTADO_PENDIENTE = "PENDIENTE"; // [cite: 29]
    public const string ESTADO_DESPACHADO = "DESPACHADO"; // [cite: 32]
    
    private static int _siguienteId = 1;

    // Atributos clave
    public int IdPedido { get; private set; } // Autogenerado [cite: 29]
    public string CedulaCliente { get; set; } // [cite: 29]
    public ListaEnlazada<PlatoPedido> Platos { get; private set; } // Lista enlazada de PlatoPedido [cite: 29, 10]
    public decimal Total { get; private set; } // [cite: 29]
    public DateTime FechaHora { get; private set; } // [cite: 29]
    public string Estado { get; set; } = ESTADO_PENDIENTE; // Por defecto PENDIENTE [cite: 29]

    public Pedido(string cedulaCliente)
    {
        IdPedido = _siguienteId++;
        CedulaCliente = cedulaCliente;
        Platos = new ListaEnlazada<PlatoPedido>();
        FechaHora = DateTime.Now;
    }

    public void CalcularTotal()
    {
        decimal totalCalculado = 0;
        
        // **Nota:** Para calcular el total, aquí necesitarías implementar
        // un método en ListaEnlazada para recorrer y sumar o usar 
        // una lógica de recorrido manual si no quieres modificar ListaEnlazada.
        // Asumiremos por ahora que lo haces con un recorrido interno.

        // Ejemplo de recorrido manual (si no tienes un ForEach):
        var actual = Platos.Cabeza;
        while (actual != null)
        {
            totalCalculado += actual.Valor.Subtotal;
            actual = actual.Siguiente;
        }

        Total = totalCalculado;
    }

    public override string ToString()
    {
        return $"Pedido #{IdPedido} - Cliente: {CedulaCliente} - Total: ${Total:N2} - Estado: {Estado}";
    }
}