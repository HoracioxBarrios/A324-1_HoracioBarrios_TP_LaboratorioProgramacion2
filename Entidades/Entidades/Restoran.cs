using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Restoran
    {
        private List<IEmpleado> _listEmpleados;
        private List<IConsumible> _listConsumibles;
        private List<IProducto> _listaProductos;

        public Restoran(IEmpleado empleado)
        {
            _listEmpleados.Add(empleado);

        }



    }
}
