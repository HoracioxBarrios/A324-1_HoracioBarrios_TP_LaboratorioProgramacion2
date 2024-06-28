using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IProducto
    {
        string Nombre { get; set; }
        double Cantidad { get; set; }
        decimal Precio { get; set; }
        ITipoUnidadDeMedida TipoDeUnidadDeMedida { get; set; }
        bool Disponibilidad { get; set; }
        int Id { get; set; }
        decimal CalcularPrecioDeCosto();

        EUnidadDeMedida UnidadDeMedida { get; set; }
        ETipoDeProducto ETipoDeProducto { get; set; }
    }
}
