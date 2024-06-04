using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlRestarException : Exception
    {

        public AlRestarException() { }
        public AlRestarException(string message) :base(message){ }
        public AlRestarException(string message, Exception innerException) :base(message, innerException) { }
    }
}
