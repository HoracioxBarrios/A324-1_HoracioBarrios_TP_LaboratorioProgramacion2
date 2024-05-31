﻿using Entidades.Enumerables;
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
        EUnidadMedida EUnidadMedida { get; set; }
        
        Proveedor Proveedor { get; set; }
        decimal Precio { get; set; }
        int Id { get; set; }        
        EDisponibilidad EDisponibilidad { get; set;}

        
    }
}