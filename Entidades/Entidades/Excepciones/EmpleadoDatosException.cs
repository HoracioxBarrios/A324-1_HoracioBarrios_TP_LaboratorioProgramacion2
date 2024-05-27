using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class EmpleadoDatosException : Exception
    {

        public EmpleadoDatosException() : base() { }

        public EmpleadoDatosException(string message) : base(message)
        {

        }
        public EmpleadoDatosException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
