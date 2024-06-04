using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class TipoDeProductoDesconocidoException : Exception
    {
        public TipoDeProductoDesconocidoException() { }
        public TipoDeProductoDesconocidoException(string msg) : base(msg) { }
        public TipoDeProductoDesconocidoException(string message, Exception innerException) : base(message, innerException) { }    
    }
}
