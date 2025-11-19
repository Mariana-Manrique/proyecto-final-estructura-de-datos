using System;

public class Cliente
{
    
    public string Cedula { get; set; } 
    public string NombreCompleto { get; set; } 
    public string Celular { get; set; } 
    public string Email { get; set; } 

    
    public ListaEnlazada<Pedido> HistorialPedidos { get; private set; } 

    public Cliente(string cedula, string nombre, string celular, string email)
    {
        Cedula = cedula;
        NombreCompleto = nombre;
        Celular = celular;
        Email = email;
        HistorialPedidos = new ListaEnlazada<Pedido>();
    }

   
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
    
    public override string ToString()
    {
        return $"CC: {Cedula} | Nombre: {NombreCompleto} | Cel: {Celular} | Email: {Email}";
    }
}