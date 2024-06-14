using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlObtenerPedidosDeLaMesaException : Exception
    {
        public AlObtenerPedidosDeLaMesaException() { }

        public AlObtenerPedidosDeLaMesaException(string message) : base(message) { }


        public AlObtenerPedidosDeLaMesaException(string message, Exception innerException):base(message, innerException) { }    
    }
}
