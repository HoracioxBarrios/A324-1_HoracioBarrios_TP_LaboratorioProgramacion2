using Entidades.Enumerables;
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
        void SelecionarIngredienteParaUnPlato(string nombreDelIngrediente, double cantidadNecesaria, EUnidadDeMedida unidadDeMedida);

        IConsumible CrearPlato(string nombreDelPlato, int tiempoDePreparacion, EUnidadDeTiempo unidadDeTiempo);
        void EditarPlato(string nombrePlato, List<IConsumible> ingredientesActualizacion);

        void EliminarPlato(string nombrePlato);
        void EstablecerPrecioAProducto(IEstablecedorDePrecios establecedorDePrecios, string nombreDelPlato, decimal precioDeVentaDelPlato);

        void AgregarPlatoAMenu(string nombreDelMenu, IConsumible plato);
        void AgregarPlatosAlMenu(string nombreDelMenu, List<IConsumible> listaDePlatos);
        void AgregarBebidasAMenu(string nombreDelMenu, List<IConsumible> listaDeBebidas);

        IMenu GetMenuPorNombre(string nombreDelMenu);
        List<IMenu> GetAllMenus();
        List<IConsumible> GetPlatosDisponibles();
        List<IConsumible> GetBebidasDisponibles();
        List<IConsumible> GetPlatosNoDisponibles();
        List<IConsumible> GetBebidasNoDisponibles();

    }
}
