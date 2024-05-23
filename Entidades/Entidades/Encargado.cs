using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Enumerables;

namespace Entidades
{
    public class Encargado : Empleado
    {
        public Encargado(string nombre, string apellido,ERolEmpleado rol) : base(nombre, apellido, rol)
        {
        }
    }
}
