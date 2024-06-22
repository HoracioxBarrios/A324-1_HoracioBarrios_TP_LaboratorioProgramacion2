using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ConsumibleInvalidoException : Exception
    {
        public ConsumibleInvalidoException() { }
        public ConsumibleInvalidoException(string message) : base(message) { }
        public ConsumibleInvalidoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
