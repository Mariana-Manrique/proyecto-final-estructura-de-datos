//namespace Listas.Dominio;

public class PlatoPedido
{
    // Atributos clave
    public string CodigoPlato { get; set; } // [cite: 33]
    public int Cantidad { get; set; } // [cite: 33] (Regla: > 0) [cite: 33]
    public decimal PrecioUnitario { get; set; } // [cite: 33]

    public decimal Subtotal => Cantidad * PrecioUnitario;

    public PlatoPedido(string codigoPlato, int cantidad, decimal precioUnitario)
    {
        CodigoPlato = codigoPlato;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
    }
    
    public override string ToString()
    {
        return $"- {Cantidad}x Plato '{CodigoPlato}' @ ${PrecioUnitario:N2} = ${Subtotal:N2}";
    }
}