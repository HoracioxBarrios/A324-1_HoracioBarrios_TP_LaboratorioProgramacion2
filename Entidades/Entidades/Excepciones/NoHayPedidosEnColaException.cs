using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class NoHayPedidosEnColaException: Exception
    {
        public NoHayPedidosEnColaException() { }
        public NoHayPedidosEnColaException(string message) : base(message) { }
        public NoHayPedidosEnColaException(string message,  Exception innerException) : base(message, innerException) { }
    }
}
