using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ErrorAlCrearProductoException : Exception
    {
        public ErrorAlCrearProductoException() { }
        public ErrorAlCrearProductoException(string message) : base(message) { }
        public ErrorAlCrearProductoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
