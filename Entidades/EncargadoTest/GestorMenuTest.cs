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
    [TestClass]
    public class GestorMenuTest
    {

        private IConsumible _ingrediente1;
        private IConsumible _ingrediente2;
        private List<IConsumible> _listaDeIngredientes;
        private ICocinero _cocinero;
        private GestorMenu _gestorMenu;

        [TestInitialize]
        public void Setup()
        {
            //Mock PROVEEDORES
            var mockProveedor1 = new Mock<IProveedor>();
            var mockProveedor2 = new Mock<IProveedor>();
            mockProveedor1.Setup(p => p.Nombre).Returns("Proveedor1");
            mockProveedor2.Setup(p => p.Nombre).Returns("Proveedor2");

            //cremos los INGREDIENTES
            _ingrediente1 = new Ingrediente(1, "Tomate", 1.0, EUnidadMedida.Kilo, 5, ETipoDeProducto.Verduleria, mockProveedor1.Object);
            _ingrediente2 = new Ingrediente(2, "Cebolla", 0.5, EUnidadMedida.Kilo, 3, ETipoDeProducto.Verduleria, mockProveedor2.Object);

            _listaDeIngredientes = new List<IConsumible>();
            _listaDeIngredientes.Add(_ingrediente1);
            _listaDeIngredientes.Add(_ingrediente2);


            //Creamos el COCINERO
            _cocinero = new Cocinero(ERol.Cocinero, "Christof", "F", "115448", "Calle inexistente 5", 50000M);

            //GESTOR MENU
            _gestorMenu = new GestorMenu(_cocinero);

            //creamos un MENU (se agrega a la lista de menús)
            _gestorMenu.CrearMenu("Desayuno");


            //Creamos el PLATO y lo agregamos al MENU (SE ENCARGA EL GESTOR MENU POR DENTRO)
            _gestorMenu.AgregarPlatoAMenu("Desayuno", "Pizza", _listaDeIngredientes);

        }

        [TestMethod]
        public void TestMenuCreadoCorrectamente_ProbamosSiSeCrea_NoDebeLanzarException()
        {
            IMenu menu = _gestorMenu.GetListaDeMenu().Find(m => m.Nombre == "Desayuno");

            Assert.IsNotNull(menu, "El menu 'Desayuno' no fue creado correctamente.");
        }

        [TestMethod]
        public void TestPlatoAgregadoAlMenuCorrectamente_SeDebePoderCorroborarQueElPlatoSeAgregoAlMenu_NoDebelanzarException()
        {
            IMenu menu = _gestorMenu.GetListaDeMenu().Find(m => m.Nombre == "Desayuno");
            Plato plato = (Plato)menu.ObtenerPlatosInMenu().Find(p => p.Nombre == "Pizza");


            Assert.IsNotNull(plato, "El plato 'Pizza' no fue agregado al menu correctamente.");
        }
        [TestMethod]
        public void TestCantidadDeIngredientesEnElPlato_DebeCorroborarLaCantidadDeIngredientesEnElPlato_NoDebeLanzarException()
        {
            IMenu menu = _gestorMenu.GetListaDeMenu().Find(m => m.Nombre == "Desayuno");
            Plato plato = (Plato)menu.ObtenerPlatosInMenu().Find(p => p.Nombre == "Pizza");
            Assert.AreEqual(_listaDeIngredientes.Count, plato.GetIngredientesDelPlato().Count, "La cantidad de ingredientes no coincide.");
        }










        //Test que se fracciono en test mas pequeños (que estan arriba)

        //    [TestMethod]
        //    public void TestCrearMenuYAgregarPlato_CreamosLosIngredientesTambienCreamosElCocineroCreamosElGestorMenuParaCrearElPlatoYAgregamosEsteAUnMenu_VerificamosQueElPlatoSeCreaYSeAgrega()
        //    {
        //        //Arrange
        //        //PROVEEDORES
        //        var mockProveedor1 = new Mock<IProveedor>();
        //        var mockProveedor2 = new Mock<IProveedor>();
        //        mockProveedor1.Setup(p => p.Nombre).Returns("Proveedor 1");
        //        mockProveedor2.Setup(p => p.Nombre).Returns("Proveedor 2");
        //        //INGREDIENTES
        //        IConsumible ingrediente1 = new Ingrediente(1, "Tomate", 1.0, EUnidadMedida.Kilo, 5, ETipoDeProducto.Verduleria, mockProveedor1.Object);
        //        IConsumible ingrediente2 = new Ingrediente(2, "Cebolla", 0.5, EUnidadMedida.Kilo, 3, ETipoDeProducto.Verduleria, mockProveedor2.Object);
        //        List<IConsumible> ingredientes = new List<IConsumible> { ingrediente1, ingrediente2 };

        //        //COCINERO
        //        ICocinero cocinero = new Cocinero(ERol.Cocinero, "Crhistof", "hf", "123456789", "Calle 123", 50000M);

        //        //GESTOR
        //        GestorMenu gestorMenu = new GestorMenu(cocinero);

        //        // MENU
        //        gestorMenu.CrearMenu("Desayuno");

        //        // PLATO
        //        gestorMenu.AgregarPlatoAMenu("Desayuno", "Pizza", ingredientes);





        //        // el menu fue creado y agregado a una lista de menu que está en Gestor Menú
        //        IMenu menu = gestorMenu.GetListaDeMenu().Find(m => m.Nombre == "Desayuno");
        //        // lanza una exception si el menu es es null
        //        Assert.IsNotNull(menu, "El menú 'Desayuno' no fue creado correctamente.");

        //        // Verificamos que el PLATO fue agregado al menú
        //        Plato plato = (Plato)menu.ObtenerPlatosInMenu().Find(p => p.Nombre == "Pizza");
        //        Assert.IsNotNull(plato, "El plato 'Pizza' no fue agregado al menú correctamente.");
        //        Assert.AreEqual(ingredientes.Count, plato.GetIngredientesDelPlato().Count, "La cantidad de ingredientes no coincide.");
        //    }
    }
}
