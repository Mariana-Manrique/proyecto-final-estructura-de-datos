using System;
public class Cola<T>
{
    // Nodo cabeza y cola para la implementación de la cola
    private Nodo<T> cabeza;
    private Nodo<T> cola;
    private int tamano;

    // Constructor que inicializa una cola vacía
    public Cola()
    {
        cabeza = null;
        cola = null;
        tamano = 0;
    }
    
    // Método para agregar un elemento al final de la cola
    public void Agregar(T valor)
    {
        Nodo<T> nuevoNodo = new Nodo<T>(valor);
        if (EstaVacia())
        {
            cabeza = nuevoNodo;
            cola = nuevoNodo;
        }
        else
        {
            cola.Siguiente = nuevoNodo;
            cola = nuevoNodo;
        }
        tamano++;
    }

    // Método para eliminar el primer elemento de la cola
    public void Eliminar()
    {
        if (EstaVacia())
        {
            throw new InvalidOperationException("La cola está vacía.");
        }
        cabeza = cabeza.Siguiente;
        tamano--;
    }

    // Método para obtener el primer elemento de la cola sin eliminarlo
    public T Primero()
    {
        if (EstaVacia())
        {
            throw new InvalidOperationException("La cola está vacía.");
        }
        return cabeza.Valor;
    }

    // Método para obtener el tamaño de la cola
    public int Tamano()
    {
        return tamano;
    }

    // Método para verificar si la cola está vacía
    public bool EstaVacia()
    {
        return tamano == 0;
    }

    // Propiedad para acceder al nodo cabeza
    public Nodo<T> Cabeza
    {
        get { return this.cabeza; }
    }

    // Método para imprimir los elementos de la cola
    public void Imprimir()
    {
        if (EstaVacia())
        {
            Console.WriteLine("La cola está vacía.");
            return;
        }

        Nodo<T> actual = cabeza;

        Console.Write("Cola: ");
        while (actual != null)
        {
            Console.Write(actual.Valor + " ");
            actual = actual.Siguiente;
        }
        Console.WriteLine();
    }

}