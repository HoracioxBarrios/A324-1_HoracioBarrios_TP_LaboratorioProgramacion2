using Entidades.Enumerables;
using System;
using System.Collections.Generic;


namespace Entidades.Interfaces
{
    public  interface IConsumible
    {
        string Nombre { get; set; }
        decimal Precio { get; set; }
        EDisponibilidad Disponibilidad { get; set; }

        
    }
}
