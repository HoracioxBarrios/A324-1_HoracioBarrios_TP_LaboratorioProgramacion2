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
        IMenu ObtenerMenuPorNombre(string nombreDelMenu);
        IConsumible CrearPlato(string nombreDelPlato, int tiempoDePreparacion, EUnidadDeTiempo unidadDeTiempo);
        void EditarPlato(string nombrePlato, List<IConsumible> ingredientesActualizacion);
        void EliminarPlato(string nombrePlato);
        void AgregarPlatoAMenu(string nombreDelMenu, IConsumible plato);
        void AgregarPlatosAlMenu(string nombreDelMenu, List<IConsumible> listaDePlatos);
        void AgregarBebidaAlMenu(string nombreDelMenu, IConsumible consumible);
        void AgregarBebidasAMenu(string nombreDelMenu, List<IConsumible> listaDeBebidas);

        void EstablecerPrecioAProducto(IEstablecedorDePrecios establecedorDePrecios, string nombreDelPlato, decimal precioDeVentaDelPlato);
        void SelecionarIngredienteParaUnPlato(string nombreDelIngrediente, double cantidadNecesaria, EUnidadDeMedida unidadDeMedida);
        IConsumible ObtenerConsumibleBebidaOPlato(string nombreConsumible);
        List<IMenu> ObtenerTodosLosMenus();
        List<IConsumible> ObtenerPlatosDisponibles();
        List<IConsumible> ObtenerBebidasDisponibles();
        List<IConsumible> ObtenerPlatosNoDisponibles();
        List<IConsumible> ObtenerBebidasNoDisponibles();

    }
}
