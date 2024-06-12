using Datos;
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

        [TestMethod]
        public void CorroborarLaCreacionDeLaTablaEmpleadoEnDb_SiEsFalseEstaOkPorqueSeConsideraCreadaLaTabla()
        {
            // Asegurarse de que la tabla se haya creado (o verificar si ya existe)
            EmpleadoDB.CrearTablaEmpleado();

            
            Assert.IsFalse(EmpleadoDB.CrearTablaEmpleado());// Verificar que intentar crearla nuevamente devuelve false
        }

    }
}
