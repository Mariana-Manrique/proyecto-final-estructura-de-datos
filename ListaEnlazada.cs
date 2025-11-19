using System;

public class ListaEnlazada<T> {
    private Nodo<T> cabeza;
    private Nodo<T> ultimo;
    private int cantidad = 0;

    public Nodo<T> Cabeza
    {
		  get {return this.cabeza;}
    }
    public Nodo<T> Ultimo
    {
		  get {return this.ultimo;}
    }
    public int Cantidad
    {
		  get {return this.cantidad;}
    }
    public void Agregar(T valor)
    {    
        cantidad++;
        if (cabeza == null) 
        {
            cabeza = new Nodo<T>(valor);
            ultimo = cabeza;
            return;
        }

        Nodo<T> actual = cabeza;
        while (actual.Siguiente != null) 
        {
            actual = actual.Siguiente;
        }
        actual.Siguiente = new Nodo<T>(valor);
        ultimo = actual.Siguiente;
    }

    public void Imprimir() 
    {
        Nodo<T> actual = cabeza;
        while (actual != null) 
        {
            Console.Write(actual.Valor + " ");
            actual = actual.Siguiente;
        }
    }
    public void Revertir() 
    {
        Nodo<T> previo = null;
        Nodo<T> actual = cabeza;
        Nodo<T> siguiente = null;
        while (actual != null) {
            siguiente = actual.Siguiente;
            actual.Siguiente = previo;
            previo = actual;
            actual = siguiente;
        }
        cabeza = previo;
    }
    public void EliminarPosicion(int posicion)
    {
        if (cabeza == null || posicion < 0)
        {
            return;
        }

        if (posicion == 0)
        {
            cabeza = cabeza.Siguiente;
            cantidad--;
            return;
        }

        Nodo<T> actual = cabeza;
        int contador = 0;

        while (actual.Siguiente != null && contador < posicion - 1)
        {
            actual = actual.Siguiente;
            contador++;
        }

        if (actual.Siguiente == null)
        {
            return;
        }

        actual.Siguiente = actual.Siguiente.Siguiente;
        cantidad--;


        if (actual.Siguiente == null)
        {
            ultimo = actual;
        }
    }


    public int Contar()
    {
        int contador = 0;
        Nodo<T> actual = cabeza;
        while (actual != null) {
            contador++;
            actual = actual.Siguiente;
        }
        return contador;
    }
    public int ContarElementos()
    {
        int contador = 0;
        if(cabeza != null)
            contador=1;

        Nodo<T> actual = cabeza;
        while (actual.Siguiente != null) 
        {
            actual = actual.Siguiente;
            contador++;
        }
        return contador;
    }
    public void InsertarEnPosicion(T valor, int posicion)
    {
        if (posicion < 0)
        {
            throw new ArgumentOutOfRangeException("La posición no puede ser negativa.");
        }

        Nodo<T> nuevoNodo = new Nodo<T>(valor);

        if (posicion == 0)
        {
            nuevoNodo.Siguiente = cabeza;
            cabeza = nuevoNodo;
            return;
        }

        Nodo<T> actual = cabeza;
        int contador = 0;
        while (actual != null && contador < posicion - 1)
        {
            actual = actual.Siguiente;
            contador++;
        }

        if (actual == null)
        {
            throw new ArgumentOutOfRangeException("La posición excede el tamaño de la lista.");
        }

        nuevoNodo.Siguiente = actual.Siguiente;
        actual.Siguiente = nuevoNodo;
    }
    

public bool EliminarPorValor(T valor)
{
    if (cabeza == null)
    {
        return false;
    }

    if (cabeza.Valor.Equals(valor)) 
    {
        cabeza = cabeza.Siguiente;
        if (cabeza == null) ultimo = null; 
        cantidad--;
        return true;
    }

    Nodo<T> actual = cabeza;
    while (actual.Siguiente != null)
    {
        if (actual.Siguiente.Valor.Equals(valor))
        {
            actual.Siguiente = actual.Siguiente.Siguiente;
            if (actual.Siguiente == null) ultimo = actual; 
            cantidad--;
            return true;
        }
        actual = actual.Siguiente;
    }

    return false; 
}

public void Recorrer(Action<T> accion)
{
    Nodo<T> actual = cabeza; 
    while (actual != null)
    {
        
        accion(actual.Valor); 
        actual = actual.Siguiente; 
    }
}
}