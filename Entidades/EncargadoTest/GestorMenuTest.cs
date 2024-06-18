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
            Plato plato = (Plato)menu.GetPlatosEnMenu().Find(p => p.Nombre == "Pizza");

            Assert.IsNotNull(plato, "El plato 'Pizza' no fue agregado al menu correctamente.");
        }

        [TestMethod]
        public void TestCantidadDeIngredientesEnElPlato_DebeCorroborarLaCantidadDeIngredientesEnElPlato_NoDebeLanzarException()
        {
            IMenu menu = _gestorMenu.GetListaDeMenusQueSeOfrecen().Find(m => m.Nombre == "Desayuno");
            Plato plato = (Plato)menu.GetPlatosEnMenu().Find(p => p.Nombre == "Pizza");

            Assert.IsNotNull(plato, "El plato 'Pizza' no fue agregado al menú correctamente.");
            Assert.IsTrue(plato.Disponibilidad, $"El plato '{plato.Nombre}' no está disponible.");

            int cantidadIngredientes = plato.GetIngredientesDelPlato().Count;
            Assert.IsTrue(cantidadIngredientes >= 2, $"El plato '{plato.Nombre}' debe tener al menos 2 ingredientes.");
        }

        [TestMethod]
        public void TesteamosLaCreacionDeUnMenuAlqueLuegoCreamosUnPlatoYSeAgregaAlMismo_SiSaleBienDeberiaDarOK()
        {
            //Ingredientes que van a formar los platos
            //Ingrediente 1-----------------------------------
            ETipoDeProducto tipoDeProducto1 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto1 = "pollo";
            double cantidad = 20;
            EUnidadDeMedida unidadDeMedida = EUnidadDeMedida.Kilo;
            decimal precio = 20000;

            var mockProveedor1 = new Mock<IProveedor>();
            mockProveedor1.Setup(p => p.Nombre).Returns("Proveedor 1");
            mockProveedor1.Setup(p => p.Cuit).Returns("30-12345678-9");
            mockProveedor1.Setup(p => p.Direccion).Returns("Calle Falsa 123");
            mockProveedor1.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Almacen);
            mockProveedor1.Setup(p => p.MediosDePago).Returns(EMediosDePago.Transferencia);
            mockProveedor1.Setup(p => p.EsAcreedor).Returns(EAcreedor.Si);
            mockProveedor1.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Lunes);
            mockProveedor1.Setup(p => p.ID).Returns(1);
            mockProveedor1.Setup(p => p.ToString()).Returns("ID: 1, Nombre: Proveedor 1, CUIT: 30-12345678-9, Direccion: Calle Falsa 123, Tipo de Producto que Provee: Almacen, Medio de Pago: Transferencia, Es Acreedor? : Si, Dia de Entrega: Lunes");

            //Ingrediente 2 --------------------------------
            ETipoDeProducto tipoDeProducto2 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto2 = "papa";
            double cantidad2 = 20;
            EUnidadDeMedida unidadDeMedida2 = EUnidadDeMedida.Kilo;
            decimal precio2 = 20000;

            var mockProveedor2 = new Mock<IProveedor>();
            mockProveedor2.Setup(p => p.Nombre).Returns("Proveedor 2");
            mockProveedor2.Setup(p => p.Cuit).Returns("31-12345678-8");
            mockProveedor2.Setup(p => p.Direccion).Returns("Calle Falsa 456");
            mockProveedor2.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Carniceria);
            mockProveedor2.Setup(p => p.MediosDePago).Returns(EMediosDePago.Tarjeta);
            mockProveedor2.Setup(p => p.EsAcreedor).Returns(EAcreedor.No);
            mockProveedor2.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Martes);
            mockProveedor2.Setup(p => p.ID).Returns(2);
            mockProveedor2.Setup(p => p.ToString()).Returns("ID: 2, Nombre: Proveedor 2, CUIT: 31-12345678-8, Direccion: Calle Falsa 456, Tipo de Producto que Provee: Carniceria, Medio de Pago: Tarjeta, Es Acreedor? : No, Dia de Entrega: Martes");



            //------------------- GESTOR DE PRODUCTOS -----------------------
            GestorDeProductos gestorDeProductos = new GestorDeProductos();


            //Act
            //Productos que van a estar en la lista del stock (está dentro de GestorProductos)
            gestorDeProductos.CrearProductoParaListaDeStock(tipoDeProducto1, nombreDeProducto1, cantidad, unidadDeMedida, precio, mockProveedor1.Object);
            gestorDeProductos.CrearProductoParaListaDeStock(tipoDeProducto2, nombreDeProducto2, cantidad2, unidadDeMedida2, precio2, mockProveedor2.Object);


            List<IConsumible> listaDeIngredientesDisponibles = gestorDeProductos.GetAllProductosIngrediente();



            IEmpleado cocinero = EmpleadoServiceFactory.CrearEmpleado(ERol.Cocinero, "Pipo", "ERG", "4215554", "Av El Ruttu 5412", 40000M);
            GestorDeMenu gestorMenu = new GestorDeMenu((Cocinero)cocinero);


            //CREAMOS EL MENU
            gestorMenu.CrearMenu("General");
            //Elegimos el producto Ingrediente 1 con su cantidad
            string nombreDelProductoSeleccionado1 = "pollo";
            double cantidadDelProductoSeleccionado1 = 1;
            EUnidadDeMedida unidadDeMedidaParaElProductoSeleccionado1 = EUnidadDeMedida.Kilo;

            //Elegimos el producto Ingrediente 1 con su cantidad
            string nombreDelProductoSeleccionado2 = "papa";
            double cantidadDelProductoSeleccionado2 = 1;
            EUnidadDeMedida unidadDeMedidaParaElProductoSeleccionado2 = EUnidadDeMedida.Kilo;



            gestorMenu.SeleccionarIngredienteParaElPlato(
                listaDeIngredientesDisponibles, nombreDelProductoSeleccionado1, cantidadDelProductoSeleccionado1, unidadDeMedidaParaElProductoSeleccionado1);

            gestorMenu.SeleccionarIngredienteParaElPlato(
                listaDeIngredientesDisponibles, nombreDelProductoSeleccionado2, cantidadDelProductoSeleccionado2, unidadDeMedidaParaElProductoSeleccionado2);


            string nombreDeMenuCreadoPreviamente = "General";
            string nombreDelPlatoACrear = "MilaPapa";

            gestorMenu.AgregarPlatoAMenu(nombreDeMenuCreadoPreviamente, nombreDelPlatoACrear);


            //Verificamos si en la lista de Menu esta el menu que creamos recien

            Assert.IsTrue(gestorMenu.GetListaDeMenusQueSeOfrecen().Count() > 0);

            ////verificamos si es el plato que creamos recien el que esta en la lista de menus.
            var menuGeneral = gestorMenu.GetListaDeMenusQueSeOfrecen().FirstOrDefault(menu => menu.Nombre.Equals(nombreDeMenuCreadoPreviamente, StringComparison.OrdinalIgnoreCase));
            Assert.IsNotNull(menuGeneral);
            Assert.IsTrue(menuGeneral.GetPlatosEnMenu().Any(plato => plato.Nombre.Equals(nombreDelPlatoACrear, StringComparison.OrdinalIgnoreCase)));





        }
    }
}
