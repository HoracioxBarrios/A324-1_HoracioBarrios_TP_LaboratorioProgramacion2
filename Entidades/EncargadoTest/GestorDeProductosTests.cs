using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Interfaces;
using Entidades.Services;
using Moq;

namespace Test
{
    [TestClass]
    public class GestorDeProductosTests
    {
        private GestorDeProductos _gestorDeProductos;
        private IEncargado _encargado;
        private IGestorContable _gestorContable;

        [TestInitialize]
        public void Setup()
        {
            IArca arca = new Arca();
            arca.AgregarDinero(100000M);
            _gestorContable = new GestorContable(arca); // Ahora el Gestor productos necesita esto al momento de Agregar los productos a stock para pagarles a los proveedores
            _gestorDeProductos = new GestorDeProductos(_gestorContable);

            //---- Instanciamos un encargado
            IEmpleado encargado = EmpleadoServiceFactory.CrearEmpleado(ERol.Encargado, "Hui", "yu", "45213", "Av pollo 12", 45000M);
            _encargado = (IEncargado)encargado;
        }

        [TestMethod]
        public void ConsultaDeStockPorAgotarse_ProductosEnStock_ReturnsProductosPorAgotarse()
        {
            // Arrange
            var mockProveedor = new Mock<IProveedor>();
            mockProveedor.Setup(p => p.Nombre).Returns("Proveedor");
            mockProveedor.Setup(p => p.Cuit).Returns("30-12345678-9");

            // Crear productos
            IProducto pollo = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "pollo", 20, EUnidadDeMedida.Kilo, 20000, mockProveedor.Object);
            IProducto papa = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "papa", 9, EUnidadDeMedida.Kilo, 20000, mockProveedor.Object);

            _gestorDeProductos.AgregarProductoAStock(pollo);
            _gestorDeProductos.AgregarProductoAStock(papa);

            // Act
            var productosPorAgotarse = _gestorDeProductos.ConsultaDeStockPorAgotarse(_encargado);

            // Assert
            Assert.AreEqual(1, productosPorAgotarse.Count, "Debería haber solo un producto por agotarse.");

            // Verificar que el producto por agotarse es la papa
            Assert.IsTrue(productosPorAgotarse.Any(p => p.Nombre == "papa"), "La papa debería estar en la lista de productos por agotarse.");
        }

        [TestMethod]
        [ExpectedException(typeof(ListaVaciaException))]
        public void ConsultaDeStockPorAgotarse_SinProductosEnStock_ThrowsListaVaciaException()
        {
            // Act
            _gestorDeProductos.ConsultaDeStockPorAgotarse(_encargado);
        }

        public void ConsultaDeStockVigenteTest()
        {
            // Arrange
            var mockProveedor = new Mock<IProveedor>();
            mockProveedor.Setup(p => p.Nombre).Returns("Proveedor");
            mockProveedor.Setup(p => p.Cuit).Returns("30-12345678-9");

            // Crear productos
            IProducto pollo = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "pollo", 20, EUnidadDeMedida.Kilo, 20000, mockProveedor.Object);
            IProducto papa = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "papa", 9, EUnidadDeMedida.Kilo, 20000, mockProveedor.Object);

            _gestorDeProductos.AgregarProductoAStock(pollo);
            _gestorDeProductos.AgregarProductoAStock(papa);

            var productosEnStock = _gestorDeProductos.ConsultaStockVigente(_encargado);

            // Assert
            Assert.AreEqual(2, productosEnStock.Count, "Debería haber 2 productos en stock.");
        }

        public void BloqueoDeStockTest()
        {
            // Arrange
            var mockProveedor = new Mock<IProveedor>();
            mockProveedor.Setup(p => p.Nombre).Returns("Proveedor");
            mockProveedor.Setup(p => p.Cuit).Returns("30-12345678-9");


            // Crear productos
            IProducto pollo = _gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "pollo", 20, EUnidadDeMedida.Kilo, 20000, mockProveedor.Object);

            //Emulamos  que se agota el pollo
            pollo.Cantidad = 0;

            //Act
            _gestorDeProductos.BloquearParaLaVenta(_encargado, pollo);

            //Assert
            Assert.AreEqual(false, pollo.Disponibilidad);

        }



        [TestMethod]
        public void AgregarProductoAStock_PagoAlProveedorCorrecto()
        {
            // Arrange
            var mockArca = new Mock<IArca>();
            var gestorContable = new GestorContable(mockArca.Object);
            var gestorDeProductos = new GestorDeProductos(gestorContable);

            var mockProveedor = new Mock<IProveedor>();
            mockProveedor.Setup(p => p.Nombre).Returns("Proveedor");
            mockProveedor.Setup(p => p.Cuit).Returns("30-12345678-9");

            var mockProducto = new Mock<IProducto>();
            mockProducto.Setup(p => p.Nombre).Returns("pollo");
            mockProducto.Setup(p => p.Precio).Returns(20000M);
            mockProducto.Setup(p => p.Proveedor).Returns(mockProveedor.Object);

            // Mock para simular suficiente dinero en el arca
            mockArca.Setup(a => a.ObtenerMontoDisponible()).Returns(30000M);

            // Act
            gestorDeProductos.AgregarProductoAStock(mockProducto.Object);

            // Assert
            mockArca.Verify(a => a.TomarDinero(20000M), Times.Once);
            Assert.IsTrue(gestorDeProductos.ObtenerTodosLosProductos().Contains(mockProducto.Object), "El producto debería estar en el stock.");
        }

        [TestMethod]
        public void AgregarProductoAStock_UsoCuentaCorrienteDelProveedor()
        {
            // Arrange
            var mockArca = new Mock<IArca>();
            var gestorContable = new GestorContable(mockArca.Object);
            var gestorDeProductos = new GestorDeProductos(gestorContable);

            var mockProveedor = new Mock<IProveedor>();
            mockProveedor.Setup(p => p.Nombre).Returns("Proveedor");
            mockProveedor.Setup(p => p.Cuit).Returns("30-12345678-9");

            var mockProducto = new Mock<IProducto>();
            mockProducto.Setup(p => p.Nombre).Returns("pollo");
            mockProducto.Setup(p => p.Precio).Returns(20000M);
            mockProducto.Setup(p => p.Proveedor).Returns(mockProveedor.Object);

            // Mock para simular dinero insuficiente en el arca
            mockArca.Setup(a => a.ObtenerMontoDisponible()).Returns(10000M); // Menor al precio del producto

            // Act
            gestorDeProductos.AgregarProductoAStock(mockProducto.Object);

            // Assert
            mockProveedor.Verify(p => p.UsarCuentaCorriente(), Times.Once);
            Assert.IsTrue(gestorDeProductos.ObtenerTodosLosProductos().Contains(mockProducto.Object), "El producto debería estar en el stock.");
        }


    }
}
