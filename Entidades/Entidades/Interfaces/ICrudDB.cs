using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IEmpleadoCrud
    {
        bool Create(IEmpleado empleado);

        IEmpleado ReadOne(int idDelEmpleado);

        List<IEmpleado> ReadAll();


        bool Update(int id, IEmpleado empleado);

        bool Delete(int id);

    }
}
