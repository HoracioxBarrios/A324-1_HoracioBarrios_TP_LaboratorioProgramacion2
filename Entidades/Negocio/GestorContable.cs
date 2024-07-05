using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class GestorContable : IGestorContable
    {
        private IArca _arca;
        private List<ICobro> _historialDeLosCobrosDeLasVentas;


        private List<IPago> _historialPagosRealizadosEmpleados; // Los pagos pendientes a Empleados se manejan distinto a los de Proveedores. ya que al empleado no se genera el pago (si no hay plata )y al proveedor si se genera el pago (pero con estado pendiente )Y se asigna a una lista de pendientes abajo, para pagar luego.

        private List<IPago> _historialPagosRealizadosAProveedores;
        private List<IPago> _pagosPendientesAProveedores;
        






        public GestorContable(IArca arca) 
        { 
            _historialDeLosCobrosDeLasVentas = new List<ICobro>();
            _historialPagosRealizadosEmpleados = new List<IPago>();

            _historialPagosRealizadosAProveedores = new List<IPago>();
            _pagosPendientesAProveedores = new List<IPago>();   
            _arca = arca;
        
        }


        public void CobrarPagosDeLasVentasDelTurno(List<ICobro> cobrosDeLasVentasDelTurno)
        {
            if(cobrosDeLasVentasDelTurno.Count < 0)
            {
                throw new ListaVaciaException("La Lista con los pagos del turno esta Vacia");
            }

            foreach (Cobro cobro in cobrosDeLasVentasDelTurno) 
            {
                _historialDeLosCobrosDeLasVentas.Add(cobro);
                _arca.AgregarDinero(cobro.Monto);
            }
        }
        public void Pagar(decimal montoAPagar)
        {
            _arca.TomarDinero(montoAPagar);
        }

        public decimal ObtenerMontoDisponible()
        {
            decimal montoEnArca = 0;
            if(_arca.ObtenerMontoDisponible() > 0)
            {
                montoEnArca = _arca.ObtenerMontoDisponible();
            }
            return montoEnArca;
        }





        public decimal CalcularDineroDeSalariosEmpleadosTotales(List<IEmpleado> empleados)
        {
            decimal total = 0;
            foreach (IEmpleado empleado in empleados)
            {
                total += empleado.Salario;
            }
            return total;
        }


        public void PagarEmpleadosSegunPrioridad(List<IEmpleado> empleados)
        {
            // Ordenar empleados por prioridad (Encargados -> Cocineros -> Meseros y Delivery)
            var empleadosOrdenadosPorPrioridad = empleados.OrderBy(e =>
            {
                switch (e.Rol)
                {
                    case ERol.Encargado:
                        return 1;
                    case ERol.Cocinero:
                        return 2;
                    case ERol.Mesero:
                    case ERol.Delivery:
                        return 3;
                    default:
                        return int.MaxValue;
                }
            }).ToList();

            foreach (var empleado in empleadosOrdenadosPorPrioridad)
            {
                decimal montoAPagar = empleado.Salario;
                if (_arca.ObtenerMontoDisponible() >= montoAPagar && empleado.CobroMensualPendienteACobrar)
                {
                    _arca.TomarDinero(montoAPagar);
                    IPago pago = new Pago(empleado.Nombre, montoAPagar);
                    empleado.RecibirPago(pago); // Se marca como cobrado

                    _historialPagosRealizadosEmpleados.Add(pago);
                }
                else
                {
                    // Si no hay suficiente dinero o el salario ya fue cobrado, continua con el siguiente empleado - (Por defecto el empleado tiene Salario no cobrado aun.) y para pagarle simplemente se corre de nuevo si hay plata se le pagara. ----> con el provvedor es distinto. este se agrega a una lista de pendientes y el proveedor se marca como pago pendiente.
                    continue;
                }
            }
        }

        public void PagarProveedor(IProducto producto, IProveedor proveedor)
        {
            decimal precioProducto = producto.Precio;
            IPago pago = new Pago(proveedor.Nombre, precioProducto);

            if (_arca.ObtenerMontoDisponible() >= precioProducto)
            {
                _arca.TomarDinero(precioProducto);
                _historialPagosRealizadosAProveedores.Add(pago);
            }
            else
            {               

                proveedor.UsarCuentaCorriente();// Usamos la cuenta corriente del proveedor y agregamos a pagos pendientes a proveedores
                _pagosPendientesAProveedores.Add(pago);
            }
        }

        #region Balances
        public List<ICobro> ObtenerCobrosEnPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            return _historialDeLosCobrosDeLasVentas.Where(cobro => cobro.Fecha >= fechaInicio && cobro.Fecha <= fechaFin)
                .ToList();
        }

        public List<IPago> ObtenerPagosAProveedoresEnPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            return _historialPagosRealizadosAProveedores
                .Where(pago => pago.FechaPago >= fechaInicio && pago.FechaPago <= fechaFin)
                .ToList();
        }

        public decimal CalcularTotalVentasEnPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            var cobrosEnPeriodo = ObtenerCobrosEnPeriodo(fechaInicio, fechaFin);
            return cobrosEnPeriodo.Sum(cobro => cobro.Monto);
        }
        public decimal CalcularTotalComprasEnPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            var pagosEnPeriodo = ObtenerPagosAProveedoresEnPeriodo(fechaInicio, fechaFin);
            return pagosEnPeriodo.Sum(pago => pago.Monto);
        }

        public decimal CalcularBalanceEnPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            decimal totalVentas = CalcularTotalVentasEnPeriodo(fechaInicio, fechaFin);
            decimal totalCompras = CalcularTotalComprasEnPeriodo(fechaInicio, fechaFin);

            return totalVentas - totalCompras;
        }



        #endregion
    }
}
