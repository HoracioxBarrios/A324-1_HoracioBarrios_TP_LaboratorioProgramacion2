using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IOperacionesDeBaseDeDatos<T>
    {
        bool CrearTabla();
        bool Create(T entidad);
        T ReadOne(int id);
        T ReadOne(string nombre, string apellido0);
        List<T> ReadAll();
        bool Update(int id, T entidad);
        bool Delete(int id);
        bool Delete(string nombre, string apellido);
    }
}
