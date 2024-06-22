using Entidades.Excepciones;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Unidades_de_Medida
{
    public class Unidad : ITipoUnidadDeMedida
    {
        public double Cantidad { get ; set ; }

        public Unidad(double cantidad) 
        { 
            Cantidad = cantidad;
        }

        public static explicit operator Unidad(double valor)
        {
            return new Unidad(valor);
        }

        public static Unidad operator+(Unidad unidad1, Unidad unidad2)
        {
            double nuevaCantidad = unidad1.Cantidad + unidad2.Cantidad;
            if(nuevaCantidad < 0)
            {
                throw new AlSumarException("Error al sumar, el resultado da negativo");
            }
            return new Unidad(nuevaCantidad);
        }
        public static Unidad operator -(Unidad unidad1, Unidad unidad2)
        {
            double nuevaCantidad = unidad1.Cantidad - unidad2.Cantidad;
            if (nuevaCantidad < 0)
            {
                throw new AlSumarException("Error al Restar, el resultado da negativo");
            }
            return new Unidad(nuevaCantidad);
        }


        //TESTEAR
        public static bool operator >(Unidad unidad1, Unidad unidad2)
        {
            return unidad1.Cantidad > unidad2.Cantidad;
        }

        public static bool operator <(Unidad unidad1, Unidad unidad2)
        {
            return unidad1.Cantidad < unidad2.Cantidad;
        }




    }
}
