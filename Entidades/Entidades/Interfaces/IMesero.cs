﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IMesero
    {
        int Id { get; }
        string Nombre { get; set; }
        string Apellido { get; set; }
        decimal MontoAcumulado { get; set; }
        List<IMesa> MesasAsignada { get; set; }

        void RecibirMesa(IMesa mesa);   
    }
}
