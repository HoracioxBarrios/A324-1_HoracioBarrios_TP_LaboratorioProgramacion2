using Entidades.Excepciones;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Unidades_de_Medida
{
    public class Litro : ITipoUnidadDeMedida
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
            throw new AlConvertirException("Error al querer convertir de Litro a MiliLitros");
        }

        public static Litro operator +(Litro litro, Litro litro2)
        {
            double nuevaCantidad = litro.Cantidad + litro2.Cantidad;
            if(nuevaCantidad > 0)
            {
                return new Litro(nuevaCantidad);
            }
            throw new AlSumarException("Error al sumar, el Resultado da negativo");
        }

        public static Litro operator - (Litro litro,  Litro litro2)
        {
            double nuevaCantidad = litro.Cantidad - litro2.Cantidad;
            if(nuevaCantidad < 0)
            {
                throw new AlRestarException("Error, el resultado de la Resta es Negativo");
            }
            return new Litro (nuevaCantidad);
        }

        public static Litro operator +(Litro litro, MiliLitro miliLitro)
        {
            Litro nuevoLitro = (Litro)miliLitro;
            double nuevaCantidad = litro.Cantidad + nuevoLitro.Cantidad;
            if( nuevaCantidad > 0)
            {
                return new Litro(nuevaCantidad);
            }
            throw new AlSumarException ("Error al querer sumar, el resultado da negativo");
        }
        public static Litro operator -(Litro litro, MiliLitro miliLitro)
        {
            Litro nuevoLitro = ( Litro)miliLitro;
            double nuevaCantidad = litro.Cantidad - nuevoLitro .Cantidad;
            if(nuevaCantidad < 0 )
            {
                throw new AlRestarException("Error al restar, el resultado da Negativo");
            }
            return new Litro(nuevaCantidad);
        }
    }
}
