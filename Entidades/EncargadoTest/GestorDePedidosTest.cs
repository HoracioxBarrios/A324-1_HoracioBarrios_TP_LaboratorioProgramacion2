using Entidades;
using Entidades.Enumerables;
using Entidades.Interfaces;
using Moq;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class GestorDePedidosTest
    {
        [TestMethod]
        public void testeandoLacreacionDeUnpedidoParaLocal()
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


            //----------- CREAMOS EL MENU -------------------
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

            //verificamos si es el plato que creamos recien el que esta en la lista de menus.
            var menuGeneral = gestorMenu.GetListaDeMenusQueSeOfrecen().FirstOrDefault(menu => menu.Nombre.Equals(nombreDeMenuCreadoPreviamente, StringComparison.OrdinalIgnoreCase));
            Assert.IsNotNull(menuGeneral);
            Assert.IsTrue(menuGeneral.GetPlatosEnMenu().Any(plato => plato.Nombre.Equals(nombreDelPlatoACrear, StringComparison.OrdinalIgnoreCase)));

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

            gestorDeProductos.CrearProductoParaListaDeStock(tipoDeProductoBebida1, nombreBebida1, cantidadBebida1, eUnidadDeMedidaBebida1, precioBebida1, proveedorBebida1, categoriaConsumibleBebida1, clasificacionBebida1);
            gestorDeProductos.CrearProductoParaListaDeStock(tipoDeProductoBebida2, nombreBebida2, cantidadBebida2, eUnidadDeMedidaBebida2, precioBebida2, proveedorBebida2, categoriaConsumibleBebida2, clasificacionBebida2);

            List< IConsumible> bebidasDelStock = gestorDeProductos.GetAllProductosBebidas();

            // AGREGAMOS LAS BEBIDAS DEL STOCK AL MENÚ
            gestorMenu.AgregarBebidasAMenu(nombreDeMenuCreadoPreviamente, bebidasDelStock);

            //Teniendo el Menú listo se puede mostrar
            //----- MOSTRAMOS EL MENU Y SELECIONAMOS LOS CONSUMIBLES PARA ARMAR EL PEDIDO -----
            gestorMenu.GetListaDeMenusQueSeOfrecen();

            //-------------- VAMOS A CREAR UN PEDIDO -----------------

            IEmpleado empleadoMesero = new Mesero(ERol.Mesero, "Pepe", "Medusa", "4521", "Av los saltamontes 54", 15000M);


            GestorDePedidos gestorDePedidos = new GestorDePedidos(gestorMenu);

            // tener el crear dentro del gestor pedidos que espere el ICreador de Pedido
        }


    }
}
