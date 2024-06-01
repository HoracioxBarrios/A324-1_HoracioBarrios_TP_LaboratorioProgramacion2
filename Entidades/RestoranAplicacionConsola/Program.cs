using Negocio;
using Entidades.Enumerables;
using Entidades.Interfaces;


namespace RestoranAplicacionConsola
{
    internal class Program
    {
        static void Main(string[] args)
        {           
            GestorDeProveedores gestorDeProveedores = new GestorDeProveedores();
            gestorDeProveedores.CrearProveedor("Aser S.A", "452", "Av Los Macacos 35", ETipoDeProducto.Almacen, EMediosDePago.Transferencia,EAcreedor.No,EDiaDeLaSemana.Lunes);
            gestorDeProveedores.CrearProveedor("Carnes Argentinas SRL", "126", "Av Sin Agua 1500", ETipoDeProducto.Carniceria, EMediosDePago.Tarjeta, EAcreedor.Si, EDiaDeLaSemana.Miercoles);
            MostrarDatosProveedoresEnConsola(gestorDeProveedores.GetProveedores(),"Proveedores: ");

            GestorDeEmpleados gestorDeEmpleados = new GestorDeEmpleados();
            gestorDeEmpleados.CrearEmpleado(ERol.Cocinero, "Gille", "Rel", "1150654", "Av los pericos 5142", 20000m);
            gestorDeEmpleados.CrearEmpleado(ERol.Encargado, "Mar", "Ruy", "1156202", "Principio Solid 5", 30000m);
            gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Jun", "Fre", "117778", "Calle 88", 15000m);
            gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Kler", "Dry", "117563", "Calle 41", 15000m);
            gestorDeEmpleados.CrearEmpleado(ERol.Delivery, "Cris", "Lol", "115632", "Calle 56", 10000m);
            MostrarDatosEmpleadosEnConsola(gestorDeEmpleados.GetEmpleados(), "Empleados: ");

            


        }
        //static bool Login(string user, string password)
        //{
        //    if (user == GestorUsuario.GetUser(user) && password == GestorUser.GetPass(password)) ;
        //}
        //static int MostrarMenuOpciones()
        //{
        //    Console.WriteLine();
        //}

        public static void MostrarDatosProveedoresEnConsola(List<IProveedor> proveedores, string mensage)
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
        public static void MostrarDatosEmpleadosEnConsola(List<IEmpleado> empleados, string mensage)
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
    }
}
