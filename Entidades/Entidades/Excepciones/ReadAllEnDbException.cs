using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ReadAllEnDbException : Exception
    {
        public ReadAllEnDbException() { }

        public ReadAllEnDbException(string message) : base(message) { }

        public ReadAllEnDbException(string message, Exception innerException) : base(message, innerException) { }
    }
}
