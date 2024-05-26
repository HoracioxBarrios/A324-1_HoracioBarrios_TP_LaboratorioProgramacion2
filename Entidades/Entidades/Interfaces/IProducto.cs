using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IProducto : IConsumible
    {
        double Cantidad { get; set; }
        Proveedor Proveedor { get; set; }

        ECategoriaConsumible Categoria { get; set; }
        int Id { get; set; }
        
    }
}
