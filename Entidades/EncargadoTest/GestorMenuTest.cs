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
using Entidades.Services;

namespace Test
{
    [TestClass]
    public class GestorMenuTest
    {

        private ICocinero? _cocinero;
        private IGestorMenu? _gestorMenu;
        private IGestorProductos? _gestorProductos;
        private IGestorContable _gestorContable;




        [TestInitialize]
        public void Init()
        {
            IArca arca = new Arca();
            arca.AgregarDinero(100000M);
            _gestorContable = new GestorContable(arca); // Ahora el Gestor productos necesita esto al momento de Agregar los productos a stock para pagarles a los proveedores

            //CREAMOS LOS PROVEEDORES
            var mockProveedor1 = new Mock<IProveedor>();
            var mockProveedor2 = new Mock<IProveedor>();
            mockProveedor1.Setup(p => p.Nombre).Returns("Proveedor1");
            mockProveedor2.Setup(p => p.Nombre).Returns("Proveedor2");

            //CREAMOS LOS INGREDIENTES
            _gestorProductos = new GestorDeProductos(_gestorContable);

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

            //INSTANCIAMOS EL GESTOR MENU
            _gestorMenu = new GestorDeMenu(_cocinero, _gestorProductos);

        }


        [TestMethod]
        public void TestMenuCreadoCorrectamente_ProbamosSiSeCrea_NoDebeLanzarException()
        {

            
            //CREAMOS EL MENU
            _gestorMenu.CrearMenu("Desayuno");

            IMenu menu = _gestorMenu.ObtenerTodosLosMenus().Find(m => m.Nombre == "Desayuno");
            Assert.IsNotNull(menu, "El menu 'Desayuno' no fue creado correctamente.");           


        }

        [TestMethod]
        public void TestPlatoAgregadoAlMenuCorrectamente_SeDebePoderCorroborarQueElPlatoSeAgregoAlMenu_NoDebelanzarException()
        {
            //CREAMOS LOS PROVEEDORES
            var mockProveedor1 = new Mock<IProveedor>();
            var mockProveedor2 = new Mock<IProveedor>();
            mockProveedor1.Setup(p => p.Nombre).Returns("Proveedor1");
            mockProveedor2.Setup(p => p.Nombre).Returns("Proveedor2");

            //CREAMOS LOS INGREDIENTES
            _gestorProductos = new GestorDeProductos(_gestorContable);

            ETipoDeProducto tipoDeproducto1 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto1 = "Tomate";
            double cantidadParaProducto1 = 10;
            EUnidadDeMedida unidadDeMedidaProd1 = EUnidadDeMedida.Kilo;
            decimal precioProd1 = 1000;

            ETipoDeProducto tipoDeProducto2 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto2 = "Cebolla";
            double cantidadParaProducto2 = 100;
            EUnidadDeMedida unidadDeMedidaProd2 = EUnidadDeMedida.Kilo;
            decimal precioProd2 = 1000;

            IProducto tomate = _gestorProductos.CrearProducto(tipoDeproducto1, nombreDeProducto1, cantidadParaProducto1, unidadDeMedidaProd1, precioProd1, mockProveedor1.Object);
            IProducto cebolla = _gestorProductos.CrearProducto(tipoDeProducto2, nombreDeProducto2, cantidadParaProducto2, unidadDeMedidaProd2, precioProd2, mockProveedor2.Object);


            //  AGREGAMOS AL STOCK LO PRODUCTOS INGREDIENTES 
            _gestorProductos.AgregarProductoAStock(tomate);
            _gestorProductos.AgregarProductoAStock(cebolla);
            //SELECIONAMOS PARA EL COCINERO LOS INGREDIENTES
            _gestorMenu.SelecionarIngredienteParaUnPlato("Tomate", 2, EUnidadDeMedida.Kilo); 
            _gestorMenu.SelecionarIngredienteParaUnPlato("Cebolla", 2, EUnidadDeMedida.Kilo);

            //CREAMOS EL MENU
            _gestorMenu.CrearMenu("Desayuno");

            //Creamos el plato ya que el cocinero elijio y ya tiene una lista con los ingredientes SELECCIONADOS


            //Datos para el plato
            string nombreDelPlato = "Milapapa";
            int tiempoPreparacion = 10;
            EUnidadDeTiempo unidadDeTiempo = EUnidadDeTiempo.Segundos;
            //Creamos el plato
            IConsumible plato1 = _gestorMenu.CrearPlato(nombreDelPlato, tiempoPreparacion, unidadDeTiempo);

            //aGREGAMOS EL PLATO AL MENU
            _gestorMenu.AgregarPlatoAMenu("Desayuno", plato1);

            //Buscamos que este el menu
            IMenu menu = _gestorMenu.ObtenerTodosLosMenus().Find(m => m.Nombre == "Desayuno");

            //Buscamos que este el plato en el menu
            Plato plato = (Plato)menu.ObtenerPlatosEnMenu().Find(p => p.Nombre == "Milapapa");

            Assert.IsNotNull(plato, "El plato 'Pizza' no fue agregado al menu correctamente.");
        }



        [TestMethod]
        public void TestCantidadDeIngredientesEnElPlato_DebeCorroborarLaCantidadDeIngredientesEnElPlato_NoDebeLanzarException()
        {
            //SELECIONAMOS PARA EL COCINERO LOS INGREDIENTES
            _gestorMenu.SelecionarIngredienteParaUnPlato("Tomate", 2, EUnidadDeMedida.Kilo);
            _gestorMenu.SelecionarIngredienteParaUnPlato("Cebolla", 2, EUnidadDeMedida.Kilo);

            //CREAMOS EL MENU
            _gestorMenu.CrearMenu("Desayuno");


            //Creamos el plato ya que el cocinero elijio y ya tiene una lista con los ingredientes SELECCIONADOS



            //Datos para el plato
            string nombreDelPlato = "Milapapa";
            int tiempoPreparacion = 10;
            EUnidadDeTiempo unidadDeTiempo = EUnidadDeTiempo.Segundos;
            //Creamos el plato
            IConsumible plato1 = _gestorMenu.CrearPlato(nombreDelPlato, tiempoPreparacion, unidadDeTiempo);


            //aGREGAMOS EL PLATO AL MENU
            _gestorMenu.AgregarPlatoAMenu("Desayuno", plato1);



            IMenu menu = _gestorMenu.ObtenerTodosLosMenus().Find(m => m.Nombre == "Desayuno");

            Plato plato = (Plato)menu.ObtenerPlatosEnMenu().Find(p => p.Nombre == "Milapapa");

            int cantidadIngredientes = plato.ObtenerIngredientes().Count;
            Assert.IsTrue(cantidadIngredientes >= 2, $"El plato '{plato.Nombre}' debe tener al menos 2 ingredientes.");
        }

        [TestMethod]
        public void TesteamosLaCreacionDeUnMenuYCorroboramosSuExistenciaEnLaListaDeMenus_SiSaleBienNoDeberiaDarException()
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
            List<IConsumible> listaDeIngredientesEnStock = _gestorProductos.ObtenerTodosLosProductosIngrediente();


            //INSTANCIAMOS EL COCINERO
            IEmpleado cocinero = EmpleadoServiceFactory.CrearEmpleado(ERol.Cocinero, "Pipo", "ERG", "4215554", "Av El Ruttu 5412", 40000M);


            //INTANCIAMOSEL GESTOR MENU
            _gestorMenu = new GestorDeMenu((Cocinero)cocinero, _gestorProductos);

            string nombreDelMenu = "General";
            //CREAMOS EL MENU
            _gestorMenu.CrearMenu(nombreDelMenu);


            //Elegimos el producto Ingrediente 1 con su cantidad
            string nombreDelProductoSeleccionado1 = "pollo";
            double cantidadDelProductoSeleccionado1 = 1;
            EUnidadDeMedida unidadDeMedidaParaElProductoSeleccionado1 = EUnidadDeMedida.Kilo;

            //Elegimos el producto Ingrediente 1 con su cantidad
            string nombreDelProductoSeleccionado2 = "papa";
            double cantidadDelProductoSeleccionado2 = 1;
            EUnidadDeMedida unidadDeMedidaParaElProductoSeleccionado2 = EUnidadDeMedida.Kilo;



            _gestorMenu.SelecionarIngredienteParaUnPlato( nombreDelProductoSeleccionado1, cantidadDelProductoSeleccionado1, unidadDeMedidaParaElProductoSeleccionado1);

            _gestorMenu.SelecionarIngredienteParaUnPlato(nombreDelProductoSeleccionado2, cantidadDelProductoSeleccionado2, unidadDeMedidaParaElProductoSeleccionado2);





            //creamos el pLato ya que el cocinero elijio los ingredientes y lostiene (en una lista interna de INGREDIENTES SELECCIONADOS)
            //CREAMOS EL PLATO
            string nombreDelPlatoACrear = "MilaPapa";
            int tiempoPreparacion = 10;
            EUnidadDeTiempo unidadTiempo = EUnidadDeTiempo.Segundos;
            IConsumible plato1 = _gestorMenu.CrearPlato(nombreDelPlatoACrear, tiempoPreparacion, unidadTiempo);

            _gestorMenu.AgregarPlatoAMenu(nombreDelMenu, plato1);


            //Verificamos si en la lista de Menu esta el menu que creamos recien


            Assert.IsTrue(_gestorMenu.ObtenerTodosLosMenus().Count() > 0);
            var menuGeneral = _gestorMenu.ObtenerTodosLosMenus().FirstOrDefault(menu => menu.Nombre.Equals(nombreDelMenu, StringComparison.OrdinalIgnoreCase));

            Assert.IsNotNull(menuGeneral);

        }



        [TestMethod]
        public void OrdenarPlatosPorIngrediente_OrdenaPlatosCorrectamente()
        {
            // Arrange
            // Crear ingredientes
            var ingrediente1 = new Ingrediente(1, "Tomate", 100, EUnidadDeMedida.Gramo, 2, ETipoDeProducto.Ingrediente, null);
            var ingrediente2 = new Ingrediente(2, "Tomate", 300, EUnidadDeMedida.Gramo, 1.5m, ETipoDeProducto.Ingrediente, null);
            var ingrediente3 = new Ingrediente(3, "Tomate", 150, EUnidadDeMedida.Gramo, 1, ETipoDeProducto.Ingrediente, null);

            // Crear platos
            var plato1 = new Plato("Plato1", new List<IConsumible> { ingrediente1 }, 10, EUnidadDeTiempo.Minutos);
            var plato2 = new Plato("Plato2", new List<IConsumible> { ingrediente2 }, 10, EUnidadDeTiempo.Minutos);
            var plato3 = new Plato("Plato3", new List<IConsumible> { ingrediente3 }, 10, EUnidadDeTiempo.Minutos);

            // Crear lista de consumibles y establecerla en el gestor de menú
            var listaConsumibles = new List<IConsumible> { plato1, plato2, plato3 };
            typeof(GestorDeMenu).GetField("_ListaGeneralDeConsumiblesLocal", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(_gestorMenu, listaConsumibles);

            // Act
            GestorDeMenu gestorMenu = (GestorDeMenu)_gestorMenu;
            var platosOrdenados = gestorMenu.OrdenarPlatosPorIngrediente("Tomate");

            // Assert
            Assert.AreEqual(plato2, platosOrdenados[0]);
            Assert.AreEqual(plato3, platosOrdenados[1]);
            Assert.AreEqual(plato1, platosOrdenados[2]);
        }





        [TestMethod]
        public void TestObtenerPlatosNoDisponibles_PlatosNoDisponiblesCorrectamente()
        {
            // Arrange
            // Crear platos con disponibilidad falsa
            var plato1 = new Plato("Plato1", new List<IConsumible>(), 10, EUnidadDeTiempo.Minutos);
            plato1.Disponibilidad = false;

            var plato2 = new Plato("Plato2", new List<IConsumible>(), 15, EUnidadDeTiempo.Minutos);
            plato2.Disponibilidad = false;

            // Lista de consumibles locales (platos)
            List<IConsumible> listaConsumibles = new List<IConsumible> { plato1, plato2 };
            typeof(GestorDeMenu).GetField("_ListaGeneralDeConsumiblesLocal",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(_gestorMenu, listaConsumibles);

            // Act
            List<IConsumible> platosNoDisponibles = _gestorMenu.ObtenerPlatosNoDisponibles();

            // Assert
            Assert.AreEqual(2, platosNoDisponibles.Count);
            Assert.IsTrue(platosNoDisponibles.Contains(plato1));
            Assert.IsTrue(platosNoDisponibles.Contains(plato2));
        }
    }
}
