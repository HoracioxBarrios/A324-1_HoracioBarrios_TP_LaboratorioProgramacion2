using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlObtenerIDException : Exception
    {
        public AlObtenerIDException() { }
        public AlObtenerIDException(string message) : base(message) { }
        public AlObtenerIDException(string message, Exception innerException) : base(message , innerException) { }
    }
}
