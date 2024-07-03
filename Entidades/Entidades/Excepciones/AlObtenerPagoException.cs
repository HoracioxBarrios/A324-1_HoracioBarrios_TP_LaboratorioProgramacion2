using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlObtenerPagoException : Exception
    {
        public AlObtenerPagoException() { }

        public AlObtenerPagoException(string message) : base(message) { }

        public AlObtenerPagoException(string message, Exception innerException): base(message, innerException) { }
    }
}
