using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ErrorAlBuscarClienteEnListaEnGestorDelivery :Exception
    {
        public ErrorAlBuscarClienteEnListaEnGestorDelivery() { }
        public ErrorAlBuscarClienteEnListaEnGestorDelivery(string msg) : base(msg) { }
        public ErrorAlBuscarClienteEnListaEnGestorDelivery(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
