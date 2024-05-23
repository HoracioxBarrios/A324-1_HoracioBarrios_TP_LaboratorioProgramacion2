using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    /// <summary>
    /// Emun que representa el estado de la mesa:
    /// Cerrada = Disponible
    /// Consumo_No_Pagado = No disponible
    /// </summary>
    public enum EStateMesa
    {
        Cerrada = 0,
        Consumo_No_Pagado = 1
    }
}
