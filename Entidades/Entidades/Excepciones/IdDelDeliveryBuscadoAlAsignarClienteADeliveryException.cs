using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class IdDelDeliveryBuscadoAlAsignarClienteADeliveryException : Exception
    {
        public IdDelDeliveryBuscadoAlAsignarClienteADeliveryException() { }
        public IdDelDeliveryBuscadoAlAsignarClienteADeliveryException(string msg) : base(msg) { }

        public IdDelDeliveryBuscadoAlAsignarClienteADeliveryException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
