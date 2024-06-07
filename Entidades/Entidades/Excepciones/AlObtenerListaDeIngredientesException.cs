using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlObtenerListaDeIngredientesException : Exception
    {
        public AlObtenerListaDeIngredientesException() { }

        public AlObtenerListaDeIngredientesException(string message) : base(message) { }


        public AlObtenerListaDeIngredientesException(string message, Exception innerException) : base(message, innerException) { }
    }
}
