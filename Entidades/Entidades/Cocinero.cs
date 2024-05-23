using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Enumerables;

namespace Entidades
{
    public class Cocinero : Empleado
    {
        public Cocinero(string nombre, string apellido, ERolEmpleado rol) : base(nombre, apellido, rol)
        {
        }
    }
}
