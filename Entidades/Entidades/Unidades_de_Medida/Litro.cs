using Entidades.Excepciones;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Unidades_de_Medida
{
    public class Litro : IUnidadDeMedida
    {
        public double Cantidad { get ; set ; }

        public Litro(double cantidad)
        {
            Cantidad = cantidad;
        }

        public static explicit operator MiliLitro(Litro litro)
        {
            if(litro.Cantidad > 0)
            {
                double cantidadConvertida = litro.Cantidad * 1000;
                return new MiliLitro(cantidadConvertida);
            }
            throw new ErrorAlConvertirException("Error al querer convertir de Litro a MiliLitros");
        }
    }
}
