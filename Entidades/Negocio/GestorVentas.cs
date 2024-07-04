using Entidades;
using Entidades.Excepciones;
using Entidades.Interfaces;
using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;

namespace Negocio
{
    public class GestorVentas : IGestorVentas
    {

        private List<IPago> _pagosDeLasVentas;





        public GestorVentas() 
        { 
            _pagosDeLasVentas = new List<IPago>();
        }

        /// <summary>
        /// Indexador para buscar el Pago en la lista de pagos de las Ventas Generales (Local y Delivery)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public IPago this[int index]
        {
            get
            {
                if (index < 0 || index >= _pagosDeLasVentas.Count)
                {
                    throw new IndexOutOfRangeException("El índice está fuera del rango de la lista de pagos.");
                }
                return _pagosDeLasVentas[index];
            }
            set
            {
                if (index < 0 || index >= _pagosDeLasVentas.Count)
                {
                    throw new IndexOutOfRangeException("El índice está fuera del rango de la lista de pagos.");
                }
                _pagosDeLasVentas[index] = value ?? throw new ArgumentNullException(nameof(value), "El pago no puede ser nulo.");
            }
        }


        public void RegistrarPago(IPago pago)
        {
            if (pago == null)
            {
                throw new ArgumentNullException(nameof(pago), "El pago no puede ser nulo.");
            }
            _pagosDeLasVentas.Add(pago);
        }



        public IPago ObtenerPago(int id)
        {
            foreach(IPago pago in _pagosDeLasVentas)
            {
                if(pago.Id == id)
                {
                    return pago;
                }
            }
            throw new AlObtenerPagoException("No se encontró el Pago por Id");
        }

        public List<IPago> ObtenerPagos()
        {
            if(_pagosDeLasVentas.Count < 0)
            {
                throw new NoHayPagosEnLaListaDePagosDeLasVentasException("La Lista de Pagos de las Ventasesta Vacia");
            }
            return _pagosDeLasVentas;
        }


        public decimal ObtenerMontoDeLosPagosDeLosConsumosTotales()
        {
            decimal montos = 0;
            if (_pagosDeLasVentas.Count > 0)
            {
                foreach (Pago pago in _pagosDeLasVentas)
                {
                    montos += pago.Monto;
                }
            }

            return montos;
        }

        public decimal ObtenerMontoDeLosPagosDeDeliverys()
        {
            decimal montos = 0;
            if (_pagosDeLasVentas.Count > 0)
            {
                foreach (Pago pago in _pagosDeLasVentas)
                {
                    if(pago.RolDelCobrador == ERol.Delivery)
                    {
                        montos += pago.Monto;
                    }
                    
                }
            }

            return montos;
        }


        public decimal ObtenerMontoDeLosPagosDeMeseros()
        {
            decimal montos = 0;
            if (_pagosDeLasVentas.Count > 0)
            {
                foreach (Pago pago in _pagosDeLasVentas)
                {
                    if (pago.RolDelCobrador == ERol.Mesero)
                    {
                        montos += pago.Monto;
                    }

                }
            }

            return montos;
        }


        public decimal ObtenerMontoPorTipoDePago(ETipoDePago tipoDePago)
        {
            return _pagosDeLasVentas
                .Where(p => p.TipoPago == tipoDePago)
                .Sum(p => p.Monto);
        }






        public string ObtenerNombreVendedor(int idDelCobrador, ERol rolDelCobrador, List<IEmpleado> empleados)
        {
            string nombreEmpleado = "Nombre no encontrado";

            switch (rolDelCobrador)
            {
                case ERol.Mesero:
                    IEmpleado mesero = empleados.FirstOrDefault(m => m.Id == idDelCobrador);
                    if (mesero != null)
                    {
                        nombreEmpleado = $"{mesero.Nombre} {mesero.Apellido}";
                    }
                    break;
                case ERol.Delivery:
                    IEmpleado delivery = empleados.FirstOrDefault(d => d.Id == idDelCobrador);
                    if (delivery != null)
                    {
                        nombreEmpleado = $"{delivery.Nombre} {delivery.Apellido}";
                    }
                    break;
                default:
                    throw new EmpleadoRolNoExistenteException("No existe el empleado");

            }

            return nombreEmpleado;
        }











        public List<Tuple<string, ERol, decimal>> ObtenerTopVentas(int cantidadParaElTop, List<IEmpleado> empleados)
        {
            Dictionary<int, decimal> ventasPorEmpleado = CalcularVentasDeCadaEmpleado();

            List<KeyValuePair<int, decimal>> topVentas = ventasPorEmpleado.OrderByDescending(pair => pair.Value)
                                                                           .Take(cantidadParaElTop)
                                                                           .ToList();

            List<Tuple<string, ERol, decimal>> resultado = new List<Tuple<string, ERol, decimal>>();

            foreach (KeyValuePair<int, decimal> venta in topVentas)
            {
                IEmpleado empleado = empleados.FirstOrDefault(e => e.Id == venta.Key);
                if (empleado != null)
                {
                    resultado.Add(new Tuple<string, ERol, decimal>(empleado.Nombre, empleado.Rol, venta.Value));
                }
            }

            return resultado;
        }


        private Dictionary<int, decimal> CalcularVentasDeCadaEmpleado()
        {
            Dictionary<int, decimal> ventasPorEmpleado = new Dictionary<int, decimal>();

            foreach (IPago pago in _pagosDeLasVentas)
            {
                if (!ventasPorEmpleado.ContainsKey(pago.IdDelCobrador))
                {
                    ventasPorEmpleado[pago.IdDelCobrador] = 0;
                }
                ventasPorEmpleado[pago.IdDelCobrador] += pago.Monto;
            }

            return ventasPorEmpleado;
        }




        public List<Tuple<string, ERol, decimal>> ObtenerTopVentasPorRol(int cantidadParaElTop, ERol rol, List<IEmpleado> empleados)
        {
            Dictionary<int, decimal> ventasPorRol = CalcularVentasDeCadaEmpleado(rol);

            
            List<KeyValuePair<int, decimal>> topVentas = ventasPorRol.OrderByDescending(pair => pair.Value)
                                                                   .Take(cantidadParaElTop)
                                                                   .ToList();// ordenamos por monto de ventas de manera descendente y tomamos la cantidad especificada en el caso de top 3 de ventas son 3 en la key

            
            List<Tuple<string, ERol, decimal>> resultado = new List<Tuple<string, ERol, decimal>>(); // aca Armamos la lista de Tuplas con (Nombre Empleado, Rol, Monto Acumulado de Ventas)

            foreach (KeyValuePair<int, decimal> venta in topVentas)
            {
                string nombreEmpleado = ObtenerNombreVendedor(venta.Key, rol, empleados);
                resultado.Add(new Tuple<string, ERol, decimal>(nombreEmpleado, rol, venta.Value));
            }

            return resultado;
        }



        //Calcula las ventas de cada empleado -- tiene en cuenta su id empleado buscando en las ventas (Pagos).
        private Dictionary<int, decimal> CalcularVentasDeCadaEmpleado(ERol rol)
        {
            Dictionary<int, decimal> ventasPorRol = new Dictionary<int, decimal>();

            
            foreach (IPago pago in _pagosDeLasVentas)
            {
                if (pago.RolDelCobrador == rol)
                {
                    if (!ventasPorRol.ContainsKey(pago.IdDelCobrador))
                    {
                        ventasPorRol[pago.IdDelCobrador] = 0;
                    }
                    ventasPorRol[pago.IdDelCobrador] += pago.Monto;
                }
            }
            return ventasPorRol;
        }

    }
}
