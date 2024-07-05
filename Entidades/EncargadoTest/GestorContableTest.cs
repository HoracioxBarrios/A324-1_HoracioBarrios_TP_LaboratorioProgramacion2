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
        public void AlCerrarTurnoSeAgregaDineroArca_SeReflejaElDineroQueSeSumaEnLasArcas()
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



        [TestMethod]
        public void PruebaPagarEmpleadosConSuficienteDinero()
        {
            // Arrange
            var arca = new Arca();
            arca.AgregarDinero(10000); // Monto inicial en el arca

            var gestorContable = new GestorContable(arca);
            List<IEmpleado> empleados = new List<IEmpleado>
            {
                new Encargado(1, ERol.Encargado, "Juan", "Perez", "123456789", "Calle A", 2000),
                new Cocinero(2, ERol.Cocinero, "Maria", "Gonzalez", "987654321", "Calle B", 1500),
                new Mesero(3, ERol.Mesero, "Pedro", "Rodriguez", "456789123", "Calle C", 1000),
                new Delivery(4, ERol.Delivery, "Ana", "Martinez", "654321987", "Calle D", 800)
            };

            // Act
            gestorContable.PagarEmpleadosSegunPrioridad(empleados);

            // Assert

            Assert.AreEqual(4700, arca.ObtenerMontoDisponible()); // debe quedar 4700 pesitos en el arca
        }


        [TestMethod]
        public void PruebaPagarEmpleadosConInsuficienteDinero()
        {
            // Arrange
            var arca = new Arca();
            arca.AgregarDinero(3500); // Monto inicial en el arca, menos del necesario para pagar a todos

            var gestorContable = new GestorContable(arca);
            List<IEmpleado> empleados = new List<IEmpleado>
            {
                new Encargado(1, ERol.Encargado, "Juan", "Perez", "123456789", "Calle A", 2000), //ok
                new Cocinero(2, ERol.Cocinero, "Maria", "Gonzalez", "987654321", "Calle B", 1500), //ok
                new Mesero(3, ERol.Mesero, "Pedro", "Rodriguez", "456789123", "Calle C", 1000),
                new Delivery(4, ERol.Delivery, "Ana", "Martinez", "654321987", "Calle D", 800)
            };

            // Act
            gestorContable.PagarEmpleadosSegunPrioridad(empleados);

            // Assert

            Assert.IsTrue(arca.ObtenerMontoDisponible() < 3500);  // Verificar que el arca queda con menos dinero del que se esperaba inicialmente

            // Verificar que algunos empleados quedaron con el pago pendiente
            foreach (var empleado in empleados)
            {
                if (empleado.Rol == ERol.Delivery || empleado.Rol == ERol.Mesero)
                {
                    Assert.IsTrue(empleado.CobroMensualPendienteACobrar); // Verificamos que estos roles quedaron con el pago pendiente
                }
                else
                {
                    Assert.IsFalse(empleado.CobroMensualPendienteACobrar); // Verificamos que otros roles NO tienen el pago pendiente
                }
            }
        }

    }
}
