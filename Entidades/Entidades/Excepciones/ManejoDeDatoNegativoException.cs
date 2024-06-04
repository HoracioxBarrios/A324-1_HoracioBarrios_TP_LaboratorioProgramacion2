using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ManejoDeDatoNegativoException : Exception
    {

        public ManejoDeDatoNegativoException() { }

        public ManejoDeDatoNegativoException(string message) : base(message) { }

        public ManejoDeDatoNegativoException(string message, Exception innerException) : base(message, innerException) { }  

    }
}
