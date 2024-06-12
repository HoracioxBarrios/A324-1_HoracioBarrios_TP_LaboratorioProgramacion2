using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlCrearTablaEmpleadoException : Exception
    {
        public AlCrearTablaEmpleadoException() { }

        public AlCrearTablaEmpleadoException(string message) : base(message) { }

        public AlCrearTablaEmpleadoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
