using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ProveedorErrorAlCrearException : Exception
    {
        public ProveedorErrorAlCrearException() { }
        public ProveedorErrorAlCrearException(string message): base (message) { }

        public ProveedorErrorAlCrearException(string message, Exception innerException): base (message, innerException) { }
    }
}
