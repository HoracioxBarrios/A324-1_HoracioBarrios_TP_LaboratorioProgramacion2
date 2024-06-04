using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class DatosDeProductoException : Exception
    {

        public DatosDeProductoException() { } 

        public DatosDeProductoException(string message) : base(message) { }

        public DatosDeProductoException(string mensaje, Exception innerException) : base(mensaje, innerException) { }
    }
}
