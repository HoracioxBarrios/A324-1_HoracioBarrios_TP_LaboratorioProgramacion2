using Entidades;
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
        private List<IPago> _historialDeLosPagosDeLasVentasGenerales;
        

        public GestorContable(IArca arca) 
        { 
            _historialDeLosPagosDeLasVentasGenerales = new List<IPago>();
            _arca = arca;
        
        }


        public void RecibirPagosDeLasVentasDelTurno(List<IPago> pagosDeLasVentasDelTurno)
        {
            if(pagosDeLasVentasDelTurno.Count < 0)
            {
                throw new ListaVaciaException("La Lista con los pagos del turno esta Vacia");
            }

            foreach (Pago pago in pagosDeLasVentasDelTurno) 
            {
                _historialDeLosPagosDeLasVentasGenerales.Add(pago);
                _arca.AgregarDinero(pago.Monto);
            }
        }

    }
}
