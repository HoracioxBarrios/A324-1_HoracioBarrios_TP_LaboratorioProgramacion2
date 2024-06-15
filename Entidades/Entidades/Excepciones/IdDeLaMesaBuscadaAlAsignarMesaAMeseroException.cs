using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class IdDeLaMesaBuscadaAlAsignarMesaAMeseroException :Exception
    {
        public IdDeLaMesaBuscadaAlAsignarMesaAMeseroException() { }
        public IdDeLaMesaBuscadaAlAsignarMesaAMeseroException(string message) : base(message) { }
        public IdDeLaMesaBuscadaAlAsignarMesaAMeseroException(string message, Exception innerException) : base(message, innerException) { }
    }
}
