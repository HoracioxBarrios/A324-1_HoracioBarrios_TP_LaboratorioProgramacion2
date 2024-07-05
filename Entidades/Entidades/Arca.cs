using Entidades.Excepciones;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Arca :IArca
    {
        private decimal _montoDineroDisponibleDelRestoran;

        public Arca() { }

        public void AgregarDinero(decimal montoAAgregar)
        {
            if(montoAAgregar < 0)
            {
                throw new ArcasMontoInvalidoException("El monto a Agregar a las Arcas es invalido");
            }
            _montoDineroDisponibleDelRestoran += montoAAgregar;

        }

        public decimal TomarDinero(decimal montoNecesario)
        {
            decimal dinero = 0;
            if(_montoDineroDisponibleDelRestoran > 0 && montoNecesario >= _montoDineroDisponibleDelRestoran)
            {
                _montoDineroDisponibleDelRestoran -= montoNecesario;
            }
            return montoNecesario;
        }



        public decimal ObtenerMontoDisponible()
        {
            return _montoDineroDisponibleDelRestoran;
        }
    }


}
