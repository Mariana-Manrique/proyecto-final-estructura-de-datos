//namespace Listas.Dominio;

using System;

public class Cliente
{
    // Atributos clave
    public string Cedula { get; set; } // Única [cite: 21, 22]
    public string NombreCompleto { get; set; } // [cite: 21]
    public string Celular { get; set; } // 10 dígitos [cite: 21, 22]
    public string Email { get; set; } // Formato válido [cite: 21, 22]

    // Historial de pedidos del cliente (Lista Enlazada)
    public ListaEnlazada<Pedido> HistorialPedidos { get; private set; } // [cite: 10]

    public Cliente(string cedula, string nombre, string celular, string email)
    {
        Cedula = cedula;
        NombreCompleto = nombre;
        Celular = celular;
        Email = email;
        HistorialPedidos = new ListaEnlazada<Pedido>();
    }

   // [cite_start]// Verifica si el cliente tiene pedidos PENDIENTES [cite: 48]
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