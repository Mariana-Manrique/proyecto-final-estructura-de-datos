using System;

public class Cliente
{
    //Información básica del cliente
    public string Cedula { get; set; } 
    public string NombreCompleto { get; set; } 
    public string Celular { get; set; } 
    public string Email { get; set; } 

    // Historial de pedidos del cliente
    public ListaEnlazada<Pedido> HistorialPedidos { get; private set; } 

    // Constructor que inicializa el cliente con sus datos básicos
    public Cliente(string cedula, string nombre, string celular, string email)
    {
        // Asignación de propiedades
        Cedula = cedula;
        NombreCompleto = nombre;
        Celular = celular;
        Email = email;
        HistorialPedidos = new ListaEnlazada<Pedido>();
    }

    // Método para verificar si el cliente tiene pedidos pendientes
    public bool TienePedidosPendientes()
    {
        var actual = HistorialPedidos.Cabeza;
        while (actual != null)
        {
            if (actual.Valor.Estado == Pedido.ESTADO_PENDIENTE)
            {
                return true;
            }
            actual = actual.Siguiente;
        }
        return false;
    }
    
    // Método para representar el cliente como una cadena de texto
    public override string ToString()
    {
        return $"CC: {Cedula} | Nombre: {NombreCompleto} | Cel: {Celular} | Email: {Email}";
    }
}