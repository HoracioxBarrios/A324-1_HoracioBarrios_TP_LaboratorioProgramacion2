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
            gestorDeProveedores.CrearProveedor("Aser S.A", "452", "Av Los Macacos 35", ETipoDeProducto.Almacen, EMediosDePago.Transferencia,EAcreedor.No,EDiaDeLaSemana.Lunes);
            gestorDeProveedores.CrearProveedor("Carnes Argentinas SRL", "126", "Av Sin Agua 1500", ETipoDeProducto.Carniceria, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Miercoles);
            gestorDeProveedores.CrearProveedor("Almacenes S.A", "598", "Av Sin Tierra 1500", ETipoDeProducto.Almacen, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Jueves);
            //Mostrar(gestorDeProveedores.GetProveedores(),"Proveedores: ");

            //GestorDeEmpleados gestorDeEmpleados = new GestorDeEmpleados();
            //gestorDeEmpleados.CrearEmpleado(ERol.Cocinero, "Gille", "Rel", "1150654", "Av los pericos 5142", 20000m);
            //gestorDeEmpleados.CrearEmpleado(ERol.Encargado, "Mar", "Ruy", "1156202", "Principio Solid 5", 30000m);
            //gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Jun", "Fre", "117778", "Calle 88", 15000m);
            //gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Kler", "Dry", "117563", "Calle 41", 15000m);
            //gestorDeEmpleados.CrearEmpleado(ERol.Delivery, "Cris", "Lol", "115632", "Calle 56", 10000m);
            //Mostrar(gestorDeEmpleados.GetEmpleados(), "Empleados: ");


            



            IProveedor proveedor1 = gestorDeProveedores.GetProveedor(1);
            IProveedor proveedor2 = gestorDeProveedores.GetProveedor(2);
            IProveedor proveedor3 = gestorDeProveedores.GetProveedor(3);

            //GestorDeProductos gestorDeProductos = new GestorDeProductos();
            //IProducto productoIngrediente1= gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "Tomate", 10, EUnidadMedida.Kilo, 1000, proveedor1);
            ////Console.WriteLine(productoIngrediente1);
            //IProducto productoIngrediente2 = gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "pollo", 20, EUnidadMedida.Kilo, 20000, proveedor1);
            //IProducto productoIngrediente3 = gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "lechuga", 6, EUnidadMedida.Kilo, 600, proveedor1);

            //IProducto productoBebida =gestorDeProductos.CrearProducto(ETipoDeProducto.Bebida, "Coca Cola", 30, EUnidadMedida.Unidad, 1500, proveedor3);
            //Console.WriteLine(productoBebida);
            //Mostrar(gestorDeProductos.GetProductos(), "Productos: ");
            //Console.WriteLine("Precio Total en Stock");
            //Console.WriteLine(gestorDeProductos.CalcularPrecio());


            //Arrange
            //Ingrediente 1
            ETipoDeProducto tipoDeProducto1 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto1 = "Pollo";
            double cantidad1 = 10;
            EUnidadMedida unidadDeMedida1 = EUnidadMedida.Kilo;
            decimal precio1 = 20000;
            //Ingrediente 1
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

            IProducto ingrediente1 = ProductoServiceFactory.CrearProducto(tipoDeProducto1, nombreDeProducto1, cantidad1, unidadDeMedida1, precio1, proveedor1);

            ingrediente1.Id = 1;

            IProducto ingrediente2 = ProductoServiceFactory.CrearProducto(tipoDeProducto2, nombreDeProducto2, cantidad2, unidadDeMedida2, precio2, proveedor2);
            ingrediente2.Id = 2;

            IProducto ingrediente3 = ProductoServiceFactory.CrearProducto(tipoDeProducto3, nombreDeProducto3, cantidad3, unidadDeMedida3, precio3, proveedor3);
            ingrediente3.Id = 1;

            Console.WriteLine($"{ingrediente1.Nombre} El ingrediente 1 tiene la ID: {ingrediente1.Id}");
            Console.WriteLine($"{ingrediente2.Nombre} El ingrediente 2 tiene la ID: {ingrediente2.Id}");
            Console.WriteLine($"{ingrediente3.Nombre} El ingrediente 3 tiene la ID: {ingrediente3.Id}");

            //Act
            //Productos que van a estar en la lista del stock
            // Configuración de los productos ingrediente1 e ingrediente2...

            List<IProducto> listaDeProductosIngredientesStock = new List<IProducto>();
            listaDeProductosIngredientesStock.Add(ingrediente1);
            listaDeProductosIngredientesStock.Add(ingrediente2);

            //Producto que va a estar en el plato(lo que  usa el plato)
            // Configuración del ingrediente3...

            List<IProducto> listaDeIngredienteEnElPlato = new List<IProducto>();
            listaDeIngredienteEnElPlato.Add(ingrediente3);

            List<IProducto> productosActualizados = new List<IProducto>();

            foreach (IProducto producto in listaDeProductosIngredientesStock)
            {
                if (producto is Ingrediente ingrediente)
                {
                    foreach (IProducto productoADescontar in listaDeIngredienteEnElPlato)
                    {
                        if (productoADescontar is Ingrediente ingredienteADescontar)
                        {
                            if (ingrediente.Id == ingredienteADescontar.Id)
                            {
                                Ingrediente nuevoIngrediente = ingrediente - (Ingrediente)productoADescontar;
                                productosActualizados.Add(nuevoIngrediente);
                            }
                            else
                            {
                                productosActualizados.Add(ingrediente);
                            }
                        }
                    }
                }
            }

            // Actualiza la lista original con los productos actualizados
            for (int i = 0; i < listaDeProductosIngredientesStock.Count; i++)
            {
                listaDeProductosIngredientesStock[i] = productosActualizados[i];
            }
            //Verifica si el ingrediente descontado es el mismo que el ingrediente en la posición 0 de la lista
            foreach (IProducto producto in listaDeProductosIngredientesStock)
            {
                if (producto is Ingrediente ingrediente)
                    Console.WriteLine($"Id: {producto.Id}, Nombre: {producto.Nombre} Cantidad: {producto.Cantidad}");
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
