using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class UnidadDeMedidaDesconocidaException : Exception
    {
        public UnidadDeMedidaDesconocidaException() { }    

        public UnidadDeMedidaDesconocidaException(string message) : base(message) { }

        public UnidadDeMedidaDesconocidaException(string message, Exception innerException) :base(message, innerException) { }
    }
}
