using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface ICocinero
    {
        IConsumible CrearPlato(string nombre, List<IConsumible> listaDeIngredientes);
        IConsumible EditarPlato();
        IConsumible EliminarPlato();
    }
}
