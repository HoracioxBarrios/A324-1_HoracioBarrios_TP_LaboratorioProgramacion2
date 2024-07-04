using Entidades.Enumerables;
using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Interfaces;
using Datos;
using Entidades.Services;

namespace Test
{
    [TestClass]
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

            gestorVentas.RegistrarPago(pago1);
            gestorVentas.RegistrarPago(pago2);
            gestorVentas.RegistrarPago(pago3);


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

            gestorVentas.RegistrarPago(pago1);
            gestorVentas.RegistrarPago(pago2);
            gestorVentas.RegistrarPago(pago3);
            gestorVentas.RegistrarPago(pago4);
            gestorVentas.RegistrarPago(pago5);

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

            gestorVentas.RegistrarPago(pago1);
            gestorVentas.RegistrarPago(pago2);

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

            gestorVentas.RegistrarPago(pago1);
            gestorVentas.RegistrarPago(pago2);
            gestorVentas.RegistrarPago(pago3);
            gestorVentas.RegistrarPago(pago4);
            gestorVentas.RegistrarPago(pago5);

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

            gestorVentas.RegistrarPago(pago1);
            gestorVentas.RegistrarPago(pago2);


            decimal totalEsperado = 0m;

            // Act
            decimal totalActual = gestorVentas.ObtenerMontoDeLosPagosDeMeseros();

            // Assert
            Assert.AreEqual(totalEsperado, totalActual, "El total de los montos de los pagos de Meseros debería ser 0 cuando no hay pagos de Meseros.");
        }







        [TestMethod]
        public void RegistrarPago_Test()
        {
            // Arrange
            GestorVentas gestorVentas = new GestorVentas();
            Pago pago = new Pago(1, 1, ERol.Mesero, 150.50m, ETipoDePago.TarjetaDeCredito);

            // Act
            gestorVentas.RegistrarPago(pago);

            // Assert
            List<IPago> pagosTotales = gestorVentas.ObtenerPagos();
            Assert.AreEqual(1, pagosTotales.Count, "El pago no se ha registrado correctamente.");
            Assert.AreEqual(pago, gestorVentas[0], "El pago registrado no es el esperado.");
        }

        [TestMethod]
        public void ObtenerMontoPorTipoDePago_Test()
        {
            // Arrange
            GestorVentas gestorVentas = new GestorVentas();

            Pago pago1 = new Pago(1, 1, ERol.Mesero, 150.50m, ETipoDePago.TarjetaDeCredito);
            Pago pago2 = new Pago(2, 2, ERol.Mesero, 200.75m, ETipoDePago.Contado);
            Pago pago3 = new Pago(3, 3, ERol.Delivery, 300.00m, ETipoDePago.Contado);
            Pago pago4 = new Pago(4, 4, ERol.Mesero, 100.00m, ETipoDePago.TarjetaDeCredito);
            Pago pago5 = new Pago(5, 5, ERol.Delivery, 250.25m, ETipoDePago.BilleteraVirtual);

            gestorVentas.RegistrarPago(pago1);
            gestorVentas.RegistrarPago(pago2);
            gestorVentas.RegistrarPago(pago3);
            gestorVentas.RegistrarPago(pago4);
            gestorVentas.RegistrarPago(pago5);

            decimal totalTarjetaDeCreditoEsperado = 150.50m + 100.00m;
            decimal totalContadoEsperado = 200.75m + 300.00m;
            decimal totalBilleteraVirtualEsperado = 250.25m;
            // Act
            decimal totalTarjetaDeCreditoActual = gestorVentas.ObtenerMontoPorTipoDePago(ETipoDePago.TarjetaDeCredito);
            decimal totalContadoActual = gestorVentas.ObtenerMontoPorTipoDePago(ETipoDePago.Contado);
            decimal totaBillerteraVirtualActual = gestorVentas.ObtenerMontoPorTipoDePago(ETipoDePago.BilleteraVirtual);

            // Assert
            Assert.AreEqual(totalTarjetaDeCreditoEsperado, totalTarjetaDeCreditoActual, "El total de los pagos con Tarjeta de Crédito no es el esperado.");
            Assert.AreEqual(totalContadoEsperado, totalContadoActual, "El total de los pagos con Contado no es el esperado.");
            Assert.AreEqual(totalBilleteraVirtualEsperado, totaBillerteraVirtualActual, "El total de los pagos con billetera Virtual no es el esperado");
        }




        [TestMethod]
        public void ObtenerTop3Ventas_MeserosYDelivery_Test()
        {
            // Arrange
            List<IEmpleado> empleados = new List<IEmpleado>();

            IEmpleado empleado1 = EmpleadoServiceFactory.CrearEmpleado(ERol.Delivery, "Willy", "Car", "1145215", "calle 3", 15000m);
            IEmpleado empleado2 = EmpleadoServiceFactory.CrearEmpleado(ERol.Delivery, "Arhi", "Shir", "1155856", "calle 11", 15000m);
            IEmpleado empleado3 = EmpleadoServiceFactory.CrearEmpleado(ERol.Mesero, "Pela", "Trex", "1153300", "av sin luz 44", 15000m);
            IEmpleado empleado4 = EmpleadoServiceFactory.CrearEmpleado(ERol.Delivery, "Liloi", "Feri", "1157775", "av los copos 1", 15000m);
            IEmpleado empleado5 = EmpleadoServiceFactory.CrearEmpleado(ERol.Mesero, "Caro", "Juill", "1150006", "av Sol 10", 15000m);
            IEmpleado empleado6 = EmpleadoServiceFactory.CrearEmpleado(ERol.Mesero, "Juli", "Aser", "1155555", "av los copos 55", 15000m);

            empleado1.Id = 1;
            empleado2.Id = 2;
            empleado3.Id = 3;
            empleado4.Id = 4;
            empleado5.Id = 5;
            empleado6.Id = 6;

            empleados.Add(empleado1);
            empleados.Add(empleado2);
            empleados.Add(empleado3);
            empleados.Add(empleado4);
            empleados.Add(empleado5);
            empleados.Add(empleado6);

            GestorVentas gestorVentas = new GestorVentas();

            // registramos algunos pagos para meseros y delivery
            gestorVentas.RegistrarPago(new Pago(1, 1, ERol.Mesero, 150.50m, ETipoDePago.TarjetaDeCredito));
            gestorVentas.RegistrarPago(new Pago(2, 2, ERol.Mesero, 200.75m, ETipoDePago.Contado));
            gestorVentas.RegistrarPago(new Pago(3, 3, ERol.Delivery, 300.00m, ETipoDePago.Contado));
            gestorVentas.RegistrarPago(new Pago(4, 4, ERol.Delivery, 100.00m, ETipoDePago.Contado));
            gestorVentas.RegistrarPago(new Pago(5, 5, ERol.Mesero, 250.25m, ETipoDePago.BilleteraVirtual));

            // Act                                                                       top 3 ----> podria ser top 2, 1 o el que se quiera
            List<Tuple<string, ERol, decimal>> top3Ventas = gestorVentas.ObtenerTopVentas(3, empleados);

            // Assert
            Assert.AreEqual(3, top3Ventas.Count, "El tamaño del top 3 no es 3.");

        }


        [TestMethod]
        public void ObtenerTop3Ventas_PorRol_Test()
        {
            // Arrange
            List<IEmpleado> empleados = new List<IEmpleado>();

            IEmpleado empleado1 = EmpleadoServiceFactory.CrearEmpleado(ERol.Delivery, "Willy", "Car", "1145215", "calle 3", 15000m);
            IEmpleado empleado2 = EmpleadoServiceFactory.CrearEmpleado(ERol.Delivery, "Arhi", "Shir", "1155856", "calle 11", 15000m);
            IEmpleado empleado3 = EmpleadoServiceFactory.CrearEmpleado(ERol.Mesero, "Pela", "Trex", "1153300", "av sin luz 44", 15000m);
            IEmpleado empleado4 = EmpleadoServiceFactory.CrearEmpleado(ERol.Delivery, "Liloi", "Feri", "1157775", "av los copos 1", 15000m);
            IEmpleado empleado5 = EmpleadoServiceFactory.CrearEmpleado(ERol.Mesero, "Caro", "Juill", "1150006", "av Sol 10", 15000m);
            IEmpleado empleado6 = EmpleadoServiceFactory.CrearEmpleado(ERol.Mesero, "Juli", "Aser", "1155555", "av los copos 55", 15000m);

            empleado1.Id = 1;
            empleado2.Id = 2;
            empleado3.Id = 3;
            empleado4.Id = 4;
            empleado5.Id = 5;
            empleado6.Id = 6;

            empleados.Add(empleado1);
            empleados.Add(empleado2);
            empleados.Add(empleado3);
            empleados.Add(empleado4);
            empleados.Add(empleado5);
            empleados.Add(empleado6);



            GestorVentas gestorVentas = new GestorVentas();


            // registramos algunos pagos para meseros y delivery
            gestorVentas.RegistrarPago(new Pago(1, 1, ERol.Mesero, 150.50m, ETipoDePago.TarjetaDeCredito));
            gestorVentas.RegistrarPago(new Pago(2, 2, ERol.Mesero, 200.75m, ETipoDePago.Contado));
            gestorVentas.RegistrarPago(new Pago(3, 3, ERol.Delivery, 300.00m, ETipoDePago.Contado));
            gestorVentas.RegistrarPago(new Pago(4, 4, ERol.Delivery, 100.00m, ETipoDePago.Contado));
            gestorVentas.RegistrarPago(new Pago(5, 5, ERol.Mesero, 250.25m, ETipoDePago.BilleteraVirtual));

            // Act                                                                          top 3
            List<Tuple<string, ERol, decimal>> top3Ventas = gestorVentas.ObtenerTopVentasPorRol(3, ERol.Mesero, empleados);

            // Assert -----------> debe haber 3 en el Top
            Assert.AreEqual(3, top3Ventas.Count, "El tamaño del top 3 no es 3.");
        }
    }
}
