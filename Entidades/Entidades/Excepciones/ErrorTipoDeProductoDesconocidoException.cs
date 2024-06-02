using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ErrorTipoDeProductoDesconocidoException : Exception
    {
        public ErrorTipoDeProductoDesconocidoException() { }
        public ErrorTipoDeProductoDesconocidoException(string msg) : base(msg) { }
        public ErrorTipoDeProductoDesconocidoException(string message, Exception innerException) : base(message, innerException) { }    
    }
}
