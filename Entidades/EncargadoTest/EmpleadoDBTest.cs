using Datos;
using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int contadorEmpleados = 0;
            empleados = empleadoDb.ReadAll();

            foreach(IEmpleado empleado in empleados)
            {
                contadorEmpleados++;
            }


            Assert.IsTrue(contadorEmpleados > 0);
            
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
