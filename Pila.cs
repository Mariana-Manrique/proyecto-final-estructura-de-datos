using System;
public class Pila<T>
{
    private Nodo<T> cima;
    private int tamano;

    public int Tamano
    {
		  get {return this.tamano;}
    }

    public void AgregarElemento(T valor)
    {
        Nodo<T> nuevoNodo = new Nodo<T>(valor); 

        if (cima == null)
        {
            cima = nuevoNodo;
        }
        else
        {
            nuevoNodo.Siguiente = cima;
            cima = nuevoNodo;
        }
        tamano++;
    }

    public void EliminarElemento()
    {
        if (cima != null)
        {
            tamano--;
            cima = cima.Siguiente;
        }
    }

    public void ImprimirPila()
    {
        Nodo<T> nodoActual = cima;
        while (nodoActual != null)
        {
            Console.WriteLine(nodoActual.Valor + " ");
            nodoActual = nodoActual.Siguiente;
        }
        Console.WriteLine();
    }

    public void ImprimirAlReves()
    {
        Pila<T> pilaAuxiliar = new Pila<T>();

        while(cima!=null)
        {
            pilaAuxiliar.AgregarElemento(cima.Valor);
            EliminarElemento();
        }

        pilaAuxiliar.ImprimirPila();
    }
}