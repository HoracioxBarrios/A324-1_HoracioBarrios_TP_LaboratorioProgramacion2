using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlLeerEmpleadoException : Exception
    {
        public AlLeerEmpleadoException() { }

        public AlLeerEmpleadoException(string message):base(message) { }

        public AlLeerEmpleadoException (string message, Exception innerException) :base (message, innerException){ }   
    }
}
