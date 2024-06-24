using Entidades.Enumerables;
using Entidades.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;
using Moq;
namespace Test
{
    [TestClass]
    public class PlatoTest
    {
        private Mock<IProveedor> _mockProveedor1;
        private List<IConsumible> _ingredientes;



        [TestInitialize]
        public void Init() 
        {
            // Arrange
            //creamos el producto para el plato
            _mockProveedor1 = new Mock<IProveedor>();

            _mockProveedor1.Setup(p => p.Nombre).Returns("Proveedor 1");
            _mockProveedor1.Setup(p => p.Cuit).Returns("30-12345678-9");
            _mockProveedor1.Setup(p => p.Direccion).Returns("Calle Falsa 123");
            _mockProveedor1.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Almacen);
            _mockProveedor1.Setup(p => p.MediosDePago).Returns(EMediosDePago.Transferencia);
            _mockProveedor1.Setup(p => p.EsAcreedor).Returns(EAcreedor.Si);
            _mockProveedor1.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Lunes);
            _mockProveedor1.Setup(p => p.ID).Returns(1);
            _mockProveedor1.Setup(p => p.ToString()).Returns("ID: 1, Nombre: Proveedor 1, CUIT: 30-12345678-9, Direccion: Calle Falsa 123, Tipo de Producto que Provee: Almacen, Medio de Pago: Transferencia, Es Acreedor? : Si, Dia de Entrega: Lunes");



            GestorDeProductos gestorDeProductos = new GestorDeProductos();

            IProducto ingrediente1 = gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "papa", 1, EUnidadDeMedida.Kilo, 10000, _mockProveedor1.Object);
            IProducto ingrediente2 = gestorDeProductos.CrearProducto(ETipoDeProducto.Ingrediente, "pollo", 1, EUnidadDeMedida.Kilo, 10000, _mockProveedor1.Object);

            List<IConsumible> ingredientes = new List<IConsumible>();
            ingredientes.Add((IConsumible)ingrediente1);
            ingredientes.Add((IConsumible)ingrediente2);
            _ingredientes = ingredientes;



        }


        [TestMethod]
        public void Cocinar_PlatoDeberiaEstaBienSiNoEsNull()
        {

            Plato plato = new Plato("NombreDelPlato", _ingredientes, 30, EUnidadDeTiempo.Segundos);
            // Act
            Assert.IsNotNull(plato);
        }

        [TestMethod]
        public void Cocinar_PlatoDeberiaIniciarEnListoParaEntregarFalse_SiEsFalseEstaBien()
        {
            // Arrange
            //creamos el producto para el plato           

            // creamos un plato con tiempo de preparación de 30 segundos
            Plato plato = new Plato("NombreDelPlato", _ingredientes, 30, EUnidadDeTiempo.Segundos);

            // Act
            Assert.AreEqual(false, plato.ListoParaEntregar);
        }


        [TestMethod]
        public async Task Cocinar_PlatoDeberiaTardar30Segundos()
        {
            // creamos un plato con tiempo de preparación de 30 segundos
            Plato plato = new Plato("NombreDelPlato", _ingredientes, 30, EUnidadDeTiempo.Segundos);

            // Act
            await plato.Cocinar(); // Iniciar la preparación del plato

            // Assert
            // Verificar que el tiempo transcurrido sea aproximadamente 30 segundos
            Assert.AreEqual(30, plato.TiempoTranscurrido.TotalSeconds, 1); // Aceptamos un margen de error de 1 segundo

        }


        [TestMethod]
        public async Task Cocinar_PlatoDeberiaTardar30SegundosYCambiarSuEstadoAEntregableTrue()
        {
            // creamos un plato con tiempo de preparación de 30 segundos
            Plato plato = new Plato("NombreDelPlato", _ingredientes, 30, EUnidadDeTiempo.Segundos);

            // Act
            await plato.Cocinar(); // Iniciar la preparación del plato

            // Assert

            Assert.AreEqual(30, plato.TiempoTranscurrido.TotalSeconds, 1); // Aceptamos un margen de error de 1 segundo

            //Luego de transcurrido el tiempo debe de estar listo = true
            Assert.AreEqual(true, plato.ListoParaEntregar);
        }
    }
}


