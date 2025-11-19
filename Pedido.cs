using System;

public class Pedido
{
    // Constantes para los estados del pedido
    public const string ESTADO_PENDIENTE = "PENDIENTE"; 
    public const string ESTADO_DESPACHADO = "DESPACHADO"; 
    
    private static int _siguienteId = 1;

    // Propiedades del pedido
    public int IdPedido { get; private set; } 
    public string CedulaCliente { get; set; } 
    public ListaEnlazada<PlatoPedido> Platos { get; private set; } 
    public decimal Total { get; private set; } 
    public DateTime FechaHora { get; private set; } 
    public string Estado { get; set; } = ESTADO_PENDIENTE; 

    // Constructor que inicializa un nuevo pedido
    public Pedido(string cedulaCliente)
    {
        IdPedido = _siguienteId++;
        CedulaCliente = cedulaCliente;
        Platos = new ListaEnlazada<PlatoPedido>();
        FechaHora = DateTime.Now;
    }

    // Método para calcular el total del pedido
    public void CalcularTotal()
    {
        decimal totalCalculado = 0;
        
        var actual = Platos.Cabeza;
        while (actual != null)
        {
            totalCalculado += actual.Valor.Subtotal;
            actual = actual.Siguiente;
        }

        Total = totalCalculado;
    }

    // Método para representar el pedido como una cadena de texto
    public override string ToString()
    {
        return $"Pedido #{IdPedido} - Cliente: {CedulaCliente} - Total: ${Total:N2} - Estado: {Estado}";
    }
}