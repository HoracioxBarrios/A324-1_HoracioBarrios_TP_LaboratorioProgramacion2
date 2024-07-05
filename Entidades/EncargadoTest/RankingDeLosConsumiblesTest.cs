using Entidades;
using Entidades.Enumerables;
using Entidades.Interfaces;
using Entidades.Services;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
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
    public class RankingDeLosConsumiblesTest
    {
        private GestorDePedidos _gestorDePedidos;
        private IGestorProductos _gestorProductos;
        private List<IConsumible> _consumiblesPlatosEjemplo;
        private Mesero _empleadoCreadorPedidos;

        private IGestorContable _gestorContable;







        [TestInitialize]
        public void Setup()
        {
            // Mock de Proveedor para ingredientes
            var mockProveedor = new Mock<IProveedor>();
            mockProveedor.Setup(p => p.Nombre).Returns("Proveedor Ejemplo");
            mockProveedor.Setup(p => p.Cuit).Returns("30-12345678-9");
            mockProveedor.Setup(p => p.Direccion).Returns("Calle Falsa 123");
            mockProveedor.Setup(p => p.TipoDeProducto).Returns(ETipoDeProducto.Ingrediente);
            mockProveedor.Setup(p => p.MediosDePago).Returns(EMediosDePago.Transferencia);
            mockProveedor.Setup(p => p.EsAcreedor).Returns(EAcreedor.Si);
            mockProveedor.Setup(p => p.DiaDeEntrega).Returns(EDiaDeLaSemana.Lunes);
            mockProveedor.Setup(p => p.ID).Returns(1);
            mockProveedor.Setup(p => p.ToString()).Returns("ID: 1, Nombre: Proveedor Ejemplo, CUIT: 30-12345678-9, Direccion: Calle Falsa 123, Tipo de Producto que Provee: Ingrediente, Medio de Pago: Transferencia, Es Acreedor? : Si, Dia de Entrega: Lunes");

            // Gestor Contable y Productos
            IArca arca = new Arca();
            arca.AgregarDinero(100000m);
            _gestorContable = new GestorContable(arca);
            _gestorProductos = new GestorDeProductos(_gestorContable);

            // Gestor de Pedidos
            _gestorDePedidos = new GestorDePedidos(_gestorProductos);

            // Ingredientes de ejemplo para platos
            IConsumible plato1 = new Plato("Plato1", new List<IConsumible>(), 1, EUnidadDeTiempo.Minutos);
            IConsumible plato2 = new Plato("Plato1", new List<IConsumible>(), 1, EUnidadDeTiempo.Minutos);
            IConsumible plato3 = new Plato("Plato1", new List<IConsumible>(), 1, EUnidadDeTiempo.Minutos);
            IConsumible plato4 = new Plato("Plato1", new List<IConsumible>(), 1, EUnidadDeTiempo.Minutos);
            IConsumible plato5 = new Plato("Plato2", new List<IConsumible>(), 1, EUnidadDeTiempo.Minutos);
            IConsumible plato6 = new Plato("Plato2", new List<IConsumible>(), 1, EUnidadDeTiempo.Minutos);
            IConsumible plato7 = new Plato("Plato2", new List<IConsumible>(), 1, EUnidadDeTiempo.Minutos);
            IConsumible plato8 = new Plato("Plato4", new List<IConsumible>(), 1, EUnidadDeTiempo.Minutos);
            IConsumible plato9 = new Plato("Plato4", new List<IConsumible>(), 1, EUnidadDeTiempo.Minutos);
            IConsumible plato10 = new Plato("Plato3", new List<IConsumible>(), 1, EUnidadDeTiempo.Minutos);

            // Agregamos platos a la lista de consumibles ejemplo
            _consumiblesPlatosEjemplo = new List<IConsumible> { plato1, plato2, plato3, plato4, plato5, plato6, plato7, plato8, plato9, plato10 };

            // Empleado creador de pedidos
            IEmpleado empleado = EmpleadoServiceFactory.CrearEmpleado(ERol.Mesero, "Nombre", "Apellido", "12345678", "Dirección", 15000m);
            _empleadoCreadorPedidos = (Mesero)empleado;
        }

        [TestMethod]
        public void ObtenerRankingDeConsumiblesMasPedido_DeberiaRetornarListaOrdenadaDescendente()
        {
            // Arrange: Crear pedidos de ejemplo
            _gestorDePedidos.CrearPedido((ICreadorDePedidos)_empleadoCreadorPedidos, ETipoDePedido.Para_Local, _consumiblesPlatosEjemplo, 3);
            _gestorDePedidos.CrearPedido((ICreadorDePedidos)_empleadoCreadorPedidos, ETipoDePedido.Para_Delivery, _consumiblesPlatosEjemplo, 5);

            // Act
            List<(IConsumible, int)> rankingMasPedidos = _gestorDePedidos.ObtenerRankingDeConsumiblesMasPedido();

            // Assert
            Assert.AreEqual(4, rankingMasPedidos.Count);
            Assert.AreEqual("Plato1", rankingMasPedidos[0].Item1.Nombre);
            Assert.AreEqual("Plato2", rankingMasPedidos[1].Item1.Nombre);
            Assert.AreEqual("Plato4", rankingMasPedidos[2].Item1.Nombre);
        }

        [TestMethod]
        public void ObtenerRankingDeConsumiblesMenosPedido_DeberiaRetornarListaOrdenadaAscendente()
        {
            // Arrange: Crear pedidos de ejemplo
            _gestorDePedidos.CrearPedido((ICreadorDePedidos)_empleadoCreadorPedidos, ETipoDePedido.Para_Local, _consumiblesPlatosEjemplo, 1);
            _gestorDePedidos.CrearPedido((ICreadorDePedidos)_empleadoCreadorPedidos, ETipoDePedido.Para_Delivery, _consumiblesPlatosEjemplo, 2);

            // Act
            List<(IConsumible, int)> rankingMenosPedidos = _gestorDePedidos.ObtenerRankingDeConsumiblesMenosPedido();

            // Assert
            Assert.AreEqual(4, rankingMenosPedidos.Count);
            Assert.AreEqual("Plato3", rankingMenosPedidos[0].Item1.Nombre);
            Assert.AreEqual("Plato4", rankingMenosPedidos[1].Item1.Nombre);
            Assert.AreEqual("Plato2", rankingMenosPedidos[2].Item1.Nombre);
        }

        [TestMethod]
        public void ObtenerTopNConsumiblesMasPedidos_DeberiaRetornarTopN()
        {
            // Arrange: Crear pedidos de ejemplo
            _gestorDePedidos.CrearPedido((ICreadorDePedidos)_empleadoCreadorPedidos, ETipoDePedido.Para_Local, _consumiblesPlatosEjemplo, 3);
            _gestorDePedidos.CrearPedido((ICreadorDePedidos)_empleadoCreadorPedidos, ETipoDePedido.Para_Delivery, _consumiblesPlatosEjemplo, 5);

            // Act
            int topN = 2;
            List<(IConsumible, int)> topNConsumibles = _gestorDePedidos.ObtenerTopNConsumiblesMasPedidos(topN);

            // Assert
            Assert.AreEqual(topN, topNConsumibles.Count);
            Assert.AreEqual("Plato1", topNConsumibles[0].Item1.Nombre);
            Assert.AreEqual("Plato2", topNConsumibles[1].Item1.Nombre);
        }

        [TestMethod]
        public void ObtenerTopNConsumiblesMenosPedidos_DeberiaRetornarTopN()
        {
            // Arrange: Crear pedidos de ejemplo
            _gestorDePedidos.CrearPedido((ICreadorDePedidos)_empleadoCreadorPedidos, ETipoDePedido.Para_Local, _consumiblesPlatosEjemplo, 1);
            _gestorDePedidos.CrearPedido((ICreadorDePedidos)_empleadoCreadorPedidos, ETipoDePedido.Para_Delivery, _consumiblesPlatosEjemplo, 2);

            // Act
            int topN = 2;
            List<(IConsumible, int)> topNConsumibles = _gestorDePedidos.ObtenerTopNConsumiblesMenosPedido(topN);

            // Assert
            Assert.AreEqual(topN, topNConsumibles.Count);
            Assert.AreEqual("Plato3", topNConsumibles[0].Item1.Nombre);
            Assert.AreEqual("Plato4", topNConsumibles[1].Item1.Nombre);
        }

    }
}
