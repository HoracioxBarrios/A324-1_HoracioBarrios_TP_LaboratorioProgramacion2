using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;
using Entidades.Services;
using Moq;
using Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class GestorDePedidosTest
    {

        private IGestorMenu _gestorMenu;
        private IGestorProductos _gestorProductos;
        private ICocinero _cocinero;
        private IMesero _mesero;
        private IEncargado _encargado;
        private List<IConsumible> _consumublesSelecionadosParaPedido;





        [TestInitialize]
        public void Setup()
        {

            //CREAMOS Y PONEMOS EN STOCK LOS INGREDIENTES que van a formar los platos.
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

            //---- Instanciamos un encargado
            IEmpleado encargado = EmpleadoServiceFactory.CrearEmpleado(ERol.Encargado, "Hui", "yu", "45213", "Av pollo 12", 45000M);
            _encargado = (IEncargado)encargado;
            //------------------- GESTOR DE PRODUCTOS -----------------------
            _gestorProductos = new GestorDeProductos();
            
            //Creamos los productos
            IProducto pollo = _gestorProductos.CrearProducto(tipoDeProducto1, nombreDeProducto1, cantidad, unidadDeMedida, precio, mockProveedor1.Object);
            IProducto papa = _gestorProductos.CrearProducto(tipoDeProducto2, nombreDeProducto2, cantidad2, unidadDeMedida2, precio2, mockProveedor2.Object);

            //AGREGAMOS AL STOCK
            _gestorProductos.AgregarProductoAStock(pollo);
            _gestorProductos.AgregarProductoAStock(papa);


            //podriamos Obtenemos la lista de ingredientes. si queremos visualizar que hay
            List<IConsumible> listaDeIngredientesDisponibles = _gestorProductos.ObtenerTodosLosProductosIngrediente();


            //-- intanciamos el COCINERO --
            IEmpleado cocinero = EmpleadoServiceFactory.CrearEmpleado(ERol.Cocinero, "Pipo", "ERG", "4215554", "Av El Ruttu 5412", 40000M);
            //Tengo que usar GestorEmepleado 
            _cocinero = (ICocinero)cocinero;

            //------------------------- GESTOR MENU --------------------------
            _gestorMenu = new GestorDeMenu(_cocinero, _gestorProductos);

            // -- CREAMOS EL MENU --
            string nombreDelmenu = "General";


            _gestorMenu.CrearMenu(nombreDelmenu);




            // -- ELEGIMOS LOS INGREDIENTES PARA EL PLATO
            //Elegimos el producto Ingrediente 1 con su cantidad
            string nombreDelProductoSeleccionado1 = "pollo";
            double cantidadDelProductoSeleccionado1 = 1;
            EUnidadDeMedida unidadDeMedidaParaElProductoSeleccionado1 = EUnidadDeMedida.Kilo;

            //Elegimos el producto Ingrediente 2 con su cantidad
            string nombreDelProductoSeleccionado2 = "papa";
            double cantidadDelProductoSeleccionado2 = 1;
            EUnidadDeMedida unidadDeMedidaParaElProductoSeleccionado2 = EUnidadDeMedida.Kilo;


            //selecciona INGREDIENTES para el COCINERO (lo guardara en su lista de ingredientes seleccionados)
            _gestorMenu.SelecionarIngredienteParaUnPlato(nombreDelProductoSeleccionado1, cantidadDelProductoSeleccionado1, unidadDeMedidaParaElProductoSeleccionado1);

            _gestorMenu.SelecionarIngredienteParaUnPlato( nombreDelProductoSeleccionado2, cantidadDelProductoSeleccionado2, unidadDeMedidaParaElProductoSeleccionado2);






            //CREAMOS EL PLATO
            string nombreDelPlatoACrear = "MilaPapa";
            int tiempoDePreparacion = 30;
            EUnidadDeTiempo unidadDeTiempo = EUnidadDeTiempo.Segundos;

            IConsumible plato1 = _gestorMenu.CrearPlato(nombreDelPlatoACrear, tiempoDePreparacion, unidadDeTiempo);

            //podemos saber el precio de costo del plato
            decimal precioDeCostoDelPlato = plato1.CalcularPrecioDeCosto();


            // -------- AHORA SE AGREGA EL PLATO AL MENU ------------
            _gestorMenu.AgregarPlatoAMenu("General", plato1);

            //Establecemos el precio de venta del plato (cuando esta agregado dentro del gestor menu) ------> <<<ESTABLECEMOS PRECIO VENTA>>>> 
            _gestorMenu.EstablecerPrecioAProducto((IEstablecedorDePrecios)_encargado, "MilaPapa", 2500);

            // ------------- EN EL MENU PUEDE HABER BEBIDAS --------------
            //PROVEEDOR MOCK
            var mockProveedor4 = new Mock<IProveedor>();
            mockProveedor4.Setup(p => p.Nombre).Returns("Proveedor 1");
            mockProveedor4.Setup(p => p.Cuit).Returns("30-12345678-9");
            mockProveedor4.Setup(p => p.Direccion).Returns("Calle Falsa 123");
            mockProveedor4.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Almacen);
            mockProveedor4.Setup(p => p.MediosDePago).Returns(EMediosDePago.Transferencia);
            mockProveedor4.Setup(p => p.EsAcreedor).Returns(EAcreedor.Si);
            mockProveedor4.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Lunes);
            mockProveedor4.Setup(p => p.ID).Returns(1);
            mockProveedor4.Setup(p => p.ToString()).Returns("ID: 1, Nombre: Proveedor 1, CUIT: 30-12345678-9, Direccion: Calle Falsa 123, Tipo de Producto que Provee: Almacen, Medio de Pago: Transferencia, Es Acreedor? : Si, Dia de Entrega: Lunes");
            //IPRODUCTO BEBIDA 1
            ETipoDeProducto tipoDeProductoBebida1 = ETipoDeProducto.Bebida;
            string nombreBebida1 = "CocaCola";
            double cantidadBebida1 = 20;
            EUnidadDeMedida eUnidadDeMedidaBebida1 = EUnidadDeMedida.Unidad;
            decimal precioBebida1 = 20000;
            IProveedor proveedorBebida1 = mockProveedor4.Object;
            ECategoriaConsumible categoriaConsumibleBebida1 = ECategoriaConsumible.Bebida;
            EClasificacionBebida clasificacionBebida1 = EClasificacionBebida.Sin_Añcohol;

            // BEBIDA 2
            ETipoDeProducto tipoDeProductoBebida2 = ETipoDeProducto.Bebida;
            string nombreBebida2 = "Cerveza QUilmes";
            double cantidadBebida2 = 10;
            EUnidadDeMedida eUnidadDeMedidaBebida2 = EUnidadDeMedida.Unidad;
            decimal precioBebida2 = 10000;
            IProveedor proveedorBebida2 = mockProveedor4.Object;
            ECategoriaConsumible categoriaConsumibleBebida2 = ECategoriaConsumible.Bebida;
            EClasificacionBebida clasificacionBebida2 = EClasificacionBebida.Con_Alcohol;

            IProducto coca = _gestorProductos.CrearProducto(tipoDeProductoBebida1, nombreBebida1, cantidadBebida1, eUnidadDeMedidaBebida1, precioBebida1, proveedorBebida1, categoriaConsumibleBebida1, clasificacionBebida1);
            IProducto cerveza = _gestorProductos.CrearProducto(tipoDeProductoBebida2, nombreBebida2, cantidadBebida2, eUnidadDeMedidaBebida2, precioBebida2, proveedorBebida2, categoriaConsumibleBebida2, clasificacionBebida2);

            //Agregamos los productos BEBIDAS
            _gestorProductos.AgregarProductoAStock(coca);
            _gestorProductos.AgregarProductoAStock(cerveza);


            List<IConsumible> bebidasDelStock = _gestorProductos.OtenerTodosLosProductosBebidas();

            // AGREGAMOS LAS BEBIDAS DEL STOCK AL MENÚ
            _gestorMenu.AgregarBebidasAMenu(nombreDelmenu, bebidasDelStock);

            //CUANDO AGREGAMOS LA BEBIDA AL gestor Menu --------------> <<<<Establecemos el PRECIO DE VENTA DE LA BEBIDA>>>>
            //podemos saber el precio unitario de las bebida
            decimal precioUnitarioCoca = coca.CalcularPrecioDeCosto();
            decimal precioUnitarioCerveza = cerveza.CalcularPrecioDeCosto();

            _gestorMenu.EstablecerPrecioAProducto((IEstablecedorDePrecios)_encargado, "CocaCola", 1500M);
            _gestorMenu.EstablecerPrecioAProducto((IEstablecedorDePrecios)_encargado, "Cerveza QUilmes", 1500M);
            //***************** Teniendo el Menú listo se puede mostrar y podriamos crear el pedido: --------->>>>>>>>>>>



            List<IConsumible> consumublesSelecionadosParaPedido = new List<IConsumible>(); // listo para los consumibles del pedido

            IMenu menuSeleccionado = _gestorMenu.ObtenerMenuPorNombre("General");//selecionamos un menu

            IConsumible platoSelecionado = menuSeleccionado.ObtenerPlatoPorNombre("MilaPapa");//del menu traemos el plato elegido por el cliente
            IConsumible bebidaSelecionada = menuSeleccionado.ObtenerBebidaPorNombre("CocaCola", 1);
            
            consumublesSelecionadosParaPedido.Add(platoSelecionado);
            consumublesSelecionadosParaPedido.Add(bebidaSelecionada);



            //-------------- DEJAMOS LISTO PARA CREAR EL PEDIDO  -----------------
            string nombreMesero = "Pepe";
            string ApellidoMesero = "Medusa";
            IEmpleado mesero = EmpleadoServiceFactory.CrearEmpleado(ERol.Mesero, nombreMesero, ApellidoMesero, "4521", "Av los saltamontes 54", 15000M);

            mesero.Id = 100; // si usamos el empleado service debemos setear nosotros l y . (con el gestor de empleado al trabajar con la db , la db se encarga de generarnos la ID)

            _mesero = (IMesero)mesero;
        }






        [TestMethod]
        public void TesteandoLaCreacionDeUnpedidoParaLocal_SeSelecionaUnPlatoYUnaBebida_SiNoLanzaExceptionEstaBien()
        {
            int cantidadDeMesas = 4;
            GestorDeMesas gestorMesas = new GestorDeMesas(_encargado, cantidadDeMesas);
           
            gestorMesas.RegistrarMesero(_mesero); //Registro el Mesero en el gestor mesas

            gestorMesas.AsignarMesaAMesero("Pepe", "Medusa", 1);


            //Id de la mesa que realiza el pedido
            int idDeLaMesCliente = 1;
            GestorDePedidos gestorDePedidos = new GestorDePedidos(_gestorProductos);


            bool seCreoPedido = gestorDePedidos.CrearPedido((ICreadorDePedidos)_mesero, ETipoDePedido.Para_Local, _consumublesSelecionadosParaPedido, idDeLaMesCliente);

            Assert.IsTrue(seCreoPedido);

        }


        [TestMethod]
        public async Task TesteamosLaCreacionDeUnPedidoParaElDelivery()
        {
            //Gestor delivery
            GestorDeDelivery gestorDeDelivery = new GestorDeDelivery(_encargado);

            //Cliente
            int idHaecodeadaDelCLiente1 = 200;
            string nombreDelCliente1 = "PepeLaMangosta";
            string direccionCliente1 = "Av Los pekes 60";
            string telefenoCliente1 = "1522446680";

            ICliente cliente1 = new Cliente(idHaecodeadaDelCLiente1, nombreDelCliente1, direccionCliente1, telefenoCliente1);

            //registramos Cliente para tener disponible sus datos
            gestorDeDelivery.RegistrarCliente(cliente1);

            //Instanciamos un delivery
            string nombreEmpleado = "pedro";
            string apellidoEmpleadoDelivery = "pika";
            string contactoEmpleadoDelivery = "4552166";
            string direccionEmpleadoDelivery = "Calle 5";
            decimal salarioEmpleadoDelivery = 20000;
            IEmpleado empleadoDelivery = EmpleadoServiceFactory.CrearEmpleado(
                ERol.Delivery, nombreEmpleado, apellidoEmpleadoDelivery, contactoEmpleadoDelivery, direccionEmpleadoDelivery, salarioEmpleadoDelivery);
            int idDelEmpleadoDelivery = 50;

            empleadoDelivery.Id = idDelEmpleadoDelivery;

            //Creamos el Pedido para delivery
            GestorDePedidos gestorDePedidos = new GestorDePedidos(_gestorProductos);

            //Tenemos que tener una lista de consumibles pedidos.
            List<IConsumible> consumublesSelecionadosParaPedido = new List<IConsumible>(); // listo para los consumibles del pedido

            IMenu menuSeleccionado = _gestorMenu.ObtenerMenuPorNombre("General");//selecionamos un menu
            //del menu traemos el plato o bebida elegido por el cliente
            IConsumible bebidaSelecionada = menuSeleccionado.ObtenerBebidaPorNombre("CocaCola", 1);

            consumublesSelecionadosParaPedido.Add(bebidaSelecionada);



            gestorDePedidos.CrearPedido((ICreadorDePedidos)_encargado, ETipoDePedido.Para_Delivery, consumublesSelecionadosParaPedido, idHaecodeadaDelCLiente1);

            IPedido pedidoParaDelivery = gestorDePedidos.TomarPedidoSinPrepararAunParaDelivery();

            bool sePreparo = await gestorDePedidos.PrepararPedido((IPreparadorDePedidos)_cocinero, pedidoParaDelivery);

            Assert.IsTrue(sePreparo);

        }




        [TestMethod]
        public async Task TesteaLaEntregaDelPedidoParaLocal_AlEntregarElPedidoSeDescuentaDelStockLosProductos_SiMarcaEntregadoDebeDarTrue()
        {

            //Arrange
            //DATOS PARA Ingrediente 1-----------------------------------
            ETipoDeProducto tipoDeProducto1 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto1 = "Pollo";
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
            string nombreDeProducto2 = "Papa";
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

            // -------------- CREAMOS LOS INGREDIENTES para el stock ------------------
            IProducto pollo = gestorDeProductos.CrearProducto(tipoDeProducto1, nombreDeProducto1, cantidad, unidadDeMedida, precio, mockProveedor1.Object);
            IProducto papa = gestorDeProductos.CrearProducto(tipoDeProducto2, nombreDeProducto2, cantidad2, unidadDeMedida2, precio2, mockProveedor2.Object);

            //AGREGAMOS AL STOCK
            gestorDeProductos.AgregarProductoAStock(pollo);
            gestorDeProductos.AgregarProductoAStock(papa);

            //Instanciamos EL COCINERO
            IEmpleado cocinero = EmpleadoServiceFactory.CrearEmpleado(ERol.Cocinero, "Pipo", "Sdd", "2323", "Av pepe 123", 15000);


            // EL ENCARGADO
            IEmpleado encargado = EmpleadoServiceFactory.CrearEmpleado(ERol.Encargado, "Frey", "Varga", "421544", "Av. los copos 66", 45000M);

            //INSTANCIAMOS EL GESTOR MENU
            GestorDeMenu gestormenu = new GestorDeMenu((ICocinero)cocinero, gestorDeProductos);


            //SELECCIONAMOS INGREDIENTES PARA EL PLATO (DEBE ESTAR CREADO EN SISTEMA ( -- STOCK --)) -- para el PLATO deben ser 
            gestormenu.SelecionarIngredienteParaUnPlato("Pollo", 1, EUnidadDeMedida.Kilo);
            gestormenu.SelecionarIngredienteParaUnPlato("Papa", 1, EUnidadDeMedida.Kilo);

            // ----------------- Creamos el Menu -------------------
            gestormenu.CrearMenu("Almuerzo");

            // -------------- Creamos el plato ---------------------
            string nombrePlato = "polloPapa";
            int tiempoPreparacion = 10;
            EUnidadDeTiempo unidadTiempo = EUnidadDeTiempo.Segundos;
            decimal precioDeVentaDelPlato = 3000;
            IConsumible plato = gestormenu.CrearPlato(nombrePlato, tiempoPreparacion, unidadTiempo);

            //Agregamos el plato al menu
            gestormenu.AgregarPlatoAMenu("Almuerzo", plato);

            //Ponemos precio al plato IMPORTANTE
            gestormenu.EstablecerPrecioAProducto((IEstablecedorDePrecios)encargado, nombrePlato, precioDeVentaDelPlato);



            // >>>>>>>>>>>>>>>>>> ---- Traemos los menus disponibles Mostramos el menu ---- <<<<<<<<<<<<<<<<<<<<<<<<<<
            List<IMenu> menusDisponibles = gestormenu.ObtenerTodosLosMenus();
            IMenu menuSeleccionado = gestormenu.ObtenerMenuPorNombre("Almuerzo");//selecionamos un menu

            //----------- SELECCION DE CONSUMIBLES ( son los que elijen los comensales o clientes) con esto Armaremos el Pedido( solo sera selecionable la bebidas que hayan en stock y los platos que se puedan cocinar) --------------------


            List<IConsumible> consumublesSelecionadosParaPedido = new List<IConsumible>(); // listo para los consumibles del pedido

            IConsumible plato1 = menuSeleccionado.ObtenerPlatoPorNombre("polloPapa");//del menu traemos el plato elegido por el cliente


            consumublesSelecionadosParaPedido.Add(plato1);
            //---------------------- CREAMOS EL PEDIDO ------------------------

            GestorDePedidos gestorDePedidos = new GestorDePedidos(gestorDeProductos);

            //ICreador de Pedidos MESERO O ENCARGADO
            //EN CASO DEL MESERO DEBE ESTAR ASIGNADO A LA MESA:


            GestorDeMesas gestorMesas = new GestorDeMesas((IEncargado)encargado, 4);


            IEmpleado mesero = EmpleadoServiceFactory.CrearEmpleado(ERol.Mesero, "Leo", "Gry", "1152000", "Av iglu 45", 15000M);
            //Al no usar GestorEmpleado , tenemos que nosotros setear la ID del mesero (El gestorEmpleado al rcearlo en la db esta se encarga de generarnos la ID del empleado MESERO en este caso)
            mesero.Id = 100;

            gestorMesas.RegistrarMesero((IMesero)mesero); //Registro el Mesero en el gestor mesas

            gestorMesas.AsignarMesaAMesero("Leo", "Gry", 1);


            //Id de la mesa que realiza el pedido
            int idDeLaMesaCliente = 1;

            bool seCreoPedido = gestorDePedidos.CrearPedido((ICreadorDePedidos)mesero, ETipoDePedido.Para_Local, consumublesSelecionadosParaPedido, idDeLaMesaCliente);

            //------------------------------------------------------ cuando SE CREa EL PEDIDO ya lo tenemos disponible
            //DEBEMOS TOMAR ESE PEDIDO O COMANDA :por eemplo el cocinero que va a preparar los platos del pedido

            IPedido pedido = gestorDePedidos.TomarPedidoSinPrepararAunParaElLocal();


            //Preparar pedido el cocinero recibe el pedido, los PLATOS TARDAN EN COCINARSE y cuando esten los platos cocinados (el pedido pasara a estar disponible ), LAS BEBIDAS SE TOMAN COMO ENTREGABLES SI ESTAN disponibles EN STOCK
            bool estaListoElPedido = await gestorDePedidos.PrepararPedido((IPreparadorDePedidos)cocinero, pedido);

            //CUANDO TERMINA EL TIEMPO(de todos los platos)----->avisa por evento que el pedido esta LISTO PARA ENTREGAR
            if (estaListoElPedido == true)
            {
                //Assert.IsTrue(estaListoElPedido);


                IPedido pedidoParaEntregar = gestorDePedidos.ObtenerPedidoListoParaLaEntregaEnLocal();
                int idDelPedido = pedido.Id;
                IMesa mesaDelPedido = gestorMesas.ObtenerMesa(1);
                //Assert.IsNotNull(mesaDelPedido);
                int idDeLaMesa = mesaDelPedido.Id;
                //Assert.AreEqual(1 , idDeLaMesa);
                IPedido pedidoParaEntrega = gestorDePedidos.ObtenerPedidoListoParaLaEntregaEnLocal();
                //Assert.IsNotNull(pedidoParaEntrega);
                int idDelPedidoParaLaMesa = pedidoParaEntrega.Id;
                //Assert.AreEqual(2, idDelPedido);
                bool seEntregoYSeDescontoDelStock = gestorDePedidos.EntregarPedido((IEntregadorPedidos)mesero, idDelPedidoParaLaMesa, idDeLaMesa); //ENTREGAMOS EL PEDIDO A LA MESA 1


                Assert.IsTrue(seEntregoYSeDescontoDelStock);



            }
        }

        [TestMethod]
        public async Task TesteaElCobroDelPedidoEnMesa()
        {

            //Arrange
            //DATOS PARA Ingrediente 1-----------------------------------
            ETipoDeProducto tipoDeProducto1 = ETipoDeProducto.Ingrediente;
            string nombreDeProducto1 = "Pollo";
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
            string nombreDeProducto2 = "Papa";
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

            // -------------- CREAMOS LOS INGREDIENTES para el stock ------------------
            IProducto pollo = gestorDeProductos.CrearProducto(tipoDeProducto1, nombreDeProducto1, cantidad, unidadDeMedida, precio, mockProveedor1.Object);
            IProducto papa = gestorDeProductos.CrearProducto(tipoDeProducto2, nombreDeProducto2, cantidad2, unidadDeMedida2, precio2, mockProveedor2.Object);

            //AGREGAMOS AL STOCK
            gestorDeProductos.AgregarProductoAStock(pollo);
            gestorDeProductos.AgregarProductoAStock(papa);

            //Instanciamos EL COCINERO
            IEmpleado cocinero = EmpleadoServiceFactory.CrearEmpleado(ERol.Cocinero, "Pipo", "Sdd", "2323", "Av pepe 123", 15000);


            // EL ENCARGADO
            IEmpleado encargado = EmpleadoServiceFactory.CrearEmpleado(ERol.Encargado, "Frey", "Varga", "421544", "Av. los copos 66", 45000M);

            //INSTANCIAMOS EL GESTOR MENU
            GestorDeMenu gestormenu = new GestorDeMenu((ICocinero)cocinero, gestorDeProductos);


            //SELECCIONAMOS INGREDIENTES PARA EL PLATO (DEBE ESTAR CREADO EN SISTEMA ( -- STOCK --)) -- para el PLATO deben ser 
            gestormenu.SelecionarIngredienteParaUnPlato("Pollo", 1, EUnidadDeMedida.Kilo);
            gestormenu.SelecionarIngredienteParaUnPlato("Papa", 1, EUnidadDeMedida.Kilo);

            // ----------------- Creamos el Menu -------------------
            gestormenu.CrearMenu("Almuerzo");

            // -------------- Creamos el plato ---------------------
            string nombrePlato = "polloPapa";
            int tiempoPreparacion = 10;
            EUnidadDeTiempo unidadTiempo = EUnidadDeTiempo.Segundos;


            decimal precioDeVentaDelPlato = 3000;
            IConsumible plato = gestormenu.CrearPlato(nombrePlato, tiempoPreparacion, unidadTiempo);

            //Agregamos el plato al menu
            gestormenu.AgregarPlatoAMenu("Almuerzo", plato);

            //Ponemos precio al plato <<<<<<<<<<<<<<<<<<<<<<<<<<<< IMPORTANTE >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            gestormenu.EstablecerPrecioAProducto((IEstablecedorDePrecios)encargado, nombrePlato, precioDeVentaDelPlato);



            // >>>>>>>>>>>>>>>>>> ---- Traemos los menus disponibles Mostramos el menu ---- <<<<<<<<<<<<<<<<<<<<<<<<<<
            List<IMenu> menusDisponibles = gestormenu.ObtenerTodosLosMenus();
            IMenu menuSeleccionado = gestormenu.ObtenerMenuPorNombre("Almuerzo");//selecionamos un menu

            //----------- SELECCION DE CONSUMIBLES ( son los que elijen los comensales o clientes) con esto Armaremos el Pedido( solo sera selecionable la bebidas que hayan en stock y los platos que se puedan cocinar) --------------------


            List<IConsumible> consumublesSelecionadosParaPedido = new List<IConsumible>(); // listo para los consumibles del pedido

            IConsumible plato1 = menuSeleccionado.ObtenerPlatoPorNombre("polloPapa");//del menu traemos el plato elegido por el cliente


            consumublesSelecionadosParaPedido.Add(plato1);
            //---------------------- CREAMOS EL PEDIDO ------------------------

            GestorDePedidos gestorDePedidos = new GestorDePedidos(gestorDeProductos);

            //ICreador de Pedidos MESERO O ENCARGADO
            //EN CASO DEL MESERO DEBE ESTAR ASIGNADO A LA MESA:


            GestorDeMesas gestorMesas = new GestorDeMesas((IEncargado)encargado, 4);



            IEmpleado mesero = EmpleadoServiceFactory.CrearEmpleado(ERol.Mesero, "Leo", "Gry", "1152000", "Av iglu 45", 15000M);
            //Al no usar GestorEmpleado , tenemos que nosotros setear la ID del mesero (El gestorEmpleado al rcearlo en la db esta se encarga de generarnos la ID del empleado MESERO en este caso)
            mesero.Id = 100;
            int idDelMesero = mesero.Id;

            gestorMesas.RegistrarMesero((IMesero)mesero); //Registro el Mesero en el gestor mesas

            gestorMesas.AsignarMesaAMesero("Leo", "Gry", 1);


            //Id de la mesa que realiza el pedido
            int idDeLaMesaCliente = 1;

            bool seCreoPedido = gestorDePedidos.CrearPedido((ICreadorDePedidos)mesero, ETipoDePedido.Para_Local, consumublesSelecionadosParaPedido, idDeLaMesaCliente);

            //------------------------------------------------------ cuando SE CREa EL PEDIDO ya lo tenemos disponible
            //DEBEMOS TOMAR ESE PEDIDO O COMANDA :por eemplo el cocinero que va a preparar los platos del pedido

            IPedido pedido = gestorDePedidos.TomarPedidoSinPrepararAunParaElLocal();


            //Preparar pedido el cocinero recibe el pedido, los PLATOS TARDAN EN COCINARSE y cuando esten los platos cocinados (el pedido pasara a estar disponible ), LAS BEBIDAS SE TOMAN COMO ENTREGABLES SI ESTAN disponibles EN STOCK
            bool estaListoElPedido = await gestorDePedidos.PrepararPedido((IPreparadorDePedidos)cocinero, pedido);

            //CUANDO TERMINA EL TIEMPO(de todos los platos)----->avisa por evento que el pedido esta LISTO PARA ENTREGAR
            if (estaListoElPedido == true)
            {
                //Assert.IsTrue(estaListoElPedido);


                IPedido pedidoParaEntregar = gestorDePedidos.ObtenerPedidoListoParaLaEntregaEnLocal();
                int idDelPedido = pedido.Id;
                IMesa mesaDelPedido = gestorMesas.ObtenerMesa(1);
                //Assert.IsNotNull(mesaDelPedido);
                int idDeLaMesa = mesaDelPedido.Id;
                //Assert.AreEqual(1 , idDeLaMesa);
                IPedido pedidoParaEntrega = gestorDePedidos.ObtenerPedidoListoParaLaEntregaEnLocal();
                //Assert.IsNotNull(pedidoParaEntrega);
                int idDelPedidoParaLaMesa = pedidoParaEntrega.Id;
                //Assert.AreEqual(2, idDelPedido);
                bool seEntregoYSeDescontoDelStock = gestorDePedidos.EntregarPedido((IEntregadorPedidos)mesero, idDelPedidoParaLaMesa, idDeLaMesa); //ENTREGAMOS EL PEDIDO A LA MESA 1


                //Assert.IsTrue(seEntregoYSeDescontoDelStock);

                


                //PROCEDEMOS A COBRAR
                bool seCobro = gestorMesas.Cobrar(idDeLaMesa, idDelMesero);

                Assert.IsTrue(seCobro);

                IMesero mes = (IMesero)mesero;
                decimal PlataEnElMesero = mes.MontoAcumulado;

                Assert.AreEqual(3000, PlataEnElMesero);
            }
        }

    }
}
