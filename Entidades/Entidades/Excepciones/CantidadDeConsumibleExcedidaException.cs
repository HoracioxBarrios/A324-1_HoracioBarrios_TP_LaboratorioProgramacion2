using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class CantidadDeConsumibleExcedidaException : Exception
    {

        public CantidadDeConsumibleExcedidaException() { }

        public CantidadDeConsumibleExcedidaException(string message) : base(message) { }    
        public CantidadDeConsumibleExcedidaException(string message, Exception innerException): base(message, innerException) { }
    }
}
