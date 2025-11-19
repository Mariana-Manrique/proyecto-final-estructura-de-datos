using System;

public class Restaurante
{
    // Propiedades del restaurante
    public string Nit { get; set; } 
    public string Nombre { get; set; } 
    public string Dueno { get; set; } 
    public string Celular { get; set; } 
    public string Direccion { get; set; } 

    // Estructuras de datos asociadas al restaurante
    public ListaEnlazada<Cliente> Clientes { get; set; } 
    public ListaEnlazada<Plato> Menu { get; set; } 
    public Cola<Pedido> ColaPedidosPendientes { get;  set; } 
    public Pila<Plato> HistorialPlatosServidos { get;  set; } 

    // Ganancias del día
    public decimal GananciasDelDia { get; set; }

    // Constructor del restaurante
    public Restaurante(string nit, string nombre, string dueno, string celular, string direccion)
    {
        Nit = nit;
        Nombre = nombre;
        Dueno = dueno;
        Celular = celular;
        Direccion = direccion;
        
        Clientes = new ListaEnlazada<Cliente>();
        Menu = new ListaEnlazada<Plato>();
        ColaPedidosPendientes = new Cola<Pedido>();
        HistorialPlatosServidos = new Pila<Plato>();
        GananciasDelDia = 0;
    }

    // Método para sumar ganancias del día
    public void SumarGanancia(decimal monto)
    {
        GananciasDelDia += monto; 
    }
    
    public override string ToString()
    {
        return $"NIT: {Nit} | Nombre: {Nombre} | Dueño: {Dueno} | Dir: {Direccion}";
    }
}