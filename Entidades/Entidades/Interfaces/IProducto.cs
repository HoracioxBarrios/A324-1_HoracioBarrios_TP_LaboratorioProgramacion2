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
        EUnidadMedida UnidadDeMedida { get; set; }        
        ETipoProductoCreable TipoDeProducto { get; set; }
        IProveedor Proveedor { get; set; }
        decimal Precio { get; set; }
        int Id { get; set; }
        bool Disponibilidad  { get; set;}

        
    }
}
