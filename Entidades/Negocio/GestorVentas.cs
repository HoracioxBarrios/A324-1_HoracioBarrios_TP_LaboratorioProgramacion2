using Entidades;
using Entidades.Excepciones;
using Entidades.Interfaces;
using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
