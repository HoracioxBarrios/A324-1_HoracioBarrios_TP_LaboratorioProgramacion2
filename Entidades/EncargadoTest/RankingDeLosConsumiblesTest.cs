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
            // Arrange: Configurar datos de prueba
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
            IConsumible plato8 = new Plato("Plato3", new List<IConsumible>(), 1, EUnidadDeTiempo.Minutos);
            IConsumible plato9 = new Plato("Plato3", new List<IConsumible>(), 1, EUnidadDeTiempo.Minutos);
            IConsumible plato10 = new Plato("Plato4", new List<IConsumible>(), 1, EUnidadDeTiempo.Minutos);
            // agregamos platos a la lista de consumibles ejemplo
            _consumiblesPlatosEjemplo = new List<IConsumible> { plato1, plato2, plato3 };

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
            Assert.AreEqual(3, rankingMasPedidos.Count);
            // Verificar que el primer elemento en la lista ordenada sea el plato con más pedidos
            Assert.AreEqual(_consumiblesPlatosEjemplo[2].Nombre, rankingMasPedidos[0].Item1.Nombre);
            // Verificar el segundo plato más pedido
            Assert.AreEqual(_consumiblesPlatosEjemplo[1].Nombre, rankingMasPedidos[1].Item1.Nombre);
            // Verificar el tercer plato más pedido
            Assert.AreEqual(_consumiblesPlatosEjemplo[0].Nombre, rankingMasPedidos[2].Item1.Nombre);
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
            Assert.AreEqual(3, rankingMenosPedidos.Count);
            // Verificar que el primer elemento en la lista ordenada sea el plato con menos pedidos
            Assert.AreEqual(_consumiblesPlatosEjemplo[0].Nombre, rankingMenosPedidos[0].Item1.Nombre);
            // Verificar el segundo plato menos pedido
            Assert.AreEqual(_consumiblesPlatosEjemplo[1].Nombre, rankingMenosPedidos[1].Item1.Nombre);
            // Verificar el tercer plato menos pedido
            Assert.AreEqual(_consumiblesPlatosEjemplo[2].Nombre, rankingMenosPedidos[2].Item1.Nombre);
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
            // Verificar que el primer elemento en el top N sea el plato con más pedidos
            Assert.AreEqual(_consumiblesPlatosEjemplo[2].Nombre, topNConsumibles[0].Item1.Nombre);
            // Verificar el segundo plato más pedido en el top N
            Assert.AreEqual(_consumiblesPlatosEjemplo[1].Nombre, topNConsumibles[1].Item1.Nombre);
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
            // Verificar que el primer elemento en el top N sea el plato con menos pedidos
            Assert.AreEqual(_consumiblesPlatosEjemplo[0].Nombre, topNConsumibles[0].Item1.Nombre);
            // Verificar el segundo plato menos pedido en el top N
            Assert.AreEqual(_consumiblesPlatosEjemplo[1].Nombre, topNConsumibles[1].Item1.Nombre);
        }
    }
}
