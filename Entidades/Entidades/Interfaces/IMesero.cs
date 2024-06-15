using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IMesero
    {
        int Id { get; }
        List<IMesa> MesasAsignada { get; set; }
        void AgregarMesa(IMesa mesa);
        void CobrarMesa(int idMesa);

        void CerrarMesa(int idMesa);
    }
}
