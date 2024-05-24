using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IGestionarStock
    {
        void ConsultarStock();
        void ConsultarStockPorAgotamiento();

        void CargarAStock();
    }
        
}
