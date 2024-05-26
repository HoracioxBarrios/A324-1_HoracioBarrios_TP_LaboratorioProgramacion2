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
        private static int _id = 0;
        private int _cantidadComnsales;
        private Mesero _mozo;
        private EStateMesa _estado = EStateMesa.Cerrada;
        
        static Mesa()
        {
            _id = 1;
        }
        public Mesa(int cantidadComnsales) 
        {
            _cantidadComnsales = cantidadComnsales;


        }
        public Mesa(int cantidadComnsales, Mesero mozo) : this(cantidadComnsales)
        {            
            _mozo = mozo;
        }

        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }


    }
}
