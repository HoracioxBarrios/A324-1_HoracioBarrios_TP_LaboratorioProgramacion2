using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ArcasMontoInvalidoException : Exception
    {
        public ArcasMontoInvalidoException() { }

        public ArcasMontoInvalidoException(string message) : base(message) { }

        public ArcasMontoInvalidoException(string message, Exception innerException): base(message, innerException) { }
    }
}
