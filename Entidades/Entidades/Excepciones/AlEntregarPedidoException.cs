using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlEntregarPedidoException : Exception
    {
        public AlEntregarPedidoException() { }
        public AlEntregarPedidoException(string msg) :base(msg){ }

        public AlEntregarPedidoException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
