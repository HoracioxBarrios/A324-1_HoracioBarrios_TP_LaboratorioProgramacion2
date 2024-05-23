using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Enumerables;

namespace Entidades
{
    public class Mesero : Empleado
    {
        public Mesero(string nombre, string apellido, ERolEmpleado rol) : base(nombre, apellido, rol)
        {
        }
    }
}
