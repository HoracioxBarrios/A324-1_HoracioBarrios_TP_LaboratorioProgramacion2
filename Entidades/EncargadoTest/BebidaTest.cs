using Entidades;
using Entidades.Enumerables;
using Entidades.Interfaces;
using Entidades.Services;
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
    public class BebidaTest
    {
        [TestMethod]
        public void TestBebidas_VerificamosSiSeCreanYSeAñadenALStock_AlAgregarseAlStock_SiSeAgreganseDebeContarLosDosYSiEsAsiEsTaBien()
        {
            //Arrange
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
           
            //IPRODUCTO BEBIDA 2
            ETipoDeProducto tipoDeProductoBebida2 = ETipoDeProducto.Bebida;
            string nombreBebida2 = "Cerveza QUilmes";
            double cantidadBebida2 = 10;
            EUnidadDeMedida eUnidadDeMedidaBebida2 = EUnidadDeMedida.Unidad;
            decimal precioBebida2 = 10000;
            IProveedor proveedorBebida2 = mockProveedor4.Object;
            ECategoriaConsumible categoriaConsumibleBebida2 = ECategoriaConsumible.Bebida;
            EClasificacionBebida clasificacionBebida2 = EClasificacionBebida.Con_Alcohol;


            //GESTOR DE PRODUCTOS
            GestorDeProductos gestorDeProductos = new GestorDeProductos();
          
            //Act  -- CREAMOS LOS PRODUCTOS --
            IProducto producto1 = gestorDeProductos.CrearProducto(tipoDeProductoBebida1, nombreBebida1, cantidadBebida1, eUnidadDeMedidaBebida1, precioBebida1, proveedorBebida1, categoriaConsumibleBebida1, clasificacionBebida1);
            IProducto producto2 = gestorDeProductos.CrearProducto(tipoDeProductoBebida2, nombreBebida2, cantidadBebida2, eUnidadDeMedidaBebida2, precioBebida2, proveedorBebida2, categoriaConsumibleBebida2, clasificacionBebida2);



            //AGREGAMOS AL STOCK LOS PRODUCTOS CREADOS
            gestorDeProductos.AgregarProductoAStock(producto1);
            gestorDeProductos.AgregarProductoAStock(producto1);


            //Assert -- VERIFICAMOS QUE SE CREARON TRAYENDOLOS DE LA LISTA DE STOCK DE PRODCUTOS --
            int contador = 0;
            foreach (var producto in gestorDeProductos.ReadAllProductos())
            {                
                if (producto != null)
                {
                    if (producto.Nombre == "CocaCola")
                    {
                        Assert.AreEqual(20, producto.Cantidad);
                        contador++;
                    }
                    if (producto.Nombre == "Cerveza QUilmes")
                    {
                        Assert.AreEqual(10, producto.Cantidad);
                        contador++;
                    }
                    
                }
            }
            Assert.AreEqual(2, contador);
            
        }

        [TestMethod]
        public void TestBebidas_VerificamosSiSeCreanYSeAñadenALStock_SeDebePoderSumarElValorDeLosProductosQueEstanEnStock_SiLoHaceDebeDar30000()
        {
            //Arrange
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

            // BBEBIDA 2
            ETipoDeProducto tipoDeProductoBebida2 = ETipoDeProducto.Bebida;
            string nombreBebida2 = "Cerveza QUilmes";
            double cantidadBebida2 = 10;
            EUnidadDeMedida eUnidadDeMedidaBebida2 = EUnidadDeMedida.Unidad;
            decimal precioBebida2 = 10000;
            IProveedor proveedorBebida2 = mockProveedor4.Object;
            ECategoriaConsumible categoriaConsumibleBebida2 = ECategoriaConsumible.Bebida;
            EClasificacionBebida clasificacionBebida2 = EClasificacionBebida.Con_Alcohol;


            //GESTOR
            GestorDeProductos gestorDeProductos = new GestorDeProductos();

            //Act
            IProducto coca = gestorDeProductos.CrearProducto(tipoDeProductoBebida1, nombreBebida1, cantidadBebida1, eUnidadDeMedidaBebida1, precioBebida1, proveedorBebida1, categoriaConsumibleBebida1, clasificacionBebida1);
            IProducto cerveza = gestorDeProductos.CrearProducto(tipoDeProductoBebida2, nombreBebida2, cantidadBebida2, eUnidadDeMedidaBebida2, precioBebida2, proveedorBebida2, categoriaConsumibleBebida2, clasificacionBebida2);


            gestorDeProductos.AgregarProductoAStock(coca);
            gestorDeProductos.AgregarProductoAStock(cerveza);
            //Assert


            Assert.AreEqual(30000, gestorDeProductos.CalcularPrecio());

        }

        [TestMethod]
        public void LaBebidaQueTieneLaUnidadDeMedidaUnidadDebeDescontarUnaUnidad()
        {
            //Arrange
            //PROVEEDOR MOCK
            var mockProveedor = new Mock<IProveedor>();
            mockProveedor.Setup(p => p.Nombre).Returns("Proveedor 1");

            ETipoDeProducto tipoDeProductoBebida1 = ETipoDeProducto.Bebida;
            string nombreBebida1 = "CocaCola";
            double cantidadBebida1 = 10;
            EUnidadDeMedida eUnidadDeMedidaBebida1 = EUnidadDeMedida.Unidad;
            decimal precioBebida1 = 10000;
            IProveedor proveedorBebida1 = mockProveedor.Object;
            ECategoriaConsumible categoriaConsumibleBebida1 = ECategoriaConsumible.Bebida;
            EClasificacionBebida clasificacionBebida1 = EClasificacionBebida.Sin_Añcohol;

            GestorDeProductos gestorDeProductos = new GestorDeProductos();
            IProducto coca = gestorDeProductos.CrearProducto(tipoDeProductoBebida1, nombreBebida1, cantidadBebida1, eUnidadDeMedidaBebida1, precioBebida1, proveedorBebida1, categoriaConsumibleBebida1, clasificacionBebida1);
            //------------------- Tenemos la COCA en stock ------------------------
            gestorDeProductos.AgregarProductoAStock(coca);



            IEmpleado empleado = EmpleadoServiceFactory.CrearEmpleado(ERol.Cocinero, "Pipo", "ERG", "4215554", "Av El Ruttu 5412", 40000M);
            //Tengo que usar Gestor Emepleado 
            Cocinero cocinero = (Cocinero)empleado;

            GestorDeMenu gestorMenu = new GestorDeMenu(cocinero, gestorDeProductos);

            gestorMenu.CrearMenu("General");
            // AGREGAMOS LAS BEBIDAS DEL STOCK AL MENÚ
            gestorMenu.AgregarBebidaAlMenu("General", (IConsumible)coca);



            //ESTABLECEMOS PRECIO
            // EL ENCARGADO
            IEmpleado encargado = EmpleadoServiceFactory.CrearEmpleado(ERol.Encargado, "Frey", "Varga", "421544", "Av. los copos 66", 45000M);
            gestorMenu.EstablecerPrecioAProducto((Encargado)encargado, "CocaCola", 1100); // el precio de costo es de 1000 por lo tanto debe ser mayor el precio de venta




            //Elegimos el menu
            IMenu menuEscogido = gestorMenu.GetMenuPorNombre("General");
            //Selecionamos la bebida que esta ofrecida en el menu
            IConsumible consumibleEscogidoParaPedido = menuEscogido.GetBebidaPorNombre("CocaCola", 1);

            List<IConsumible> consumiblesDelPedidoParaDescontrDeStock = new List<IConsumible>(); // en este punto es cuando ya se entrega el pedido en el caso del delivery al cliente y se debe descontar de stock
            consumiblesDelPedidoParaDescontrDeStock.Add(consumibleEscogidoParaPedido);
            //Vamos a descontardel stock

            bool seDesconto = gestorDeProductos.DescontarProductosDeStock(consumiblesDelPedidoParaDescontrDeStock);

            IConsumible bebidaEnStockEnGestorMenu = gestorMenu.ObtenerConsumible("CocaCola");
            IProducto bebidaEnStockEnGestorProducto = gestorDeProductos.ReadProducto("CocaCola");
            Assert.IsTrue(seDesconto);

            //Si se desconto entonces si es asi debe tener 9
            Assert.AreEqual(9, bebidaEnStockEnGestorProducto.Cantidad);
        }
    }
}
