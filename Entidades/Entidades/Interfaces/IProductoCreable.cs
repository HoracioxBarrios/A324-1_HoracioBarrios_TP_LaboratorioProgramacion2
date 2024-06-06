using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IProductoCreable
    {

        ETipoDeProducto ETipoDeProducto { get; set; }
        EUnidadMedida EUnidadDeMedida { get; set; }
        IProveedor Proveedor { get; set; }
    }
}
