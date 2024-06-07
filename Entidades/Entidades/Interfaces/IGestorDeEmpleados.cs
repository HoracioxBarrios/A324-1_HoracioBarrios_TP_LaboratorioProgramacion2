using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IGestorDeEmpleados
    {

        void CrearEmpleado(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario);
        List<IEmpleado> GetEmpleados();
        IEmpleado GetEmpleado(string nombre);



    }
}
