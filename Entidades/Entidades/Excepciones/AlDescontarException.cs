using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlDescontarException : Exception
    {

        public AlDescontarException() { }
        public AlDescontarException(string message) :base(message){ }
        public AlDescontarException (string message, Exception innerException) :base(message, innerException){ }
    }
}
