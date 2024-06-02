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
            GestorDeProveedores gestorDeProveedores = new GestorDeProveedores();
            gestorDeProveedores.CrearProveedor("Aser S.A", "452", "Av Los Macacos 35", ETipoDeProductoProveido.Almacen, EMediosDePago.Transferencia,EAcreedor.No,EDiaDeLaSemana.Lunes);
            gestorDeProveedores.CrearProveedor("Carnes Argentinas SRL", "126", "Av Sin Agua 1500", ETipoDeProductoProveido.Carniceria, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Miercoles);
            gestorDeProveedores.CrearProveedor("Almacenes S.A", "598", "Av Sin Tierra 1500", ETipoDeProductoProveido.Almacen, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Jueves);
            Mostrar(gestorDeProveedores.GetProveedores(),"Proveedores: ");

            GestorDeEmpleados gestorDeEmpleados = new GestorDeEmpleados();
            gestorDeEmpleados.CrearEmpleado(ERol.Cocinero, "Gille", "Rel", "1150654", "Av los pericos 5142", 20000m);
            gestorDeEmpleados.CrearEmpleado(ERol.Encargado, "Mar", "Ruy", "1156202", "Principio Solid 5", 30000m);
            gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Jun", "Fre", "117778", "Calle 88", 15000m);
            gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Kler", "Dry", "117563", "Calle 41", 15000m);
            gestorDeEmpleados.CrearEmpleado(ERol.Delivery, "Cris", "Lol", "115632", "Calle 56", 10000m);
            Mostrar(gestorDeEmpleados.GetEmpleados(), "Empleados: ");


            



            IProveedor proveedor1 = gestorDeProveedores.GetProveedor(1);
            IProveedor proveedor2 = gestorDeProveedores.GetProveedor(2);
            IProveedor proveedor3 = gestorDeProveedores.GetProveedor(3);

            GestorDeProductos gestorDeProductos = new GestorDeProductos();
            IProducto productoIngrediente1= gestorDeProductos.CrearProducto(ETipoProductoCreable.Ingrediente, "Tomate", 10, EUnidadMedida.Kilo, 600, proveedor1);
            //Console.WriteLine(productoIngrediente1);

            IProducto productoBebida =gestorDeProductos.CrearProducto(ETipoProductoCreable.Bebida, "Coca Cola", 30, EUnidadMedida.Unidad, 1500, proveedor3);
            //Console.WriteLine(productoBebida);
            Mostrar(gestorDeProductos.GetProductos(), "Productos: ");
            Console.WriteLine("Precio Total en Stock");
            Console.WriteLine(gestorDeProductos.CalcularPrecio());





            Console.ReadKey();
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
