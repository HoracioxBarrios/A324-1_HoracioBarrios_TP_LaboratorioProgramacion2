using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class DatoIncorrectoException : Exception
    {
        public DatoIncorrectoException() { }
        public DatoIncorrectoException(string message) : base(message) { }
        public DatoIncorrectoException(string message, Exception innerException): base(message, innerException) { }
    }
}
