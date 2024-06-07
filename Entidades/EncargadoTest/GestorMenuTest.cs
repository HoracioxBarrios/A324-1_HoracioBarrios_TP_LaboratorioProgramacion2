using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Enumerables;
using Entidades.Interfaces;
using Entidades;
using Moq;
using Negocio;

namespace TestEntidades
{
    public class GestorMenuTest
    {
        [TestMethod]
        public void TestCrearMenuYAgregarPlato()
        {
            //Arrange
            //PROVEEDORES
            var mockProveedor1 = new Mock<IProveedor>();
            var mockProveedor2 = new Mock<IProveedor>();
            mockProveedor1.Setup(p => p.Nombre).Returns("Proveedor 1");
            mockProveedor2.Setup(p => p.Nombre).Returns("Proveedor 2");
            //INGREDIENTES
            IConsumible ingrediente1 = new Ingrediente(1, "Tomate", 1.0, EUnidadMedida.Kilo, 5, ETipoDeProducto.Verduleria, mockProveedor1.Object);
            IConsumible ingrediente2 = new Ingrediente(2, "Cebolla", 0.5, EUnidadMedida.Kilo, 3, ETipoDeProducto.Verduleria, mockProveedor2.Object);
            List<IConsumible> ingredientes = new List<IConsumible> { ingrediente1, ingrediente2 };

            //COCINERO
            ICocinero cocinero = new Cocinero(ERol.Cocinero, "Crhistof", "hf", "123456789", "Calle 123", 50000M);

            //GESTOR
            GestorMenu gestorMenu = new GestorMenu(cocinero);

            // MENU
            gestorMenu.CrearMenu("Desayuno");

            // PLATO
            gestorMenu.AgregarPlatoAMenu("Desayuno", "Pizza", ingredientes);

            // el menu fue creado
            IMenu menu = gestorMenu.GetListaDeMenu().Find(m => m.Nombre == "Desayuno");
            // lanza una exception si es null
            Assert.IsNotNull(menu, "El menú 'Desayuno' no fue creado correctamente.");

            // Verificar que el plato fue agregado al menú
            Plato plato = (Plato)menu..Find(p => p.Nombre == "Pizza");
            Assert.IsNotNull(plato, "El plato 'Pizza' no fue agregado al menú correctamente.");
            Assert.AreEqual(ingredientes.Count, ((Plato)plato).Ingredientes.Count, "La cantidad de ingredientes no coincide.");
        }
    }
}
