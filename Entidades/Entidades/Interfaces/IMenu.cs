using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IMenu
    {
        string Nombre { get; set; }
        void Agregar(IConsumible consumible);

        void Quitar(IConsumible consumible);
    }
}
