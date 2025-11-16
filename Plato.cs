//namespace Listas.Dominio;

using System;
//esta es la clase plato que contiene los atributos y metodos necesarios para manejar los platos del menu
public class Plato
{
    // Atributos clave
    public string Codigo { get; set; } // [cite: 24]
    public string Nombre { get; set; } // [cite: 24]
    public string Descripcion { get; set; } // [cite: 24]
    public decimal Precio { get; set; } // [cite: 24] (Regla: > 0) [cite: 25]

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