using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class EmpleadoErrorAlCrearException : Exception
    {
        public EmpleadoErrorAlCrearException() { }
        public EmpleadoErrorAlCrearException(string message) : base(message) { }
        public EmpleadoErrorAlCrearException(string message, Exception innerException) : base(message, innerException) { }
    }
}
