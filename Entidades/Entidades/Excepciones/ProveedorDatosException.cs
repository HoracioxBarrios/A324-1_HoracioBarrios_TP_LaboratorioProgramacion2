using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{

    public class ProveedorDatosException : Exception
    {

        public ProveedorDatosException() { }
        public ProveedorDatosException(string message) : base(message) { }

        public ProveedorDatosException(string message,  Exception innerException) : base(message, innerException) { }
    }
}
