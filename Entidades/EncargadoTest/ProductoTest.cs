using Entidades.Enumerables;
using Entidades.Interfaces;
using Entidades.Services;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Negocio;

namespace Test
{
    [TestClass]
    public class ProductoTest
    {
        [TestMethod]
        public void ProductoBebidaTest_VerificamosSiSeCrea()
        {
            ETipoDeProducto tipoDeProducto = ETipoDeProducto.Bebida;
            int idParaAsignar = 1;
            string nombre = "CocaCola";
            double cantidad = 1;
            EUnidadDeMedida unidadDeMedida = EUnidadDeMedida.Unidad;
            decimal precio = 1000;

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

            ECategoriaConsumible categoria = ECategoriaConsumible.Bebida;
            EClasificacionBebida clasificacionBebida = EClasificacionBebida.Sin_Añcohol;


            IProducto producto = ProductoServiceFactory.CrearProducto(tipoDeProducto, idParaAsignar, nombre, cantidad, unidadDeMedida, precio, mockProveedor1.Object, categoria, clasificacionBebida);


            Assert.IsNotNull(producto);
        }

        [TestMethod]
        public void ProductoBebidaTest_VerificamosSiSeSeteaElPrecio()
        {
            ETipoDeProducto tipoDeProducto = ETipoDeProducto.Bebida;
            int idParaAsignar = 1;
            string nombre = "CocaCola";
            double cantidad = 1;
            EUnidadDeMedida unidadDeMedida = EUnidadDeMedida.Unidad;
            decimal precioCosto = 1000;

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

            ECategoriaConsumible categoria = ECategoriaConsumible.Bebida;
            EClasificacionBebida clasificacionBebida = EClasificacionBebida.Sin_Añcohol;


            IProducto producto = ProductoServiceFactory.CrearProducto(tipoDeProducto, idParaAsignar, nombre, cantidad, unidadDeMedida, precioCosto, mockProveedor1.Object, categoria, clasificacionBebida);

            //INPORTANTE --------------------- Establecemos el PRECIO DE VENTA -----------------------------
            producto.Precio = 1100;


            Assert.AreEqual(1100, producto.Precio);
        }

        [TestMethod]
        public void ProductoBebidaTest_CorroborandoImplementacionConGestorProductos()
        {
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


            //IPRODUCTO BEBIDA 1
            ETipoDeProducto tipoDeProductoBebida1 = ETipoDeProducto.Bebida;
            string nombreBebida1 = "CocaCola";
            double cantidadBebida1 = 1;
            EUnidadDeMedida eUnidadDeMedidaBebida1 = EUnidadDeMedida.Unidad;
            decimal precioCostoBebida1 = 1000;
            IProveedor proveedorBebida1 = mockProveedor1.Object;
            ECategoriaConsumible categoriaConsumibleBebida1 = ECategoriaConsumible.Bebida;
            EClasificacionBebida clasificacionBebida1 = EClasificacionBebida.Sin_Añcohol;


            GestorDeProductos gestorProductos = new GestorDeProductos();

            IProducto coca = gestorProductos.CrearProducto(tipoDeProductoBebida1, nombreBebida1, cantidadBebida1, eUnidadDeMedidaBebida1, precioCostoBebida1, proveedorBebida1, categoriaConsumibleBebida1, clasificacionBebida1);

            coca.Precio = 1100;

            Assert.AreEqual(1100, coca.Precio);

        }

        [TestMethod]
        public void ProductoBebidaTestCorroborandoImplementacionConGestorProductos_ElQueRealmenteEstableceElPrecioFinalALosProductosDeVentaEsElGestorMenu_TesteamosQueElPrecioSeaMilCien()
        {
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


            //IPRODUCTO BEBIDA 1
            ETipoDeProducto tipoDeProductoBebida1 = ETipoDeProducto.Bebida;
            string nombreBebida1 = "CocaCola";
            double cantidadBebida1 = 1;
            EUnidadDeMedida eUnidadDeMedidaBebida1 = EUnidadDeMedida.Unidad;
            decimal precioCostoBebida1 = 1000;
            IProveedor proveedorBebida1 = mockProveedor1.Object;
            ECategoriaConsumible categoriaConsumibleBebida1 = ECategoriaConsumible.Bebida;
            EClasificacionBebida clasificacionBebida1 = EClasificacionBebida.Sin_Añcohol;


            GestorDeProductos gestorProductos = new GestorDeProductos();

            IProducto coca = gestorProductos.CrearProducto(tipoDeProductoBebida1, nombreBebida1, cantidadBebida1, eUnidadDeMedidaBebida1, precioCostoBebida1, proveedorBebida1, categoriaConsumibleBebida1, clasificacionBebida1);
            //Agregamos el producto BEBIDA al stock
            gestorProductos.AgregarProductoAStock(coca);


            List<IConsumible> bebidasDelStock = gestorProductos.OtenerTodosLosProductosBebidas(); // vemos que esta en el stock

            //debemos crear el menu

            //-- intanciamos el COCINERO --
            IEmpleado empleado = EmpleadoServiceFactory.CrearEmpleado(ERol.Cocinero, "Pipo", "ERG", "4215554", "Av El Ruttu 5412", 40000M);
            //Tengo que usar GestorEmepleado 
            Cocinero cocinero = (Cocinero)empleado;

            GestorDeMenu gestorMenu = new GestorDeMenu(cocinero, gestorProductos);

            gestorMenu.CrearMenu("General");
            // AGREGAMOS LAS BEBIDAS DEL STOCK AL MENÚ
            gestorMenu.AgregarBebidasAMenu("General", bebidasDelStock);

            //ESTABLECEMOS PRECIO


            IEmpleado empleado2 = EmpleadoServiceFactory.CrearEmpleado(ERol.Encargado, "Alex", "Canigia", "4215554", "Av El Ruttu 5412", 40000M);
            Encargado encargado = (Encargado)empleado2;
            gestorMenu.EstablecerPrecioAProducto(encargado, "CocaCola", 1100);


            //Elegimos el menu
            IMenu menuEscogido = gestorMenu.ObtenerMenuPorNombre("General");
            //Selecionamos la bebida que esta ofrecida en el menu
            IConsumible consumible = menuEscogido.ObtenerBebidaPorNombre("CocaCola", 1);

            Bebida bebidaCoca = (Bebida)consumible;

            Assert.AreEqual(1100, consumible.Precio);
        }

        [TestMethod]
        public void ProductoBebidaTestCorroborandoImplementacionConGestorProductos_ElQueRealmenteEstableceElPrecioFinalALosProductosDeVentaEsElGestorMenu_TesteamosQUeLaCantidadSeaUNo()
        {
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


            //IPRODUCTO BEBIDA 1
            ETipoDeProducto tipoDeProductoBebida1 = ETipoDeProducto.Bebida;
            string nombreBebida1 = "CocaCola";
            double cantidadBebida1 = 1;
            EUnidadDeMedida eUnidadDeMedidaBebida1 = EUnidadDeMedida.Unidad;
            decimal precioCostoBebida1 = 1000;
            IProveedor proveedorBebida1 = mockProveedor1.Object;
            ECategoriaConsumible categoriaConsumibleBebida1 = ECategoriaConsumible.Bebida;
            EClasificacionBebida clasificacionBebida1 = EClasificacionBebida.Sin_Añcohol;


            GestorDeProductos gestorProductos = new GestorDeProductos();

            IProducto coca = gestorProductos.CrearProducto(tipoDeProductoBebida1, nombreBebida1, cantidadBebida1, eUnidadDeMedidaBebida1, precioCostoBebida1, proveedorBebida1, categoriaConsumibleBebida1, clasificacionBebida1);
            //Agregamos el producto BEBIDA al stock
            gestorProductos.AgregarProductoAStock(coca);


            List<IConsumible> bebidasDelStock = gestorProductos.OtenerTodosLosProductosBebidas(); // vemos que esta en el stock

            //debemos crear el menu

            //-- intanciamos el COCINERO --
            IEmpleado empleado = EmpleadoServiceFactory.CrearEmpleado(ERol.Cocinero, "Pipo", "ERG", "4215554", "Av El Ruttu 5412", 40000M);
            //Tengo que usar GestorEmepleado 
            Cocinero cocinero = (Cocinero)empleado;

            GestorDeMenu gestorMenu = new GestorDeMenu(cocinero, gestorProductos);

            gestorMenu.CrearMenu("General");
            // AGREGAMOS LAS BEBIDAS DEL STOCK AL MENÚ
            gestorMenu.AgregarBebidasAMenu("General", bebidasDelStock);

            //ESTABLECEMOS PRECIO


            IEmpleado empleado2 = EmpleadoServiceFactory.CrearEmpleado(ERol.Encargado, "Alex", "Canigia", "4215554", "Av El Ruttu 5412", 40000M);
            Encargado encargado = (Encargado)empleado2;
            gestorMenu.EstablecerPrecioAProducto(encargado, "CocaCola", 1100);


            //Elegimos el menu
            IMenu menuEscogido = gestorMenu.ObtenerMenuPorNombre("General");
            //Selecionamos la bebida que esta ofrecida en el menu
            IConsumible consumible = menuEscogido.ObtenerBebidaPorNombre("CocaCola", 1);

            Bebida bebidaCoca = (Bebida)consumible;

            Assert.AreEqual(1, consumible.Cantidad);
        }
    }
}
