using Entidades.Enumerables;
using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class GestorDeVentasTest
    {
        [TestMethod]
        public void ObtenerMontoDeLosPagosDeLosConsumosTotales_Test()
        {
            // Arrange
            GestorVentas gestorVentas = new GestorVentas();

            Pago pago1 = new Pago(1, 1, ERol.Mesero, 100.50m, ETipoDePago.TarjetaDeCredito);
            Pago pago2 = new Pago(2, 2, ERol.Mesero, 200.75m, ETipoDePago.Contado);
            Pago pago3 = new Pago(3, 3, ERol.Delivery, 300.00m, ETipoDePago.BilleteraVirtual);

            gestorVentas.Pagos.Add(pago1);
            gestorVentas.Pagos.Add(pago2);
            gestorVentas.Pagos.Add(pago3);

            decimal TotalEsperado = 100.50m + 200.75m + 300.00m;

            // Act
            decimal actualTotal = gestorVentas.ObtenerMontoDeLosPagosDeLosConsumosTotales();

            // Assert
            Assert.AreEqual(TotalEsperado, actualTotal, "El total de los montos de los pagos no es el esperado.");
        }

        [TestMethod]
        public void ObtenerMontoDeLosPagosDeLosConsumosTotales_SinPagos_Test()
        {
            // Arrange
            GestorVentas gestorVentas = new GestorVentas();

            decimal TotalEsperado = 0m;

            // Act
            decimal actualTotal = gestorVentas.ObtenerMontoDeLosPagosDeLosConsumosTotales();

            // Assert
            Assert.AreEqual(TotalEsperado, actualTotal, "El total de los montos de los pagos debería ser 0 cuando no hay pagos.");
        }
    }
}
