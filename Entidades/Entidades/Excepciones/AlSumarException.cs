using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlSumarException : Exception
    {

        public AlSumarException() { } 
        public AlSumarException(string message) : base(message) { }
        public AlSumarException(string message,  Exception innerException) : base(message, innerException) { }
    }
}
