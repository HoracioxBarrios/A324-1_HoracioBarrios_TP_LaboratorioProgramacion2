using Negocio;
using Entidades.Enumerables;
using Entidades.Interfaces;
using Entidades;


namespace RestoranAplicacionConsola
{
    internal class Program
    {
        static void Main(string[] args)
        {


            Restoran restoran = new Restoran();


        //    //EMPLEADOS
        //    GestorDeEmpleados gestorDeEmpleados = new GestorDeEmpleados();
        //    gestorDeEmpleados.CrearEmpleado(ERol.Cocinero, "Gille", "Rel", "1150654", "Av los pericos 5142", 20000m);
        //    gestorDeEmpleados.CrearEmpleado(ERol.Encargado, "Mar", "Ruy", "1156202", "Principio Solid 5", 30000m);
        //    gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Jun", "Fre", "117778", "Calle 88", 15000m);
        //    gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Kler", "Dry", "117563", "Calle 41", 15000m);
        //    gestorDeEmpleados.CrearEmpleado(ERol.Delivery, "Cris", "Lol", "115632", "Calle 56", 10000m);
        //    Mostrar(gestorDeEmpleados.GetEmpleados(), "Empleados: ");



        //    //PROVEEDORES
        //    GestorDeProveedores gestorDeProveedores = new GestorDeProveedores();
        //    gestorDeProveedores.CrearProveedor("Aser S.A", "452", "Av Los Macacos 35", ETipoDeProducto.Almacen, EMediosDePago.Transferencia, EAcreedor.No, EDiaDeLaSemana.Lunes);
        //    gestorDeProveedores.CrearProveedor("Carnes Argentinas SRL", "126", "Av Sin Agua 1500", ETipoDeProducto.Carniceria, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Miercoles);
        //    gestorDeProveedores.CrearProveedor("Almacenes S.A", "598", "Av Sin Tierra 1500", ETipoDeProducto.Almacen, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Jueves);
        //    Mostrar(gestorDeProveedores.GetProveedores(), "Proveedores: ");

        //    IProveedor proveedor1 = gestorDeProveedores.GetProveedor(1);
        //    IProveedor proveedor2 = gestorDeProveedores.GetProveedor(2);
        //    IProveedor proveedor3 = gestorDeProveedores.GetProveedor(3);


        //// PRODUCTOS: INGREDIENTES
        //GestorDeProductos gestorDeProductos = new GestorDeProductos();

        //    //Ingrediente 1
        //    ETipoDeProducto tipoDeProducto1 = ETipoDeProducto.Ingrediente;
        //    string nombreDeProducto1 = "Aceite";
        //    double cantidad1 = 10;
        //    EUnidadMedida unidadDeMedida1 = EUnidadMedida.Litro;
        //    decimal precio1 = 10000;
        //    //Ingrediente 2
        //    ETipoDeProducto tipoDeProducto2 = ETipoDeProducto.Ingrediente;
        //    string nombreDeProducto2 = "Papa";
        //    double cantidad2 = 20;
        //    EUnidadMedida unidadDeMedida2 = EUnidadMedida.Kilo;
        //    decimal precio2 = 20000;

        //    //Ingrediente 3 que va a ser el mismo que el ingrediente 1 pero se va a usar para descontar
        //    ETipoDeProducto tipoDeProducto3 = ETipoDeProducto.Ingrediente;
        //    string nombreDeProducto3 = "Aceite";
        //    double cantidad3 = 500;
        //    EUnidadMedida unidadDeMedida3 = EUnidadMedida.MiliLitro;
        //    decimal precio3 = 1000;

        //    gestorDeProductos.CrearProductoParaListaDeStock(tipoDeProducto1, nombreDeProducto1, cantidad1, unidadDeMedida1, precio1, proveedor1);
        //    gestorDeProductos.CrearProductoParaListaDeStock(tipoDeProducto2, nombreDeProducto2, cantidad2, unidadDeMedida2, precio2, proveedor2);


        //    Mostrar(gestorDeProductos.GetAllProductos(), "Productos Ingredientes");


        //    //Producto que va a estar en el PLATO(lo que  usa el plato) Ingrediente 3

        //    List<IProducto> listaDeIngredienteEnElPlato = new List<IProducto>();
        //    IProducto ingrediente3 = gestorDeProductos.CrearProducto(
        //        tipoDeProducto3, nombreDeProducto3, cantidad3, unidadDeMedida3, precio3, proveedor3);

        //    listaDeIngredienteEnElPlato.Add(ingrediente3);



        //    List<IProducto> productosActualizados = new List<IProducto>();

        //    //DESCONTAR
        //    bool seDesconto = gestorDeProductos.DescontarProductosDeStock(listaDeIngredienteEnElPlato);

        //    if (seDesconto)
        //    {
        //        Mostrar(gestorDeProductos.GetAllProductos(), "Lista Actualizada: Productos Ingredientes");
        //    }



        //    //IPRODUCTO BEBIDA 1
        //    ETipoDeProducto tipoDeProductoBebida1 = ETipoDeProducto.Bebida;
        //    string nombreBebida1 = "CocaCola";
        //    double cantidadBebida1 = 20;
        //    EUnidadMedida eUnidadDeMedidaBebida1 = EUnidadMedida.Unidad;
        //    decimal precioBebida1 = 20000;
        //    IProveedor proveedorBebida1 = proveedor3;
        //    ECategoriaConsumible categoriaConsumibleBebida1 = ECategoriaConsumible.Bebida;
        //    EClasificacionBebida clasificacionBebida1 = EClasificacionBebida.Sin_Añcohol;
        //    gestorDeProductos.CrearProductoParaListaDeStock(tipoDeProductoBebida1, nombreBebida1, cantidadBebida1, eUnidadDeMedidaBebida1, precioBebida1, proveedorBebida1, categoriaConsumibleBebida1, clasificacionBebida1);
        //    // Bebida 2
        //    ETipoDeProducto tipoDeProductoBebida2 = ETipoDeProducto.Bebida;
        //    string nombreBebida2 = "Cerveza QUilmes";
        //    double cantidadBebida2 = 10;
        //    EUnidadMedida eUnidadDeMedidaBebida2 = EUnidadMedida.Unidad;
        //    decimal precioBebida2 = 10000;
        //    IProveedor proveedorBebida2 = proveedor3;
        //    ECategoriaConsumible categoriaConsumibleBebida2 = ECategoriaConsumible.Bebida;
        //    EClasificacionBebida clasificacionBebida2 = EClasificacionBebida.Con_Alcohol;
        //    gestorDeProductos.CrearProductoParaListaDeStock(tipoDeProductoBebida2, nombreBebida2, cantidadBebida2, eUnidadDeMedidaBebida2, precioBebida2, proveedorBebida2, categoriaConsumibleBebida2, clasificacionBebida2);

        //    Mostrar(gestorDeProductos.GetAllProductos(), "A ver si figuran las Bebidas?");

        //    //VEAMOS SI PODEMOS VER EL PRECIO TOTAL EN PRODUCTOS QUE HAY EN STOCK
        //    decimal precioTotalEnproductos = gestorDeProductos.CalcularPrecio();
        //    Console.WriteLine($"Precio Total entre los productos: {precioTotalEnproductos}");


        //    //----------------------------------Cocinero - Platos - menu - Gestor menú -----------------------------
        //    // Crear ingredientes
        //    IConsumible ingrediente1 = new Ingrediente(1, "Tomate", 1.0, EUnidadMedida.Kilo, 5, ETipoDeProducto.Verduleria, proveedor1);
        //    IConsumible ingrediente2 = new Ingrediente(2, "Cebolla", 0.5, EUnidadMedida.Kilo, 3, ETipoDeProducto.Verduleria, proveedor2);
        //    List<IProducto> ingredientes = new List<IProducto>();
        //    ingredientes.Add((IProducto)ingrediente1);
        //    ingredientes.Add((IProducto)ingrediente2);

        //    // Crear cocinero
        //    ICocinero cocinero = new Cocinero(ERol.Cocinero, "Crhistof", "hf", "123456789", "Calle 123", 50000M);

        //    // Crear gestor de menú
        //    GestorMenu gestorMenu = new GestorMenu(cocinero);

        //    // Crear menú
        //    gestorMenu.CrearMenu("Desayuno");

        //    // Agregar plato al menú
        //    gestorMenu.AgregarPlatoAMenu("Desayuno", "Pizza", ingredientes);



        //    Mostrar(gestorMenu.GetListaDeAllMenus(), "Menues");


            Console.ReadKey();
        }
        public static void Mostrar(List<IMenu> menus, string mensage)
        {
            if (menus.Count > 0)
            {
                Console.WriteLine(mensage);
                foreach (IMenu menu in menus)
                {

                    Console.WriteLine(menu);
                }
            }

        }



        public static void Mostrar(List<IProveedor> proveedores, string mensage)
        {
            if(proveedores.Count > 0)
            {
                Console.WriteLine(mensage);
                foreach (IProveedor provedor in proveedores)
                {
                    
                    Console.WriteLine(provedor);
                }
            }

        }
        public static void Mostrar(List<IEmpleado> empleados, string mensage)
        {
            if (empleados.Count > 0)
            {
                Console.WriteLine(mensage);
                foreach (IEmpleado empleado in empleados)
                {
                    
                    Console.WriteLine(empleado);
                }
            }

        }

        public static void Mostrar(List<IProducto> productos, string mensage)
        {
            if (productos.Count > 0)
            {
                Console.WriteLine(mensage);
                foreach (IProducto producto in productos)
                {

                    Console.WriteLine(producto);
                }
            }
        }
    }
}
