using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface ICocinero
    {
        void SeleccionarIngredienteParaElPlato(List<IConsumible> listaDeConsumiblesEnStock, string nombreDelIngrediente, double cantidadNecesaria, EUnidadDeMedida unidadDeMedida);
        IConsumible CrearPlato(string nombreDelPlato, List<IConsumible> ingredientes, int tiempoDePreparacion, EUnidadDeTiempo unidadDeTiempo);
        IConsumible EditarPlato(IConsumible plato, List<IConsumible> ingredientesActualizacion);
        void EliminarPlato(string nombre, List<IConsumible> listaDePlatos);
        Task CocinarPlato(ICocinable plato);
        List<IConsumible> ObtenerListaDeIngredientesSeleccionadosParaPlato();

    }
}
