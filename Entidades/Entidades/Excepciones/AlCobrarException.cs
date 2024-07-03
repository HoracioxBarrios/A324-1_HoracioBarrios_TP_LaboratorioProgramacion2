using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlCobrarException : Exception
    {
        public AlCobrarException() { }

        public AlCobrarException(string message) : base(message) { }

        public AlCobrarException(string message, Exception innerException) : base(message, innerException) { }
    }
}
