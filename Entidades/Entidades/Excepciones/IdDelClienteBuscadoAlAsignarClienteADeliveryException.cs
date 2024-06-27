using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class IdDelClienteBuscadoAlAsignarClienteADeliveryException :Exception
    {

        public IdDelClienteBuscadoAlAsignarClienteADeliveryException() { }
        public IdDelClienteBuscadoAlAsignarClienteADeliveryException(string msg) : base(msg) { }

        public IdDelClienteBuscadoAlAsignarClienteADeliveryException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
