using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{

    public delegate void StockDeProductosActualizoDelegate();


    public interface IGestorProductos
    {


        IProducto CrearProducto(
             ETipoDeProducto tipoProducto, string nombre, double cantidad, EUnidadDeMedida unidadDeMedida,
             decimal precio, IProveedor proveedor, ECategoriaConsumible categoria = default,
             EClasificacionBebida clasificacionDeBebida = default);
 
        void EditarProducto(string nombre, string correccionDelNombre);
        void EliminarProducto(string nombre);
        void AgregarProductoAStock(IProducto producto);
        List<IConsumible> ObtenerTodosLosProductosIngrediente();
        List<IConsumible> OtenerTodosLosProductosBebidas();
        List<IProducto> ObtenerTodosLosProductos();

        bool DescontarProductosDeStock(List<IConsumible> listaDeIngredienteEnElPlato);

        event StockDeProductosActualizoDelegate EventStockDeProductosActualizados;
    }
}
