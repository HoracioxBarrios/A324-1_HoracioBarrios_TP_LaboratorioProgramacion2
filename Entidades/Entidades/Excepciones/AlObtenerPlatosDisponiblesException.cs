using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlObtenerPlatosDisponiblesException :Exception
    {
        public AlObtenerPlatosDisponiblesException() { }

        public AlObtenerPlatosDisponiblesException(string message) :base(message){ }

        public AlObtenerPlatosDisponiblesException(string message, Exception innerException):base(message, innerException) { }
    }
}
