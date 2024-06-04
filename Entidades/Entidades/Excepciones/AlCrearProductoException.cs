using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlCrearProductoException : Exception
    {
        public AlCrearProductoException() { }
        public AlCrearProductoException(string message) : base(message) { }
        public AlCrearProductoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
