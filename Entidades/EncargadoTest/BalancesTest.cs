using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Collections.Generic;
    using Entidades;
    using Entidades.Enumerables;
    using Entidades.Interfaces;
    using Negocio;
    using Entidades.Services;

    namespace Test
    {
        [TestClass]
        public class Balances
        {
            [TestMethod]
            public void BalanceEntreVentasYCompras()
            {
                // Arrange
                decimal montoInicialArca = 100000m; // Monto inicial del Arca = cien mil
                IArca arca = new Arca();
                arca.AgregarDinero(montoInicialArca); // Agregar monto inicial al Arca

                GestorContable gestorContable = new GestorContable(arca);

                // Simulación de cobro por venta con fecha específica
                DateTime fechaVenta1 = new DateTime(2024, 7, 5);
                ICobro cobro1 = new Cobro(1, 100, ERol.Mesero, 1200m, ETipoDePago.Contado) { Fecha = fechaVenta1 };

                DateTime fechaVenta2 = new DateTime(2024, 7, 6);
                ICobro cobro2 = new Cobro(2, 101, ERol.Delivery, 500m, ETipoDePago.TarjetaDeCredito) { Fecha = fechaVenta2 };

                List<ICobro> cobrosDeVentas = new List<ICobro> { cobro1, cobro2 };

                gestorContable.CobrarPagosDeLasVentasDelTurno(cobrosDeVentas);

                // Creación de proveedor real
                IProveedor proveedor = new Proveedor(
                    "Proveedor1", "12345", "Dirección1", ETipoDeProducto.Ingrediente,
                    EMediosDePago.Contado, EAcreedor.Si, EDiaDeLaSemana.Lunes
                );

                IProducto producto = ProductoServiceFactory.CrearProducto(
                    ETipoDeProducto.Ingrediente, 1, "Producto1", 800, EUnidadDeMedida.Unidad, 800m, proveedor);

                gestorContable.PagarProveedor(producto, proveedor);

                // Act
                DateTime fechaInicio = new DateTime(2024, 7, 1); // Fecha de inicio del periodo
                DateTime fechaFin = new DateTime(2024, 7, 31); // Fecha de fin del periodo

                // Calcular totales de ventas y compras en el periodo especificado
                decimal totalVentas = gestorContable.CalcularTotalVentasEnPeriodo(fechaInicio, fechaFin);
                decimal totalCompras = gestorContable.CalcularTotalComprasEnPeriodo(fechaInicio, fechaFin);
                decimal balance = gestorContable.CalcularBalanceEnPeriodo(fechaInicio, fechaFin);

                // Assert
                Assert.AreEqual(1700m, totalVentas, $"El total de ventas para el periodo {fechaInicio.ToShortDateString()} - {fechaFin.ToShortDateString()} no coincide.");
                Assert.AreEqual(800m, totalCompras, $"El total de compras para el periodo {fechaInicio.ToShortDateString()} - {fechaFin.ToShortDateString()} no coincide.");
                Assert.AreEqual(900m, balance, $"El balance entre ventas y compras para el periodo {fechaInicio.ToShortDateString()} - {fechaFin.ToShortDateString()} no es el esperado.");

                // Verificar el monto en el arca después de las operaciones
                Assert.AreEqual(100900m, arca.ObtenerMontoDisponible(), "El monto en el arca no coincide después de las operaciones.");
            }


        }
    }
}
