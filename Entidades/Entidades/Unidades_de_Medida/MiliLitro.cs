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
            throw new AlConvertirException("Error al querer convertir de miliLitro a Litro");
        }   


        public static MiliLitro operator +(MiliLitro miliLitro, MiliLitro miliLitro2)
        {
            double nuevaCantidad = miliLitro.Cantidad + miliLitro2.Cantidad;
            if(nuevaCantidad < 0)
            {
                throw new AlSumarException("Error al sumar, el Resultado da negativo");
            }
            return new MiliLitro(nuevaCantidad);
        }
        public static MiliLitro operator -(MiliLitro miliLitro, MiliLitro miliLitro2)
        {
            double nuevaCantidad = miliLitro.Cantidad - miliLitro2.Cantidad;
            if (nuevaCantidad < 0)
            {
                throw new AlRestarException("Error al sumar, el Resultado da negativo");
            }
            return new MiliLitro(nuevaCantidad);
        }
        public static MiliLitro operator +(MiliLitro miliLitro, Litro litro)
        {
            MiliLitro nuevoMilitro = (MiliLitro)litro;
            double nuevaCantidad = miliLitro.Cantidad + nuevoMilitro.Cantidad;
            if(nuevaCantidad < 0)
            {
                throw new AlSumarException("Error al sumar , la cantidad da negativa");
            }
            return new MiliLitro(nuevaCantidad);
        }
        public static MiliLitro operator -(MiliLitro miliLitro, Litro litro)
        {
            MiliLitro nuevoMilitro = (MiliLitro)litro;
            double nuevaCantidad = miliLitro.Cantidad - nuevoMilitro.Cantidad;
            if (nuevaCantidad < 0)
            {
                throw new AlSumarException("Error al Restar , la cantidad da negativa");
            }
            return new MiliLitro(nuevaCantidad);
        }


    }
}
