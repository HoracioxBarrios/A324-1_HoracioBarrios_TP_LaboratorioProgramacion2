using Entidades.Excepciones;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Unidades_de_Medida
{
    public class Gramo : IUnidadDeMedida
    {
        public double Cantidad { get ; set ; }


        public Gramo(double cantidad)
        {
            Cantidad = cantidad;
        }


        /// <summary>
        /// Operador de Conversion Explicito de Gramos a Kilos
        /// </summary>
        /// <param name="gramos">recibe un obj. Gramo</param>
        /// <returns>Devuelve el  Kilo con su respectiva Cantidad dentro</returns>
        /// <exception cref="AlConvertirException"></exception>
        
        public static explicit operator Kilo(Gramo gramos)
        {
            if (gramos.Cantidad > 0)
            {
                double cantidadConvertida = gramos.Cantidad / 1000;
                return new Kilo(cantidadConvertida);
            }
            throw new AlCastearException("Error al querer Convertir de Gramos a Kilo");
        }

        /// <summary>
        /// Sobrecarga Operador '+': suma la Cantidad que tiene un obj. Gramo, con otro obj Gramo
        /// </summary>
        /// <param name="gramo1">recibe un obj Gramo</param>
        /// <param name="gramo2">recibe otro obj. Gramo</param>
        /// <returns>Devuelve el nuevo Gramo con su respectva Cantidad</returns>
        public static Gramo operator +(Gramo gramo1, Gramo gramo2)
        {

            double nuevaCantidad = gramo1.Cantidad + gramo2.Cantidad;
            if(nuevaCantidad < 0)
            {
                throw new AlSumarException("Error al querer sumar, el resultado da negativo");
            }
            return new Gramo(nuevaCantidad);
        }


        /// <summary>
        /// Sobrecarga Operador '-': Resta la Cantidad entre un obj. Gramo y otro Obj Gramo
        /// </summary>
        /// <param name="gramo1">recibe un: obj. gramo</param>
        /// <param name="gramo2">recibe otro: obj. gramo</param>
        /// <returns>Devuelve el nuevo Gramo con su respectva Cantidad</returns>
        /// <exception cref="AlRestarException"></exception>
        public static Gramo operator -(Gramo gramo1, Gramo gramo2)
        {
            double nuevaCantidad = gramo1.Cantidad - gramo2.Cantidad;           
            if (nuevaCantidad < 0)
            {
                throw new AlRestarException("La resta da resultado negativo de gramos.");
            }
            return new Gramo(nuevaCantidad);
        }




        /// <summary>
        /// Sobrecarga Operador '+': suma Cantidades entre los tipo Gramo y kilos 
        /// </summary>
        /// <param name="gramo">recibe un obj. gramo</param>
        /// <param name="kilo">recibe un obj. kilo</param>
        /// <returns>Devuelve el nuevo Gramo </returns>
        public static Gramo operator +(Gramo gramo, Kilo kilo)
        {
            Gramo nuevoGramo = (Gramo)kilo;
            double nuevaCantidad = gramo.Cantidad + nuevoGramo.Cantidad;
            if (nuevaCantidad < 0)
            {
                throw new AlSumarException("Error al querer sumar, el resultado da negativo");
            }
            return new Gramo(nuevaCantidad);
        }




        /// <summary>
        /// Sobrecarga Operador '-': suma Cantidadeds entre Gramo y kilos 
        /// </summary>
        /// <param name="gramo">recibe un obj. Gramo</param>
        /// <param name="kilo">recibe otro obj. Kilo</param>
        /// <returns>Devuelve el nuevo Gramo con su respectva Cantidad</returns>
        /// <exception cref="AlRestarException"></exception>
        public static Gramo operator -(Gramo gramo, Kilo kilo)
        {
            Gramo nuevoGramo = (Gramo)kilo;
            double nuevaCantidad = gramo.Cantidad - nuevoGramo.Cantidad;
            if (nuevaCantidad < 0)
            {
                throw new AlRestarException("La resta da resultado negativo de gramos.");
            }
            return new Gramo(nuevaCantidad);
        }

    }
}
