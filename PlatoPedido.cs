using System;

public class PlatoPedido
{
    // Propiedades del plato en el pedido
    public string CodigoPlato { get; set; } 
    public int Cantidad { get; set; } 
    public decimal PrecioUnitario { get; set; } 

    // Propiedad calculada para el subtotal
    public decimal Subtotal => Cantidad * PrecioUnitario;

    // Constructor del plato en el pedido
    public PlatoPedido(string codigoPlato, int cantidad, decimal precioUnitario)
    {
        CodigoPlato = codigoPlato;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
    }
    
    // Método para representar el plato en el pedido como una cadena de texto
    public override string ToString()
    {
        return $"- {Cantidad}x Plato '{CodigoPlato}' @ ${PrecioUnitario:N2} = ${Subtotal:N2}";
    }
}