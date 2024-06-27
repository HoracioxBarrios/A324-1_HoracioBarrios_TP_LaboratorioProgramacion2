using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class ErrorAlBuscarMesaEnListaEnGestorDeMesas :Exception
    {
        public ErrorAlBuscarMesaEnListaEnGestorDeMesas() { }

        public ErrorAlBuscarMesaEnListaEnGestorDeMesas(string message) : base(message) { }
        public ErrorAlBuscarMesaEnListaEnGestorDeMesas(string message, Exception innerException) : base(message, innerException) { }
    }
}
