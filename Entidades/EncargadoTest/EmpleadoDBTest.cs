using Datos;
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

        [TestMethod]
        public void CorroborarLaCreacionDeLaTablaEmpleadoEnDb_SiEsFalseEstaOkPorqueSeConsideraCreadaLaTabla()
        {
            IOperacionesDeBaseDeDatos<IEmpleado> enpleadoDb = new EmpleadoDB();

            // Asegurarse de que la tabla se haya creado (o verificar si ya existe)
            enpleadoDb.CrearTabla();

            
            Assert.IsFalse(enpleadoDb.CrearTabla());// Verificar que intentar crearla nuevamente devuelve false
        }

    }
}
