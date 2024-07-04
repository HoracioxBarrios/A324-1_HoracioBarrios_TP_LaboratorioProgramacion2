using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class NoHayPagosEnLaListaDePagosDeLasVentasException : Exception
    {
        public NoHayPagosEnLaListaDePagosDeLasVentasException() { }

        public NoHayPagosEnLaListaDePagosDeLasVentasException(string message) : base(message) { }

        public NoHayPagosEnLaListaDePagosDeLasVentasException(string message, Exception innerException) : base(message, innerException) { }
    }
}
