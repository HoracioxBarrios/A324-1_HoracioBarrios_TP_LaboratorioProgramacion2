using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ErrorDatosDeProductoException : Exception
    {

        public ErrorDatosDeProductoException() { } 

        public ErrorDatosDeProductoException(string message) : base(message) { }

        public ErrorDatosDeProductoException(string mensaje, Exception innerException) : base(mensaje, innerException) { }
    }
}
