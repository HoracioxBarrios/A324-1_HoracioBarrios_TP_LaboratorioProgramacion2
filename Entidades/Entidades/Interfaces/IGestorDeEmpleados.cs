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

        bool CrearEmpleado(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario);

        IEmpleado ReadOneEmpleado(int id);
        IEmpleado ReadOneEmpleado(string nombre, string apellido);
        void EditarEmpleado(int id, string password);
        void EditarEmpleado(int id, string nombre, string apellido);
        void EditarEmpleado(int id, decimal salario);
        void EliminarEmpleado(int id);
        List<IEmpleado> GetEmpleadosEnList();
        IEmpleado GetEmpleadoEnList(string nombre);



    }
}
