using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IGestorDeProveedores
    {

        void CrearProveedor(
                string nombre, string cuit, string direccion, ETipoDeProducto tipoDeProducto
               , EMediosDePago mediosDePago, EAcreedor esAcreedor, EDiaDeLaSemana diaDeEntrega);


        IProveedor ObtenerProveedor(string nombre);
        IProveedor ObtenerProveedor(int id);
        List<IProveedor> ObtenerProveedores();

        void EditarProveedor(int id, string nombre);
        void EliminarProveedor(int id);
    }
}
