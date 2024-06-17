using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class EmpleadoExisteEnDBException : Exception
    {

        public EmpleadoExisteEnDBException() { }

        public EmpleadoExisteEnDBException(string message) : base(message) { }  

        public EmpleadoExisteEnDBException(string message, Exception innrerEception):base(message, innrerEception) { }
    }
}
