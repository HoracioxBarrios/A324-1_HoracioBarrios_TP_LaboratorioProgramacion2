using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ErrorAlObtenerIDException : Exception
    {
        public ErrorAlObtenerIDException() { }
        public ErrorAlObtenerIDException(string message) : base(message) { }
        public ErrorAlObtenerIDException(string message, Exception innerException) : base(message , innerException) { }
    }
}
