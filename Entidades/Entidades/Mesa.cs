using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Enumerables;

namespace Entidades
{
    public class Mesa
    {
        private int _cantidadComnsales;
        private Mesero _mozo;
        private EStateMesa _estado = EStateMesa.Cerrada;
        
        public Mesa(int cantidadComnsales) 
        {
            _cantidadComnsales = cantidadComnsales;
        }
        public Mesa(int cantidadComnsales, Mesero mozo) : this(cantidadComnsales)
        {            
            _mozo = mozo;
        }


    }
}
