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






        [TestMethod]
        public void ObtenerMontoDeLosPagosDeDeliverys_Test()
        {
            // Arrange
            GestorVentas gestorVentas = new GestorVentas();

            Pago pago1 = new Pago(1, 1, ERol.Delivery, 150.50m, ETipoDePago.TarjetaDeCredito);
            Pago pago2 = new Pago(2, 2, ERol.Mesero, 200.75m, ETipoDePago.Contado);
            Pago pago3 = new Pago(3, 3, ERol.Delivery, 300.00m, ETipoDePago.Contado);
            Pago pago4 = new Pago(4, 4, ERol.Mesero, 100.00m, ETipoDePago.Contado);
            Pago pago5 = new Pago(5, 5, ERol.Delivery, 250.25m, ETipoDePago.BilleteraVirtual);

            gestorVentas.Pagos.Add(pago1);
            gestorVentas.Pagos.Add(pago2);
            gestorVentas.Pagos.Add(pago3);
            gestorVentas.Pagos.Add(pago4);
            gestorVentas.Pagos.Add(pago5);

            decimal totalEsperado = 150.50m + 300.00m + 250.25m;

            // Act
            decimal totalActual = gestorVentas.ObtenerMontoDeLosPagosDeDeliverys();

            // Assert
            Assert.AreEqual(totalEsperado, totalActual, "El total de los montos de los pagos de Delivery no es el esperado.");
        }

        [TestMethod]
        public void ObtenerMontoDeLosPagosDeDeliverys_SinPagosDelivery_Test()
        {
            // Arrange
            GestorVentas gestorVentas = new GestorVentas();

            Pago pago1 = new Pago(1, 1, ERol.Mesero, 150.50m, ETipoDePago.TarjetaDeCredito);
            Pago pago2 = new Pago(2, 2, ERol.Mesero, 200.75m, ETipoDePago.Contado);

            gestorVentas.Pagos.Add(pago1);
            gestorVentas.Pagos.Add(pago2);

            decimal TotalEsperado = 0m;

            // Act
            decimal actualTotal = gestorVentas.ObtenerMontoDeLosPagosDeDeliverys();

            // Assert
            Assert.AreEqual(TotalEsperado, actualTotal, "El total de los montos de los pagos de Delivery debería ser 0 cuando no hay pagos de Delivery.");
        }







        [TestMethod]
        public void ObtenerMontoDeLosPagosDeMeseros_Test()
        {
            // Arrange
            GestorVentas gestorVentas = new GestorVentas();

            Pago pago1 = new Pago(1, 1, ERol.Mesero, 150.50m, ETipoDePago.TarjetaDeCredito);
            Pago pago2 = new Pago(2, 2, ERol.Mesero, 200.75m, ETipoDePago.Contado);
            Pago pago3 = new Pago(3, 3, ERol.Delivery, 300.00m, ETipoDePago.Contado);
            Pago pago4 = new Pago(4, 4, ERol.Mesero, 100.00m, ETipoDePago.Contado);
            Pago pago5 = new Pago(5, 5, ERol.Delivery, 250.25m, ETipoDePago.BilleteraVirtual);

            gestorVentas.Pagos.Add(pago1);
            gestorVentas.Pagos.Add(pago2);
            gestorVentas.Pagos.Add(pago3);
            gestorVentas.Pagos.Add(pago4);
            gestorVentas.Pagos.Add(pago5);

            decimal totalEsperado = 150.50m + 200.75m + 100.00m;

            // Act
            decimal totalActual = gestorVentas.ObtenerMontoDeLosPagosDeMeseros();

            // Assert
            Assert.AreEqual(totalEsperado, totalActual, "El total de los montos de los pagos de Meseros no es el esperado.");
        }

        [TestMethod]
        public void ObtenerMontoDeLosPagosDeMeseros_SinPagosMesero_Test()
        {
            // Arrange
            GestorVentas gestorVentas = new GestorVentas();

            Pago pago1 = new Pago(1, 1, ERol.Delivery, 150.50m, ETipoDePago.TarjetaDeCredito);
            Pago pago2 = new Pago(2, 2, ERol.Delivery, 200.75m, ETipoDePago.Contado);

            gestorVentas.Pagos.Add(pago1);
            gestorVentas.Pagos.Add(pago2);

            decimal totalEsperado = 0m;

            // Act
            decimal totalActual = gestorVentas.ObtenerMontoDeLosPagosDeMeseros();

            // Assert
            Assert.AreEqual(totalEsperado, totalActual, "El total de los montos de los pagos de Meseros debería ser 0 cuando no hay pagos de Meseros.");
        }
    }
}
