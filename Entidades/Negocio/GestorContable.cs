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
        private List<ICobro> _historialDeLosCobrosDeLasVentasGenerales;
        

        public GestorContable(IArca arca) 
        { 
            _historialDeLosCobrosDeLasVentasGenerales = new List<ICobro>();
            _arca = arca;
        
        }


        public void RecibirPagosDeLasVentasDelTurno(List<ICobro> pagosDeLasVentasDelTurno)
        {
            if(pagosDeLasVentasDelTurno.Count < 0)
            {
                throw new ListaVaciaException("La Lista con los pagos del turno esta Vacia");
            }

            foreach (Cobro cobro in pagosDeLasVentasDelTurno) 
            {
                _historialDeLosCobrosDeLasVentasGenerales.Add(cobro);
                _arca.AgregarDinero(cobro.Monto);
            }
        }

    }
}
