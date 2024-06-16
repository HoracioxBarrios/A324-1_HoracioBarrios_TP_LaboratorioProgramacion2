using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IGestorProductos
    {
        void CrearProductoParaListaDeStock(
              ETipoDeProducto tipoProducto, string nombre, double cantidad, EUnidadDeMedida unidadDeMedida
            , decimal precio, IProveedor proveedor, ECategoriaConsumible categoria = default
            , EClasificacionBebida clasificacionDeBebida = default);

        IConsumible CrearProducto(
             ETipoDeProducto tipoProducto, string nombre, double cantidad, EUnidadDeMedida unidadDeMedida,
             decimal precio, IProveedor proveedor, ECategoriaConsumible categoria = default,
             EClasificacionBebida clasificacionDeBebida = default);

        bool DescontarProductosDeStock(List<IConsumible> listaDeIngredienteEnElPlato);

        List<IConsumible> GetAllProductosIngrediente();
        List<IConsumible> GetAllProductosBebidas();

    }
}
