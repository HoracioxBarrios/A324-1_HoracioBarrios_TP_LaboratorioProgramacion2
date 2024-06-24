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


        private IGestorMenu _gestorMenu;
        private IGestorProductos _gestorProductos;





        [TestInitialize]
        public void Setup()
        {
            //CREAMOS LOS PROVEEDORES
            var mockProveedor1 = new Mock<IProveedor>();
            var mockProveedor2 = new Mock<IProveedor>();
            mockProveedor1.Setup(p => p.Nombre).Returns("Proveedor1");
            mockProveedor2.Setup(p => p.Nombre).Returns("Proveedor2");

            //CREAMOS LOS INGREDIENTES
            _gestorProductos = new GestorDeProductos();

            ETipoDeProducto tipoDeproducto1 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto1 = "Tomate";
            double cantidadParaproducto1 = 10;
            EUnidadDeMedida unidadDeMedidaProd1 = EUnidadDeMedida.Kilo;
            decimal precioProd1 = 1000;

            ETipoDeProducto tipoDeproducto2 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto2 = "Cebolla";
            double cantidadParaproducto2 = 10;
            EUnidadDeMedida unidadDeMedidaProd2 = EUnidadDeMedida.Kilo;
            decimal precioProd2 = 1000;

            IProducto tomate = _gestorProductos.CrearProducto(tipoDeproducto1, nombreDeProducto1, cantidadParaproducto1, unidadDeMedidaProd1, precioProd1, mockProveedor1.Object);
            IProducto cebolla = _gestorProductos.CrearProducto(tipoDeproducto2, nombreDeProducto2, cantidadParaproducto2, unidadDeMedidaProd2, precioProd2, mockProveedor2.Object);

            //  AGREGAMOS AL STOCK LO PRODUCTOS INGREDIENTES 
            _gestorProductos.AgregarProductoAStock(tomate);
            _gestorProductos.AgregarProductoAStock(cebolla);




            //Instanciamos el COCINERO 
            _cocinero = new Cocinero(ERol.Cocinero, "Christof", "F", "115448", "Calle inexistente 5", 50000M);


            _gestorMenu = new GestorDeMenu(_cocinero, _gestorProductos);
            //CREAMOS EL MENU
            _gestorMenu.CrearMenu("Desayuno");


            //SELECIONAMOS PARA EL COCINERO LOS INGREDIENTES
            _gestorMenu.SelecionarIngrediente("Tomate", 2, EUnidadDeMedida.Kilo);
            _gestorMenu.SelecionarIngrediente( "Cebolla", 2, EUnidadDeMedida.Kilo);



            //Creamos el plato ya que el cocinero elijio y ya tiene una lista con los ingredientes SELECCIONADOS
            //CREAMOS EL PLATO
            string nombreDelPlato = "Milapapa";
            int tiempoDePreparacion = 30;
            EUnidadDeTiempo unidadDeTiempo = EUnidadDeTiempo.Segundos;
            IConsumible plato1 = _gestorMenu.CrearPlato(nombreDelPlato, tiempoDePreparacion, unidadDeTiempo);

            //aGREGAMOS EL PLATO AL MENU
            _gestorMenu.AgregarPlatoAMenu("Desayuno", plato1);
        }


        [TestMethod]
        public void TestMenuCreadoCorrectamente_ProbamosSiSeCrea_NoDebeLanzarException()
        {
            IMenu menu = _gestorMenu.GetAllMenus().Find(m => m.Nombre == "Desayuno");

            Assert.IsNotNull(menu, "El menu 'Desayuno' no fue creado correctamente.");
        }

        [TestMethod]
        public void TestPlatoAgregadoAlMenuCorrectamente_SeDebePoderCorroborarQueElPlatoSeAgregoAlMenu_NoDebelanzarException()
        {
            IMenu menu = _gestorMenu.GetAllMenus().Find(m => m.Nombre == "Desayuno");
            Plato plato = (Plato)menu.GetPlatosEnMenu().Find(p => p.Nombre == "Milapapa");

            Assert.IsNotNull(plato, "El plato 'Pizza' no fue agregado al menu correctamente.");
        }

        [TestMethod]
        public void TestCantidadDeIngredientesEnElPlato_DebeCorroborarLaCantidadDeIngredientesEnElPlato_NoDebeLanzarException()
        {
            IMenu menu = _gestorMenu.GetAllMenus().Find(m => m.Nombre == "Desayuno");
            Plato plato = (Plato)menu.GetPlatosEnMenu().Find(p => p.Nombre == "Milapapa");

            Assert.IsNotNull(plato, "El plato 'Pizza' no fue agregado al menú correctamente.");
            Assert.IsTrue(plato.Disponibilidad, $"El plato '{plato.Nombre}' no está disponible.");

            //int cantidadIngredientes = plato.GetIngredientesDelPlato().Count;
            //Assert.IsTrue(cantidadIngredientes >= 2, $"El plato '{plato.Nombre}' debe tener al menos 2 ingredientes.");
        }

        [TestMethod]
        public void TesteamosLaCreacionDeUnMenuAlqueLuegoCreamosUnPlatoYSeAgregaAlMismo_SiSaleBienDeberiaDarOK()
        {
            //Ingredientes que van a formar los platos
            //DATOS PARA Ingrediente 1-----------------------------------
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

            //DATOS PARA Ingrediente 2 --------------------------------
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



            //Act
            //Productos que van a estar en la lista del stock (está dentro de GestorProductos)
            IProducto pollo = _gestorProductos.CrearProducto(tipoDeProducto1, nombreDeProducto1, cantidad, unidadDeMedida, precio, mockProveedor1.Object);
            IProducto papa = _gestorProductos.CrearProducto(tipoDeProducto2, nombreDeProducto2, cantidad2, unidadDeMedida2, precio2, mockProveedor2.Object);

            //ahora AGREGAMOS AL STOCK
            _gestorProductos.AgregarProductoAStock(pollo);
            _gestorProductos.AgregarProductoAStock(papa);

            //PODEMOS VER LOS PRODUCTOS QUE SON INGREDIENTES EN STOCK
            List<IConsumible> listaDeIngredientesEnStock = _gestorProductos.ReadAllProductosIngredientes();


            //usamos EL COCINERO         


            //INTANCIAMOSEL GESTOR MENU
            _gestorMenu = new GestorDeMenu(_cocinero, _gestorProductos);


            //CREAMOS EL MENU
            _gestorMenu.CrearMenu("General");


            //Elegimos el producto Ingrediente 1 con su cantidad
            string nombreDelProductoSeleccionado1 = "pollo";
            double cantidadDelProductoSeleccionado1 = 1;
            EUnidadDeMedida unidadDeMedidaParaElProductoSeleccionado1 = EUnidadDeMedida.Kilo;

            //Elegimos el producto Ingrediente 1 con su cantidad
            string nombreDelProductoSeleccionado2 = "papa";
            double cantidadDelProductoSeleccionado2 = 1;
            EUnidadDeMedida unidadDeMedidaParaElProductoSeleccionado2 = EUnidadDeMedida.Kilo;



            _gestorMenu.SelecionarIngrediente( nombreDelProductoSeleccionado1, cantidadDelProductoSeleccionado1, unidadDeMedidaParaElProductoSeleccionado1);

            _gestorMenu.SelecionarIngrediente(nombreDelProductoSeleccionado2, cantidadDelProductoSeleccionado2, unidadDeMedidaParaElProductoSeleccionado2);


            string nombreDeMenuCreadoPreviamente = "General";
            int tiempodePreparacion = 30;
            EUnidadDeTiempo unidadDeTiempo = EUnidadDeTiempo.Segundos;
            string nombreDelPlatoACrear = "MilaPapa";

            //creamos el pLato ya que el cocinero elijio los ingredientes y lostiene (en una lista interna de INGREDIENTES SELECCIONADOS)
            //CREAMOS EL PLATO
            IConsumible plato1 = _gestorMenu.CrearPlato(nombreDelPlatoACrear, tiempodePreparacion, unidadDeTiempo);
            _gestorMenu.AgregarPlatoAMenu(nombreDeMenuCreadoPreviamente, plato1);


            //Verificamos si en la lista de Menu esta el menu que creamos recien

            Assert.IsTrue(_gestorMenu.GetAllMenus().Count() > 0);

            ////verificamos si es el plato que creamos recien el que esta en la lista de menus.
            var menuGeneral = _gestorMenu.GetAllMenus().FirstOrDefault(menu => menu.Nombre.Equals(nombreDeMenuCreadoPreviamente, StringComparison.OrdinalIgnoreCase));
            Assert.IsNotNull(menuGeneral);
            Assert.IsTrue(menuGeneral.GetPlatosEnMenu().Any(plato => plato.Nombre.Equals(nombreDelPlatoACrear, StringComparison.OrdinalIgnoreCase)));





        }
    }
}
