using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Excepciones
{
    public class AlSetearMesaAMeseroException : Exception
    {
        public AlSetearMesaAMeseroException() { }

        public AlSetearMesaAMeseroException(string mensaje) :base(mensaje){ }

        public AlSetearMesaAMeseroException(string mensaje, Exception innerEception) : base(mensaje, innerEception) { }
    }
}
