using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlEditarEmpleadoEnDBException : Exception
    {
        public AlEditarEmpleadoEnDBException() { }

        public AlEditarEmpleadoEnDBException(string message) : base(message) { }

        public AlEditarEmpleadoEnDBException(string message,  Exception innerException) : base(message, innerException) { }

    }
}
