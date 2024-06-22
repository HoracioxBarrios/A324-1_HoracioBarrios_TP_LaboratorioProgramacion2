using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IVendible
    {
        decimal CalcularPrecioDeCosto();

        decimal GetPrecioDeCosto();

        decimal Precio { get; set; }


        bool ListoParaEntregar { get; }
    }
}
