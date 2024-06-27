using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ConsumibleSinPrecioDeVentaException: Exception
    {
        public ConsumibleSinPrecioDeVentaException() { }
        public ConsumibleSinPrecioDeVentaException(string message) : base(message) { }
        public ConsumibleSinPrecioDeVentaException(string message, Exception innerException) : base(message, innerException) { }
    }
}
