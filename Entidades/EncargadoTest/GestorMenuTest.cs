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

namespace Test
{
    [TestClass]
    public class GestorMenuTest
    {

        private IConsumible _ingrediente1;
        private IConsumible _ingrediente2;
        private List<IConsumible> _listaDeIngredientes;
        private ICocinero _cocinero;
        private GestorDeMenu _gestorMenu;
        [TestInitialize]
        public void Setup()
        {
            var mockProveedor1 = new Mock<IProveedor>();
            var mockProveedor2 = new Mock<IProveedor>();
            mockProveedor1.Setup(p => p.Nombre).Returns("Proveedor1");
            mockProveedor2.Setup(p => p.Nombre).Returns("Proveedor2");

            _ingrediente1 = new Ingrediente(1, "Tomate", 10, EUnidadDeMedida.Kilo, 5, ETipoDeProducto.Verduleria, mockProveedor1.Object);
            _ingrediente2 = new Ingrediente(2, "Cebolla", 20, EUnidadDeMedida.Kilo, 3, ETipoDeProducto.Verduleria, mockProveedor2.Object);

            _listaDeIngredientes = new List<IConsumible> { _ingrediente1, _ingrediente2 };

            _cocinero = new Cocinero(ERol.Cocinero, "Christof", "F", "115448", "Calle inexistente 5", 50000M);
            _gestorMenu = new GestorDeMenu(_cocinero);

            _gestorMenu.CrearMenu("Desayuno");

            _gestorMenu.SeleccionarIngredienteParaElPlato(_listaDeIngredientes, "Tomate", 2, EUnidadDeMedida.Kilo);
            _gestorMenu.SeleccionarIngredienteParaElPlato(_listaDeIngredientes, "Cebolla", 2, EUnidadDeMedida.Kilo);

            _gestorMenu.AgregarPlatoAMenu("Desayuno", "Pizza");
        }


        [TestMethod]
        public void TestMenuCreadoCorrectamente_ProbamosSiSeCrea_NoDebeLanzarException()
        {
            IMenu menu = _gestorMenu.GetListaDeMenusQueSeOfrecen().Find(m => m.Nombre == "Desayuno");

            Assert.IsNotNull(menu, "El menu 'Desayuno' no fue creado correctamente.");
        }

        [TestMethod]
        public void TestPlatoAgregadoAlMenuCorrectamente_SeDebePoderCorroborarQueElPlatoSeAgregoAlMenu_NoDebelanzarException()
        {
            IMenu menu = _gestorMenu.GetListaDeMenusQueSeOfrecen().Find(m => m.Nombre == "Desayuno");
            Plato plato = (Plato)menu.GetPlatosInMenu().Find(p => p.Nombre == "Pizza");

            Assert.IsNotNull(plato, "El plato 'Pizza' no fue agregado al menu correctamente.");
        }

        [TestMethod]
        public void TestCantidadDeIngredientesEnElPlato_DebeCorroborarLaCantidadDeIngredientesEnElPlato_NoDebeLanzarException()
        {
            IMenu menu = _gestorMenu.GetListaDeMenusQueSeOfrecen().Find(m => m.Nombre == "Desayuno");
            Plato plato = (Plato)menu.GetPlatosInMenu().Find(p => p.Nombre == "Pizza");

            Assert.IsNotNull(plato, "El plato 'Pizza' no fue agregado al menú correctamente.");
            Assert.IsTrue(plato.Disponibilidad, $"El plato '{plato.Nombre}' no está disponible.");

            int cantidadIngredientes = plato.GetIngredientesDelPlato().Count;
            Assert.IsTrue(cantidadIngredientes >= 2, $"El plato '{plato.Nombre}' debe tener al menos 2 ingredientes.");
        }


    }
}
