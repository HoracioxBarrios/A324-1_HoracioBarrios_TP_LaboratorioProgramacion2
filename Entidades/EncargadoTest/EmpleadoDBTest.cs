using Datos;
using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;
using Negocio;

namespace Test
{
    [TestClass]
    public class EmpleadoDBTest
    {
        //CREACION DE LA TABLA EMPLEADO EN DB
        [TestMethod]
        public void CorroborarLaCreacionDeLaTablaEmpleadoEnDb_SiEsFalseEstaOkPorqueSeConsideraCreadaLaTabla()
        {
            
            IOperacionesEmpleadoDB empleadoDb = new EmpleadoDB();

            // Asegurarse de que la tabla se haya creado (o verificar si ya existe)
            empleadoDb.CrearTabla();


            Assert.IsFalse(empleadoDb.CrearTabla());// Verificar que intentar crearla nuevamente devuelve false
        }
        //CREACION DE EMPLEADOS EN DB
        [TestMethod]
        public void CorroboraLaCreacionDeVariosEmpleadoEnLaDB_DaTrueSiSeCrearon()
        {
            // Arrange
            EmpleadoDB operacionesDeBaseDeDatosEmpleados = new EmpleadoDB();
            GestorDeEmpleados gestorDeEmpleados = new GestorDeEmpleados(operacionesDeBaseDeDatosEmpleados);

            //----- instancio los EMPLEADOS y se crean en la DB ------- (AL MOMENTO DE CREAR SI EXISTEN LANZA EXCEPTION, ES PARA LA PRIMERA VEZ NOMAS)
            //gestorDeEmpleados.CrearEmpleado(ERol.Cocinero, "Gille", "Rel", "1150654", "Av los pericos 5142", 20000m);
            //gestorDeEmpleados.CrearEmpleado(ERol.Encargado, "Mar", "Ruy", "1156202", "Principio Solid 5", 30000m);
            //gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Jun", "Fre", "117778", "Calle 88", 15000m);
            //gestorDeEmpleados.CrearEmpleado(ERol.Mesero, "Kler", "Dry", "117563", "Calle 41", 15000m);
            //gestorDeEmpleados.CrearEmpleado(ERol.Delivery, "Cris", "Lol", "115632", "Calle 56", 10000m);

            List<IEmpleado> empleados = gestorDeEmpleados.GetEmpleadosEnList();
            Assert.IsTrue(empleados.Count > 0);

        }


        //CREACION DE EMPLEADO EN DB
        [TestMethod]
        public void CorroboraLaExistenciaDeUnEmpleadoEnLaDB_DaTrueSiSeCreoElEmpleadoEnLaDB()
        {
            ERol rol = ERol.Encargado;
            string nombre = "ada";
            string apellido = "DBTEst";
            //string contacto = "1144553311";
            //string nombreVacio = string.Empty;
            //string direccion = "Av. San Pocho";
            //decimal salario = 150000.50M;
            IOperacionesEmpleadoDB empleadoDb = new EmpleadoDB();
            Assert.IsNotNull(empleadoDb.ReadOne(nombre, apellido));
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
            //NUEVO PASSWORD
            string passwordActualizacion = "444444";

            IOperacionesEmpleadoDB enpleadoDb = new EmpleadoDB();



            Assert.IsTrue(enpleadoDb.Update(id, passwordActualizacion));
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
            Assert.IsTrue(empleadoDb.Delete(3));

        }
        //ELIMINA EMPLEADO POR NOMBRE Y APELLIDO
        [TestMethod]
        public void CorroboraSiSeEliminaElEmpleadoPorNombreYApellido_CambiaACeroElStatusSiSeEliminoElEmpleado()
        {


            IOperacionesEmpleadoDB empleadoDb = new EmpleadoDB();
            Assert.IsTrue(empleadoDb.Delete("Gille", "Rel"));

        }
    }
}
