using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IEmpleado
    {
        string Nombre { get; set; }
        string Apellido { get; set; }
        string Contacto { get; set; }
        ERol Rol { get; set; }
        string Direccion { get; set; }
        decimal Salario { get; set; }
        string Password {  get; set; }
        int Id { get; set; }
        EStatus Status { get; set; }

    }
}
