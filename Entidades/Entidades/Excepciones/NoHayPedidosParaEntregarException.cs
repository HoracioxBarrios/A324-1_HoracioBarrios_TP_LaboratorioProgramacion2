using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class NoHayPedidosParaEntregarException : Exception
    {
        public NoHayPedidosParaEntregarException() { }
        public NoHayPedidosParaEntregarException(string msg) : base(msg) { }
        public NoHayPedidosParaEntregarException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
