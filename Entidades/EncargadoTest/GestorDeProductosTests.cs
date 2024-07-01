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

        [TestInitialize]
        public void Setup()
        {
            _gestorDeProductos = new GestorDeProductos();

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
    }
}
