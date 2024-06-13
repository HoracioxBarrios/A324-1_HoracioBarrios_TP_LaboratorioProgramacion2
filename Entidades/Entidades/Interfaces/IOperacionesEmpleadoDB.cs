using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IOperacionesEmpleadoDB
    {
        bool CrearTabla();
        bool Create(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario);
        IEmpleado ReadOne(int id);
        IEmpleado ReadOne(string nombre, string apellido0);
        List<IEmpleado> ReadAll();
        bool Update(int id, string password, EStatus status, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario);
        bool Delete(int id);
        bool Delete(string nombre, string apellido);
    }
}
