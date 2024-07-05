using Entidades.Enumerables;
using Entidades.Interfaces;
using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class GestorContableTest
    {
        [TestMethod]
        public void CerrarTurno_AgregaDineroArca_SeReflejaElDineroQueSeSumaEnLasArcas()
        {
            // Arrange
            IArca arca = new Arca();
            IGestorContable gestorContable = new GestorContable(arca);
            IGestorVentas gestorVentas = new GestorVentas(gestorContable);

            ICobro pagoMesero = new Cobro(1, 100, ERol.Mesero, 1200, ETipoDePago.Contado);
            ICobro pagoDelivery = new Cobro(2, 101, ERol.Delivery, 500, ETipoDePago.TarjetaDeCredito);

            gestorVentas.RegistrarCobro(pagoMesero);
            gestorVentas.RegistrarCobro(pagoDelivery);

            // Act
            gestorVentas.CerrarTurno();

            // Assert
            var montoEnArca = arca.ObtenerMontoDisponible();
            Assert.AreEqual(1700, montoEnArca);
        }
    }
}
