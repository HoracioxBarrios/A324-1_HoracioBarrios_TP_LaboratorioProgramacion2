using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Enumerables
{
    /// <summary>
    /// Emun que representa el estado de la mesa:
    /// Cerrada = Disponible /
    /// Consumo_No_Pagado = No disponible
    /// </summary>
    public enum EStateMesa
    {
        Cerrada,
        Consumo_No_Pagado,
        
    }
}
