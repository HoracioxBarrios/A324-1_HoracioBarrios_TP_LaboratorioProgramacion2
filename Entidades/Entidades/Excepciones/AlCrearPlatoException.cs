using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlCrearPlatoException :Exception
    {
        public AlCrearPlatoException() { }

        public AlCrearPlatoException(string message) : base(message) { }

        public AlCrearPlatoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
