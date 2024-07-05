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
        private List<IPago> _historialDePagos;

        public GestorContable(IArca arca) 
        { 
            _historialDeLosCobrosDeLasVentas = new List<ICobro>();
            _historialDePagos = new List<IPago>();
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

                    _historialDePagos.Add(pago);
                }
                else
                {
                    // Si no hay suficiente dinero o el salario ya fue cobrado, continuar con el siguiente empleado - (Por defecto el empleado tiene Salario no cobrado aun.)
                    continue;
                }
            }
        }
    }
}
