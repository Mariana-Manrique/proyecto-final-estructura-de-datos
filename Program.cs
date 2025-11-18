using System;
class Program

{

    private static GestorPrincipal _gestorPrincipal = new GestorPrincipal();

    private static GestorPedidos _gestorPedidos = new GestorPedidos(_gestorPrincipal);

    private static GestorCliente _gestorCliente = new GestorCliente(_gestorPrincipal);

    private static GestorMenu _gestorMenu = new GestorMenu(_gestorPrincipal);





    static void Main(string[] args)

   

    {

        // RNF-04: Datos de ejemplo para empezar

        SetupDatosEjemplo();



        MostrarMenuPrincipal();

    }

private static void MenuGestionRestaurantes()

{

    bool regresar = false;

    while (!regresar)

    {

        Console.WriteLine("\n-- Gestión de Restaurantes (RF-01) --");

        Console.WriteLine("1. Crear Restaurante");

        Console.WriteLine("2. Listar Restaurantes");

        Console.WriteLine("3. Editar Restaurante");

        Console.WriteLine("4. Borrar Restaurante");

        Console.WriteLine("0. Regresar");

        Console.Write("Seleccione opción: ");



        string opcion = Console.ReadLine()?.Trim();



        switch (opcion)

        {

            case "1":

                CrearRestauranteDesdeConsola();

                break;

            case "2":

                ListarRestaurantes();

                break;

            case "3":

                EditarRestauranteDesdeConsola();

                break;

            case "4":

                BorrarRestauranteDesdeConsola();

                break;

            case "0":

                regresar = true;

                break;

            default:

                Console.WriteLine("Opción no válida.");

                break;

        }

    }

}



private static void CrearRestauranteDesdeConsola()

{

    Console.Write("NIT: ");

    string nit = Console.ReadLine()?.Trim();



    Console.Write("Nombre: ");

    string nombre = Console.ReadLine()?.Trim();



    Console.Write("Dueño: ");

    string dueño = Console.ReadLine()?.Trim();



    Console.Write("Celular (10 dígitos): ");

    string celular = Console.ReadLine()?.Trim();



    Console.Write("Dirección: ");

    string dir = Console.ReadLine()?.Trim();



    var r = new Restaurante(nit, nombre, dueño, celular, dir);



    if (_gestorPrincipal.CrearRestaurante(r))

        Console.WriteLine("Restaurante creado con éxito.");

    else

        Console.WriteLine("No se pudo crear (NIT duplicado o datos inválidos)");

}



private static void ListarRestaurantes()

{

    Console.WriteLine("\n-- Restaurantes registrados --");

    _gestorPrincipal.Restaurantes.Recorrer(r =>

    {

        Console.WriteLine($"{r.Nit} | {r.Nombre}");

    });

}



private static void EditarRestauranteDesdeConsola()

{

    Console.Write("Ingrese NIT del restaurante a editar: ");

    string nit = Console.ReadLine()?.Trim();



    var restaurante = _gestorPrincipal.BuscarRestaurante(nit);

    if (restaurante == null)

    {

        Console.WriteLine("Restaurante no encontrado.");

        return;

    }

    Console.Write("Nuevo nombre (enter para mantener): ");

    string nuevoNom = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(nuevoNom))

        restaurante.Nombre = nuevoNom;



    Console.Write("Nuevo dueño (enter para mantener): ");

    string nuevoDueño = Console.ReadLine();

    
    Console.Write("Nuevo celular (enter para mantener): ");

    string nuevoCel = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(nuevoCel))

        restaurante.Celular = nuevoCel;



    Console.Write("Nueva dirección (enter para mantener): ");

    string nuevaDir = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(nuevaDir))

        restaurante.Direccion = nuevaDir;



    Console.WriteLine("Restaurante actualizado.");

}



private static void BorrarRestauranteDesdeConsola()

{

    Console.Write("Ingrese NIT del restaurante a borrar: ");

    string nit = Console.ReadLine()?.Trim();



    if (_gestorPrincipal.BorrarRestaurante(nit))

        Console.WriteLine("Restaurante eliminado.");

    else

        Console.WriteLine("No se pudo borrar (no existe o tiene pedidos).");

}



    private static void SetupDatosEjemplo()

    {

        // Crear Restaurante de ejemplo

        var r1 = new Restaurante("1234567890", "El Buen Sabor", "Juan Pérez", "3001234567", "Calle Falsa 123");

        _gestorPrincipal.CrearRestaurante(r1);



        // Agregar Cliente de ejemplo

        var c1 = new Cliente("1001", "Ana Gomez", "3109876543", "ana@mail.com");

        r1.Clientes.Agregar(c1);



        // Agregar Platos de ejemplo

        r1.Menu.Agregar(new Plato("P01", "Hamburguesa Clásica", "Doble carne y queso", 15.00m));

        r1.Menu.Agregar(new Plato("P02", "Papas Fritas", "Porción grande", 5.00m));

    }



    private static void MostrarMenuPrincipal()

    {

        // Implementación básica del Menú Navegable (RF-09)

        bool salir = false;

        while (!salir)

        {

            Console.Clear();

            Console.WriteLine("=====================================");

            Console.WriteLine(" 🍴 SISTEMA DE GESTIÓN DE RESTAURANTE");

            Console.WriteLine("=====================================");

            Console.WriteLine("1. Gestión de Restaurantes");

            Console.WriteLine("2. Gestión de Menú y Clientes (Requiere seleccionar Restaurante)");

            Console.WriteLine("3. Gestión de Pedidos y Reportes");

            Console.WriteLine("0. Salir");

            Console.Write("\nSeleccione una opción: ");



            string opcion = Console.ReadLine();



            switch (opcion)

            {

                case "1":

                   MenuGestionRestaurantes();

                    break;

                case "2":

                    // Primero pide el NIT del restaurante a gestionar

                    GestionarMenuYClientes();

                    break;

                case "3":

                    MenuGestionPedidos();

                    break;

                case "0":

                    salir = true;

                    break;

                default:

                    Console.WriteLine("Opción no válida.");

                    Pausar();

                    break;

            }

        }

    }



    // [Implementar métodos: MenuGestionRestaurantes, GestionarMenuYClientes, MenuGestionPedidos, etc.]

   

    // Método de utilidad para pausar la consola (RNF-03)

    private static void Pausar()

    {

        Console.WriteLine("\nPresione cualquier tecla para continuar...");

        Console.ReadKey();

    }



    private static void GestionarMenuYClientes()

{

    // Función para seleccionar el restaurante a gestionar

    Console.WriteLine("\n--- SELECCIÓN DE RESTAURANTE ---");

    Console.Write("Ingrese el NIT del Restaurante a gestionar: ");

    string nit = Console.ReadLine();



    Restaurante restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nit);



    if (restaurante == null)

    {

        Console.WriteLine($"\nRestaurante con NIT {nit} no encontrado.");

        Pausar();

        return;

    }



    Console.WriteLine($"\n--- Gestionando: {restaurante.Nombre} ---");

    MenuGestionClientesYPlatos(restaurante);

}



private static void MenuGestionClientesYPlatos(Restaurante restaurante)

{

    bool regresar = false;

    while (!regresar)

    {

        Console.Clear();

        Console.WriteLine($"\n== GESTIÓN de {restaurante.Nombre} (NIT: {restaurante.Nit}) ==");

        Console.WriteLine("1. Gestión de Clientes (RF-02)");

        Console.WriteLine("2. Gestión de Platos del Menú (RF-03)");

        Console.WriteLine("0. Regresar al Menú Principal");

        Console.Write("\nSeleccione una opción: ");



        string opcion = Console.ReadLine();



        switch (opcion)

        {

            case "1":

                MenuClientes(restaurante);

                break;

            case "2":

                MenuPlatos(restaurante);

                break;

            case "0":

                regresar = true;

                break;

            default:

                Console.WriteLine("Opción no válida.");

                Pausar();

                break;

        }

    }

}

// ----------------------------------------------------------------------

// Métodos de navegación específicos

// ----------------------------------------------------------------------



private static void MenuClientes(Restaurante restaurante)

{

    bool regresar = false;

    while (!regresar)

    {

        Console.WriteLine("\n-- Gestión de Clientes --");

        Console.WriteLine("1. Crear Cliente");

        Console.WriteLine("2. Listar Clientes");

        Console.WriteLine("0. Regresar");

        Console.Write("Seleccione opción: ");



        string opcion = Console.ReadLine()?.Trim();



        switch (opcion)

        {

            case "1":

                _gestorCliente.CrearClienteDesdeConsola(restaurante);

                break;

            case "2":

                _gestorCliente.ListarClientes(restaurante);

                break;

            case "0":

                regresar = true;

                break;

            default:

                Console.WriteLine("Opción no válida");

                break;

        }

    }

}





private static void CrearClienteDesdeConsola(Restaurante restaurante)

{

    Console.Clear();

    Console.WriteLine($"\n== CREAR CLIENTE en {restaurante.Nombre} ==");



    Console.Write("Cédula: ");

    string cedula = Console.ReadLine();



    // Validar que no exista

    if (ObtenerClientePorCedula(restaurante, cedula) != null)

    {

        Console.WriteLine("Error: Ya existe un cliente con esa cédula.");

        Pausar();

        return;

    }



    Console.Write("Nombre completo: ");

    string nombre = Console.ReadLine();



    Console.Write("Celular (10 dígitos): ");

    string celular = Console.ReadLine();



    Console.Write("Email: ");

    string email = Console.ReadLine();



    // Validaciones básicas (según el PDF)

    if (string.IsNullOrWhiteSpace(cedula) ||

        string.IsNullOrWhiteSpace(nombre) ||

        celular.Length != 10)

    {

        Console.WriteLine("Error de validación. Revise: cédula/nombre no vacíos y celular de 10 dígitos.");

        Pausar();

        return;

    }



    if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))

    {

        Console.WriteLine("Error de validación. Email con formato no válido.");

        Pausar();

        return;

    }



    var nuevoCliente = new Cliente(cedula, nombre, celular, email);

    restaurante.Clientes.Agregar(nuevoCliente);



    Console.WriteLine("Cliente creado con éxito.");

    Pausar();

}



private static void EditarClienteDesdeConsola(Restaurante restaurante)

{

    Console.Clear();

    Console.WriteLine($"\n== EDITAR CLIENTE en {restaurante.Nombre} ==");



    Console.Write("Ingrese la cédula del cliente a editar: ");

    string cedula = Console.ReadLine();



    // Usamos el GestorCliente que ya creaste

    Console.Write("Nuevo nombre completo: ");

    string nuevoNombre = Console.ReadLine();



    Console.Write("Nuevo celular (10 dígitos): ");

    string nuevoCelular = Console.ReadLine();



    Console.Write("Nuevo email: ");

    string nuevoEmail = Console.ReadLine();



    _gestorCliente.EditarCliente(restaurante.Nit, cedula, nuevoNombre, nuevoCelular, nuevoEmail);

    Pausar();

}



private static void BorrarClienteDesdeConsola(Restaurante restaurante)

{

    Console.Clear();

    Console.WriteLine($"\n== BORRAR CLIENTE en {restaurante.Nombre} ==");



    Console.Write("Ingrese la cédula del cliente a borrar: ");

    string cedula = Console.ReadLine();



    // Usa el borrado seguro (valida pedidos pendientes)

    _gestorCliente.BorrarClienteSeguro(restaurante.Nit, cedula);



    Pausar();

}



private static void MenuPlatos(Restaurante restaurante)

{

    bool regresar = false;

    while (!regresar)

    {

        Console.WriteLine("\n-- Gestión de Platos --");

        Console.WriteLine("1. Crear Plato");

        Console.WriteLine("2. Listar Platos");

        Console.WriteLine("0. Regresar");

        Console.Write("Seleccione opción: ");



        string opcion = Console.ReadLine()?.Trim();



        switch (opcion)

        {

            case "1":

                _gestorMenu.CrearPlatoDesdeConsola(restaurante);

                break;

            case "2":

                _gestorMenu.ListarPlatos(restaurante);

                break;

            case "0":

                regresar = true;

                break;

            default:

                Console.WriteLine("Opción no válida.");

                break;

        }

    }

}





private static void CrearPlatoDesdeConsola(Restaurante restaurante)

{

    Console.Clear();

    Console.WriteLine($"\n== CREAR PLATO en {restaurante.Nombre} ==");



    Console.Write("Código del plato: ");

    string codigo = Console.ReadLine();



    // Verificar que no exista ya un plato con ese código

    if (ObtenerPlatoPorCodigo(restaurante, codigo) != null)

    {

        Console.WriteLine("Error: Ya existe un plato con ese código.");

        Pausar();

        return;

    }



    Console.Write("Nombre del plato: ");

    string nombre = Console.ReadLine();



    Console.Write("Descripción: ");

    string descripcion = Console.ReadLine();



    Console.Write("Precio: ");

    string precioTexto = Console.ReadLine();



    if (!decimal.TryParse(precioTexto, out decimal precio))

    {

        Console.WriteLine("Error: El precio debe ser un número válido.");

        Pausar();

        return;

    }



    if (precio <= 0)

    {

        Console.WriteLine("Error: El precio debe ser mayor que 0.");

        Pausar();

        return;

    }



    if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre))

    {

        Console.WriteLine("Error: Código y nombre no pueden estar vacíos.");

        Pausar();

        return;

    }



    var nuevoPlato = new Plato(codigo, nombre, descripcion, precio);

    restaurante.Menu.Agregar(nuevoPlato);



    Console.WriteLine("Plato creado con éxito.");

    Pausar();

}



private static void ListarPlatosRestaurante(Restaurante restaurante)

{

    Console.Clear();

    Console.WriteLine($"\n== MENÚ DE PLATOS de {restaurante.Nombre} ==");



    var actual = restaurante.Menu.Cabeza;

    if (actual == null)

    {

        Console.WriteLine("No hay platos registrados en el menú.");

        return;

    }



    int indice = 1;

    while (actual != null)

    {

        Console.WriteLine($"{indice}. {actual.Valor}");

        actual = actual.Siguiente;

        indice++;

    }

}



private static void EditarPlatoDesdeConsola(Restaurante restaurante)

{

    Console.Clear();

    Console.WriteLine($"\n== EDITAR PLATO en {restaurante.Nombre} ==");



    Console.Write("Código del plato a editar: ");

    string codigo = Console.ReadLine();



    Console.Write("Nuevo nombre: ");

    string nuevoNombre = Console.ReadLine();



    Console.Write("Nueva descripción: ");

    string nuevaDescripcion = Console.ReadLine();



    Console.Write("Nuevo precio: ");

    string precioTexto = Console.ReadLine();



    if (!decimal.TryParse(precioTexto, out decimal nuevoPrecio))

    {

        Console.WriteLine("Error: El precio debe ser un número válido.");

        Pausar();

        return;

    }



    // Usamos el GestorMenu para aplicar reglas de negocio

    _gestorMenu.EditarPlato(restaurante.Nit, codigo, nuevoNombre, nuevaDescripcion, nuevoPrecio);

    Pausar();

}



private static void BorrarPlatoDesdeConsola(Restaurante restaurante)

{

    Console.Clear();

    Console.WriteLine($"\n== BORRAR PLATO en {restaurante.Nombre} ==");



    Console.Write("Código del plato a borrar: ");

    string codigo = Console.ReadLine();



    // Usa el borrado seguro (no permite borrar si hay pedidos pendientes que lo usan)

    _gestorMenu.BorrarPlatoSeguro(restaurante.Nit, codigo);

    Pausar();

}





private static void MenuGestionPedidos()

{

    Console.WriteLine("\n--- GESTIÓN DE PEDIDOS ---");

    Console.Write("Ingrese el NIT del Restaurante: ");

    string nit = Console.ReadLine();



    Restaurante restaurante = _gestorPrincipal.ObtenerRestaurantePorNit(nit);



    if (restaurante == null)

    {

        Console.WriteLine("\nRestaurante no encontrado.");

        Pausar();

        return;

    }



    bool regresar = false;

    while (!regresar)

    {

        Console.Clear();

        Console.WriteLine($"\n== GESTIÓN DE PEDIDOS en {restaurante.Nombre} ==");

        Console.WriteLine("1. Tomar Nuevo Pedido (RF-04, RF-05)");

        Console.WriteLine("2. Despachar Siguiente Pedido (RF-06)");

        Console.WriteLine("3. Reporte de Ganancias del Día (RF-07)");

        Console.WriteLine("4. Reporte de Platos Servidos Recientes (RF-07)");

        Console.WriteLine("0. Regresar");

        Console.Write("\nSeleccione una opción: ");



        string opcion = Console.ReadLine();



        switch (opcion)

        {

            case "1":

                FlujoTomarPedido(restaurante);

                Pausar();

                break;

            case "2":

                _gestorPedidos.DespacharSiguientePedido(restaurante.Nit);

                Pausar();

                break;

            case "3":

                _gestorPedidos.ReporteGananciasDelDia(restaurante.Nit);

                Pausar();

                break;

            case "4":

                _gestorPedidos.ReportePlatosServidosRecientes(restaurante.Nit);

                Pausar();

                break;

            case "0":

                regresar = true;

                break;

            default:

                Console.WriteLine("Opción no válida.");

                Pausar();

                break;

        }

    }

}



private static void FlujoTomarPedido(Restaurante restaurante)

{

    Console.WriteLine("\n--- INICIO DE PEDIDO ---");

    Console.Write("Ingrese la Cédula del Cliente: ");

    string cedula = Console.ReadLine();

   

    // Validar existencia de cliente

    Cliente cliente = ObtenerClientePorCedula(restaurante, cedula);

    if (cliente == null)

    {

        Console.WriteLine("Error: Cliente no encontrado o Cédula incorrecta.");

        return;

    }



    // Usaremos una Lista Enlazada temporal para guardar los ítems antes de confirmar

    var itemsPedidoTemp = new ListaEnlazada<PlatoPedido>();

    bool agregarMas = true;

   

    while (agregarMas)

    {

        Console.Clear();

        Console.WriteLine($"\n-- Agregando Ítems para {cliente.NombreCompleto} --");

       

        // RF-04: Ver menú

        new GestorMenu(_gestorPrincipal).ListarPlatos(restaurante);

       

        Console.Write("\nIngrese el Código del Plato a ordenar (o 'FIN' para terminar): ");

        string codigo = Console.ReadLine().ToUpper();



        if (codigo == "FIN")

        {

            agregarMas = false;

            break;

        }

       

        // Buscar el plato en el menú del restaurante para obtener precio y validar

        Plato platoSeleccionado = ObtenerPlatoPorCodigo(restaurante, codigo);

       

        if (platoSeleccionado == null)

        {

            Console.WriteLine("Código de plato no válido. Intente de nuevo.");

            Pausar();

            continue;

        }



        Console.Write($"Ingrese la cantidad de '{platoSeleccionado.Nombre}': ");

        if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)

        {

            Console.WriteLine("Cantidad no válida. Debe ser un número entero mayor que cero.");

            Pausar();

            continue;

        }



        // Crear el item y agregarlo a la lista temporal

        var item = new PlatoPedido(platoSeleccionado.Codigo, cantidad, platoSeleccionado.Precio);

        itemsPedidoTemp.Agregar(item);

       

        Console.WriteLine($"'{platoSeleccionado.Nombre}' x{cantidad} agregado.");

        Pausar();

    }

   

    // Si no se agregaron ítems

    if (itemsPedidoTemp.Cantidad == 0)

    {

        Console.WriteLine("El pedido fue cancelado al no agregar ítems.");

        return;

    }



    // Calcular Total Previo (RF-04)

    decimal totalPrevio = 0;

    var actualItem = itemsPedidoTemp.Cabeza;

    while(actualItem != null)

    {

        totalPrevio += actualItem.Valor.Subtotal;

        actualItem = actualItem.Siguiente;

    }



    Console.WriteLine($"\nRESUMEN DEL PEDIDO: Total a pagar: ${totalPrevio:N2}");

    Console.Write("¿Desea confirmar el pedido? (S/N): ");

   

    if (Console.ReadLine().ToUpper() == "S")

    {

        // RF-05: Encolar Pedido

        _gestorPedidos.TomarYConfirmarPedido(restaurante.Nit, cedula, itemsPedidoTemp);

    }

    else

    {

        Console.WriteLine("Pedido cancelado.");

    }

}



// Métodos de utilidad para Program.cs

private static Cliente ObtenerClientePorCedula(Restaurante restaurante, string cedula)

{

    var actual = restaurante.Clientes.Cabeza;

    while (actual != null)

    {

        if (actual.Valor.Cedula == cedula)

        {

            return actual.Valor;

        }

        actual = actual.Siguiente;

    }

    return null;

}



private static Plato ObtenerPlatoPorCodigo(Restaurante restaurante, string codigo)

{

    var actual = restaurante.Menu.Cabeza;

    while (actual != null)

    {

        if (actual.Valor.Codigo == codigo)

        {

            return actual.Valor;

        }

        actual = actual.Siguiente;

    }

    return null;

}

}