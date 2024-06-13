using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlCrearEmpleadoEnDBException : Exception
    {
        public AlCrearEmpleadoEnDBException() { }

        public AlCrearEmpleadoEnDBException(string message) : base(message) { }

        public AlCrearEmpleadoEnDBException(string message, Exception innerException) : base(message, innerException) { }
    }
}
