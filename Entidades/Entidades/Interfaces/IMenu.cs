using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IMenu
    {
        string Nombre { get; set; }
        void Agregar(IConsumible consumible);
        void Quitar(IConsumible consumible);
        IConsumible ObtenerPlatoPorNombre(string nombreDelPlato);
        IConsumible ObtenerBebidaPorNombre(string nombreDeLaBebida, int cantidadRequerida);
        List<IConsumible> ObtenerPlatosEnMenu();

        List<IConsumible> ObtenerBebidasEnMenu();

        List<IConsumible> ObtenerTodosLosConsumiblesEnMenu();
    }
}
