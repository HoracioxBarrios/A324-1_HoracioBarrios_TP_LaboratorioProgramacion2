using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlConvertirException : Exception
    {

        public AlConvertirException() { }
        public AlConvertirException(string message) :base(message){ }
        public AlConvertirException (string message, Exception innerException) :base(message, innerException){ }
    }
}
