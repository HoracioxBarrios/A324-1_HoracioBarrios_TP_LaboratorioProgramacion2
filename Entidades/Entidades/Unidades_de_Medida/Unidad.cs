using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Unidades_de_Medida
{
    public class Unidad : IUnidadDeMedida
    {
        public double Cantidad { get ; set ; }

        public Unidad(double cantidad) 
        { 
            Cantidad = cantidad;
        }
    }
}
