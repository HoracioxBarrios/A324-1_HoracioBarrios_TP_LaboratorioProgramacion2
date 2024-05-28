using Entidades;
using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using Entidades.Interfaces;
using Entidades.Excepciones;
namespace EntidadTest
{
    [TestClass]
    public class EmpleadosTest
    {

        [TestMethod]
        public void VerificaElEmpleadoFactory_SiLograInstanciarEmpleadosFuncionaBien_EstaBienSiNoEsNull()
        {
            //Arrange
            string nombre = "Pibe";
            string apellido = "Mc Test";
            string contacto = "1144553311";
            ERol rol = ERol.Encargado;
            string direccion = "Av. San Pocho";
            decimal salario = 150000.50M;

            //Act
            IEmpleado empleado = EmpleadoFactory.CrearEmpleado(rol, nombre, apellido, contacto, direccion, salario);

            //Assert
            Assert.IsNotNull(empleado);

        }

        [TestMethod]
        public void VerificaElEmpleadoFactory_SePasaNombreVacioDebeDarExcepcion_SiDaExcepcionFuncionaBien()
        {
            //Arrange
            string nombre = "Pibe";
            string nombreVacio = string.Empty;
            string apellido = "Mc Test";
            string contacto = "1144553311";
            ERol rol = ERol.Encargado;            
            string direccion = "Av. San Pocho";
            decimal salario = 150000.50M;

            //Act
           

            //Act y Assert
            Assert.ThrowsException<EmpleadoDatosException>(()=> EmpleadoFactory.CrearEmpleado(rol, nombreVacio, apellido, contacto, direccion, salario));

        }

    }
}
