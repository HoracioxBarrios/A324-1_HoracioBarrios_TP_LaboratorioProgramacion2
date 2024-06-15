using Datos;
using Entidades.Enumerables;
using Entidades.Interfaces;
using Negocio;

namespace TestEntidades
{
    [TestClass]
    public class EmpleadoDBTest
    {
        //CREACION DE LA TABLA EMPLEADO EN DB
        [TestMethod]
        public void CorroborarLaCreacionDeLaTablaEmpleadoEnDb_SiEsFalseEstaOkPorqueSeConsideraCreadaLaTabla()
        {
            
            IOperacionesEmpleadoDB enpleadoDb = new EmpleadoDB();

            // Asegurarse de que la tabla se haya creado (o verificar si ya existe)
            enpleadoDb.CrearTabla();


            Assert.IsFalse(enpleadoDb.CrearTabla());// Verificar que intentar crearla nuevamente devuelve false
        }


        //CREACION DE EMPLEADO EN DB
        [TestMethod]
        public void CorroboraLaCreacionDeUnEmpleadoEnLaDB_DaTrueSiSeCreoElEmpleadoEnLaDB()
        {
            ERol rol = ERol.Encargado;
            string nombre = "ada";
            string apellido = "DBTEst";
            string contacto = "1144553311";
            string nombreVacio = string.Empty;



            string direccion = "Av. San Pocho";
            decimal salario = 150000.50M;
            IOperacionesEmpleadoDB enpleadoDb = new EmpleadoDB();



            Assert.IsTrue(enpleadoDb.Create(rol, nombre, apellido, contacto, direccion, salario));
        }
        //CREACION DE EMPLEADO EN DB
        [TestMethod]
        public void CorroboraLaCreacionDeVariosEmpleadoEnLaDB_DaTrueSiSeCrearonLosEmpleadosEnLaDB()
        {
            EmpleadoDB operacionesDeBaseDeDatosEmpleados = new EmpleadoDB();


            GestorDeEmpleados gestorDeEmpleados = new GestorDeEmpleados(operacionesDeBaseDeDatosEmpleados);

            //----- instancio los EMPLEADOS y se crean en la DB -------
            bool seCreoEmpleado1 = gestorDeEmpleados.CrearEmpleado(ERol.Cocinero, "Gille", "Rel", "1150654", "Av los pericos 5142", 20000m);
            bool seCreoEmpleado2 = gestorDeEmpleados.CrearEmpleado(ERol.Encargado, "Mar", "Ruy", "1156202", "Principio Solid 5", 30000m);
            bool seCreoEmpleado3 = gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Jun", "Fre", "117778", "Calle 88", 15000m);
            bool seCreoEmpleado4 = gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Kler", "Dry", "117563", "Calle 41", 15000m);
            bool seCreoEmpleado5 = gestorDeEmpleados.CrearEmpleado(ERol.Delivery, "Cris", "Lol", "115632", "Calle 56", 10000m);

            Assert.IsNotNull(gestorDeEmpleados.GetEmpleadosEnList());

            
        }
        //MODIFICACION DE EMPLEADO EN DB
        [TestMethod]
        public void CorroboraLaModificacionDeUnEmpleadoPorIdEnLaDB_DaTrueSiSeModificoElEmpleadoEnLaDB()
        {
            ERol rol = ERol.Encargado;
            string nombre = "Jhon";
            string apellido = "DBTEst";
            string contacto = "1144553311";
            string nombreVacio = string.Empty;
            string direccion = "Av. San Pocho";
            decimal salario = 150000.50M;


            int id = 6;
            string nuevaDireccion = "Av. MateListo 1234";
            string passwordActualizacion = "444444";
            EStatus status = EStatus.Activo;

            IOperacionesEmpleadoDB enpleadoDb = new EmpleadoDB();



            Assert.IsTrue(enpleadoDb.Update(id, passwordActualizacion, status, rol, nombre, apellido, contacto, nuevaDireccion, salario));
        }

        //READ ALL EMPLEADOS DE LA DB
        [TestMethod]
        public void CorroboraLaLecturaDeTodosLosEmpleadosEnLaDB_DaTrueSiSeconsigueQueLaListaTengaEmpleados()
        {
            List<IEmpleado> empleados = new List<IEmpleado>();
            IOperacionesEmpleadoDB empleadoDb = new EmpleadoDB();

            empleados = empleadoDb.ReadAll();


            Assert.IsTrue(empleados.Count > 0);
            
        }
        //READ ONE EMPLEADO DE LA DB
        public void CorroboraSiLeeUnEmpleadoPorIdDeLaDB()
        {
            
            IOperacionesEmpleadoDB empleadoDb = new EmpleadoDB();
            IEmpleado empleado = empleadoDb.ReadOne(2);

            Assert.IsNotNull(empleado);
        }
        //READ ONE EMPLEADO DE LA DB
        public void CorroboraSiLeeUnEmpleadoPorNombreYApellidoDeLaDB()
        {

            IOperacionesEmpleadoDB empleadoDb = new EmpleadoDB();
            IEmpleado empleado = empleadoDb.ReadOne("Jun", "Fre");

            Assert.IsNotNull(empleado);
        }



        //ELIMINA EMPLEADO POR ID
        [TestMethod]
        public void CorroboraSiSeEliminaElEmpleadoPorId_CambiaACeroElStatusSiSeEliminoElEmpleado()
        {
            

            IOperacionesEmpleadoDB empleadoDb = new EmpleadoDB();
            Assert.IsTrue(empleadoDb.Delete(8));

        }
        //ELIMINA EMPLEADO POR NOMBRE Y APELLIDO
        [TestMethod]
        public void CorroboraSiSeEliminaElEmpleadoPorNombreYApellido_CambiaACeroElStatusSiSeEliminoElEmpleado()
        {


            IOperacionesEmpleadoDB empleadoDb = new EmpleadoDB();
            Assert.IsTrue(empleadoDb.Delete("Cris", "Lol"));

        }
    }
}
