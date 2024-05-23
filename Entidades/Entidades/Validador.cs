using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{ 
    /// <summary>
    /// Class para Validar Datos
    /// </summary>
    public static class Validador
    {


        /// <summary>
        /// Evalua si una cadena de texto es null o vacia
        /// </summary>
        /// <param name="dato">Cadena de texto a evaluar</param>
        /// <returns>true si es valido</returns>
        public static bool ValidarCadenaNoNuloOVacio(string dato)
        {
            bool esValido = false;
            if (!string.IsNullOrEmpty(dato))
            {                
                esValido = true;
            }
            return esValido;
        }


        /// <summary>
        /// Comprueba que una cadena no supere la longitud maxima
        /// </summary>
        /// <param name="dato">cadena a evaluar</param>
        /// <param name="longitudMaxima">longitud maxima permitida</param>
        /// <returns>devuelve true si esta dentro del rango </returns>
        public static bool ValidarLongitudCadea(string dato, int longitudMaxima)
        {
            bool esValido = false;
            if(dato.Length <= longitudMaxima)
            {
                esValido = true;
            }
            return esValido;
        }
    }
}
