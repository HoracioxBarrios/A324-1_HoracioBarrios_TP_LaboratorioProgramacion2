using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Mesa
    {
        private int _cantidadComnsales;
        private Mesero _mozo;
        
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
