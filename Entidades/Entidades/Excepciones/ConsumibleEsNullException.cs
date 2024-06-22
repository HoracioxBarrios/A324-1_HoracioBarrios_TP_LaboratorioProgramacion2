using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ConsumibleEsNullException : Exception
    {
        public ConsumibleEsNullException() { }
        public ConsumibleEsNullException(string message) :base(message){ }
        public ConsumibleEsNullException(string message, Exception innerException) : base(message, innerException){ }  
    }
}
