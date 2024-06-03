using Entidades.Excepciones;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Unidades_de_Medida
{
    public class MiliLitro : IUnidadDeMedida
    {
        public double Cantidad { get ; set ; }

        public MiliLitro(double cantidad) 
        { 
            Cantidad = cantidad;
        }

        public static explicit operator Litro(MiliLitro miliLitro) 
        { 
            if(miliLitro.Cantidad > 0)
            {
                double cantidadConvertida = miliLitro.Cantidad / 1000;
                return new Litro(cantidadConvertida);
            }
            throw new ErrorAlConvertirException("Error al querer convertir de miliLitro a Litro");
        }   

    }
}
