using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ReadOneEnDbException : Exception
    {
        public ReadOneEnDbException() { }  
        public ReadOneEnDbException(string message) : base(message) { }

        public ReadOneEnDbException(string message, Exception innerException) : base(message, innerException) { }
    }
}
