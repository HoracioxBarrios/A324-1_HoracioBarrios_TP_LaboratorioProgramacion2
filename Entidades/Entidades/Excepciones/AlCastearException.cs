using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlCastearException : Exception
    {

        public AlCastearException() { }
        public AlCastearException(string message) : base(message) { }
        public AlCastearException(string message,  Exception innerException) : base(message, innerException) { }
    }
}
