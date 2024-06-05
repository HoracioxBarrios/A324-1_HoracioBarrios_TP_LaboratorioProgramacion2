using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ValorNegativoEnTipoDeUnidadDeMedidaException : Exception
    {
        public ValorNegativoEnTipoDeUnidadDeMedidaException() { }   
        public ValorNegativoEnTipoDeUnidadDeMedidaException(string message) : base(message) { }
        public ValorNegativoEnTipoDeUnidadDeMedidaException(string message, Exception innerEception) : base(message, innerEception) { } 
    }
}
