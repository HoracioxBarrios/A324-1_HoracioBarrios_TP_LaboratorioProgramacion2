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

            //EMPLEADOS
            GestorDeEmpleados gestorDeEmpleados = new GestorDeEmpleados();
            gestorDeEmpleados.CrearEmpleado(ERol.Cocinero, "Gille", "Rel", "1150654", "Av los pericos 5142", 20000m);
            gestorDeEmpleados.CrearEmpleado(ERol.Encargado, "Mar", "Ruy", "1156202", "Principio Solid 5", 30000m);
            gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Jun", "Fre", "117778", "Calle 88", 15000m);
            gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Kler", "Dry", "117563", "Calle 41", 15000m);
            gestorDeEmpleados.CrearEmpleado(ERol.Delivery, "Cris", "Lol", "115632", "Calle 56", 10000m);
            Mostrar(gestorDeEmpleados.GetEmpleados(), "Empleados: ");



            //PROVEEDORES
            GestorDeProveedores gestorDeProveedores = new GestorDeProveedores();
            gestorDeProveedores.CrearProveedor("Aser S.A", "452", "Av Los Macacos 35", ETipoDeProducto.Almacen, EMediosDePago.Transferencia, EAcreedor.No, EDiaDeLaSemana.Lunes);
            gestorDeProveedores.CrearProveedor("Carnes Argentinas SRL", "126", "Av Sin Agua 1500", ETipoDeProducto.Carniceria, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Miercoles);
            gestorDeProveedores.CrearProveedor("Almacenes S.A", "598", "Av Sin Tierra 1500", ETipoDeProducto.Almacen, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Jueves);
            Mostrar(gestorDeProveedores.GetProveedores(), "Proveedores: ");

            IProveedor proveedor1 = gestorDeProveedores.GetProveedor(1);
            IProveedor proveedor2 = gestorDeProveedores.GetProveedor(2);
            IProveedor proveedor3 = gestorDeProveedores.GetProveedor(3);


            //PRODUCTOS : INGREDIENTES
            GestorDeProductos gestorDeProductos = new GestorDeProductos();

            //Ingrediente 1
            ETipoDeProducto tipoDeProducto1 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto1 = "Pollo";
            double cantidad1 = 10;
            EUnidadMedida unidadDeMedida1 = EUnidadMedida.Kilo;
            decimal precio1 = 20000;
            //Ingrediente 2
            ETipoDeProducto tipoDeProducto2 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto2 = "Papa";
            double cantidad2 = 20;
            EUnidadMedida unidadDeMedida2 = EUnidadMedida.Kilo;
            decimal precio2 = 20000;

            //Ingrediente 3 que va a ser el mismo que el ingrediente 1 pero se va a usar para descontar
            ETipoDeProducto tipoDeProducto3 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto3 = "Pollo";
            double cantidad3 = 4;
            EUnidadMedida unidadDeMedida3 = EUnidadMedida.Kilo;
            decimal precio3 = 20000;

            gestorDeProductos.CrearProductoParaListaDeStock(tipoDeProducto1, nombreDeProducto1, cantidad1, unidadDeMedida1, precio1, proveedor1);
            gestorDeProductos.CrearProductoParaListaDeStock(tipoDeProducto2, nombreDeProducto2, cantidad2, unidadDeMedida2, precio2, proveedor2);


            Mostrar(gestorDeProductos.GetProductos(), "Productos Ingredientes");


            //Producto que va a estar en el PLATO(lo que  usa el plato) Ingrediente 3   

            List<IProducto> listaDeIngredienteEnElPlato = new List<IProducto>();
            IProducto ingrediente3 = gestorDeProductos.CrearProducto(
                tipoDeProducto3, nombreDeProducto3, cantidad3, unidadDeMedida3, precio3, proveedor3);

            listaDeIngredienteEnElPlato.Add(ingrediente3);



            List<IProducto> productosActualizados = new List<IProducto>();

            //DESCONTAR
            bool seDesconto = gestorDeProductos.DescontarProductosDeStock(listaDeIngredienteEnElPlato);

            if (seDesconto)
            {
                Mostrar(gestorDeProductos.GetProductos(), "Lista Actualizada: Productos Ingredientes");
            }






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
