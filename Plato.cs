using System;

public class Plato
{
    // Propiedades del plato
    public string Codigo { get; set; } 
    public string Nombre { get; set; } 
    public string Descripcion { get; set; } 
    public decimal Precio { get; set; } 

    // Constructor del plato
    public Plato(string codigo, string nombre, string descripcion, decimal precio)
    {
        Codigo = codigo;
        Nombre = nombre;
        Descripcion = descripcion;
        Precio = precio;
    }

    // Método para mostrar información del plato
    public override string ToString()
    {
        return $"[{Codigo}] {Nombre} - ${Precio:N2} ({Descripcion})";
    }
}