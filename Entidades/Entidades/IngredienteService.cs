using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class IngredienteService
    {
        public static IConsumible ObtenerIngredienteParaPlato(List<IConsumible> ingredientes, string nombreIngrediente, double cantidad, EUnidadDeMedida unidadDeMedida)
        {
            
            Ingrediente ingredienteSeleccionado = (Ingrediente)ingredientes.Find(ingrediente => ingrediente.Nombre.Equals(nombreIngrediente, StringComparison.OrdinalIgnoreCase));

            if (ingredienteSeleccionado == null)
            {
                throw new ArgumentException($"Ingrediente con nombre '{nombreIngrediente}' no encontrado en la lista.");
            }

            
            return ingredienteSeleccionado.CrearCopiaConCantidadNueva(cantidad, unidadDeMedida);// Creamos una copia del ingrediente con la nueva cantidad y unidad de medida
        }
    }
}
