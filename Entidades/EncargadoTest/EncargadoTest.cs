using Entidades;
using Entidades.Enumerables;
namespace EncargadoTest
{
    [TestClass]
    public class EncargadoTest
    {
        [TestMethod]
        public void VerificarInstanciaDeEncargado_PruebaSiElSalarioObtenido_EsIgualDaTrue()
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
    }
}