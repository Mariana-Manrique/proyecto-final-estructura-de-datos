//namespace Listas.Dominio;

using System;

public class Restaurante
{
    // Atributos clave
    public string Nit { get; set; } // Único [cite: 18, 19]
    public string Nombre { get; set; } // [cite: 18]
    public string Dueno { get; set; } // [cite: 18]
    public string Celular { get; set; } // 10 dígitos [cite: 18, 19]
    public string Direccion { get; set; } // [cite: 18]

    // Estructuras de datos para la lógica central
    public ListaEnlazada<Cliente> Clientes { get; set; } // [cite: 10]
    public ListaEnlazada<Plato> Menu { get; set; } // [cite: 10]
    public Cola<Pedido> ColaPedidosPendientes { get;  set; } // Cola de pedidos pendientes [cite: 11]
    public Pila<Plato> HistorialPlatosServidos { get;  set; } // Pila para historial [cite: 12]

    // Variable para las ganancias del día
    public decimal GananciasDelDia { get; set; }

    public Restaurante(string nit, string nombre, string dueno, string celular, string direccion)
    {
        Nit = nit;
        Nombre = nombre;
        Dueno = dueno;
        Celular = celular;
        Direccion = direccion;
        
        // Inicialización de las estructuras de datos
        Clientes = new ListaEnlazada<Cliente>();
        Menu = new ListaEnlazada<Plato>();
        ColaPedidosPendientes = new Cola<Pedido>();
        HistorialPlatosServidos = new Pila<Plato>();
        GananciasDelDia = 0;
    }

    //[cite_start]// Método llamado al despachar un pedido (RF-06) [cite: 58]
    public void SumarGanancia(decimal monto)
    {
        GananciasDelDia += monto; // [cite: 50]
    }
    
    public override string ToString()
    {
        return $"NIT: {Nit} | Nombre: {Nombre} | Dueño: {Dueno} | Dir: {Direccion}";
    }
}