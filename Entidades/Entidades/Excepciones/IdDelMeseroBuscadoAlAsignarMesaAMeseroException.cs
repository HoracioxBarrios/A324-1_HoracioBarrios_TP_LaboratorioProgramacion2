using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class IdDelMeseroBuscadoAlAsignarMesaAMeseroException : Exception
    {

        public IdDelMeseroBuscadoAlAsignarMesaAMeseroException() { }
        public IdDelMeseroBuscadoAlAsignarMesaAMeseroException(string message) : base(message) { }
        public IdDelMeseroBuscadoAlAsignarMesaAMeseroException(string message, Exception innerExcetion): base(message, innerExcetion) { }   
    }
}
