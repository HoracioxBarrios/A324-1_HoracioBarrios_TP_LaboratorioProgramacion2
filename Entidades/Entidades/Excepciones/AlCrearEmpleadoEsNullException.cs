using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlCrearEmpleadoEsNullException :Exception
    {
        public AlCrearEmpleadoEsNullException() { }

        public AlCrearEmpleadoEsNullException(string message) : base(message) { }

        public AlCrearEmpleadoEsNullException(string message,  Exception innerException) : base(message, innerException) { }
    }
}
