using Entidades;
using Entidades.Enumerables;
using Entidades.Interfaces;
namespace EncargadoTest
{
    [TestClass]
    public class EncargadoTest
    {
        [TestMethod]
        public void VerificaInstanciaDeEncargado_SiElSalarioObtenidoEsIgual_DaTrue()
        {
            //Arrange
            string nombre = "Pibe";
            string apellido = "Mc Test";
            string contacto = "1144553311";
            ERol rol = ERol.Encargado;
            string direccion = "Av. San Pocho";
            decimal salario = 150000.50M;
            //act
            Encargado encargado = new Encargado(nombre, apellido,contacto,direccion,salario);
            decimal salarioObtenido = encargado.Salario;

            //assert
            Assert.AreEqual(salarioObtenido, salario);

        }

        [TestMethod]
        public void VerificaIntanciaDeEncargado_SiEncargadoEsIEncargado_DaTrue()
        {
            //Arrange
            string nombre = "Robert";
            string apellido = "Test";
            string contacto = "1144553311";
            string direccion = "Av. San Pocho";
            ERol rol = ERol.Encargado;
            decimal salario = 150000.50M;
            //Act
            Encargado encargado = new Encargado(nombre, apellido, contacto, direccion, salario);

            //Assert
            Assert.IsInstanceOfType(encargado, typeof(IEmpleado));
        }
    }
}