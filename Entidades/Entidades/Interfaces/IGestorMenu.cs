using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IGestorMenu
    {
        void CrearMenu(string nombreMenu);
        void AgregarPlatoAMenu(string nombreMenu, string nombrePlato, List<IProducto> listaDeIngredientes);
        void AgregarBebidasAMenu(string nombreDelMenu, List<IProducto> listaDeBebidas);

        List<IMenu> GetListaDeMenusQueSeOfrecen();
        List<IConsumible> GetListaDeTodosLosPlatosDisponibles();
        List<IConsumible> GetListaDeTodasLasBebidasDisponibles();
        List<IConsumible> GetListaDeTodosLosPlatosNoDisponibles();
        List<IConsumible> GetListaDeTodasLasBebidasNoDisponibles();

    }
}
