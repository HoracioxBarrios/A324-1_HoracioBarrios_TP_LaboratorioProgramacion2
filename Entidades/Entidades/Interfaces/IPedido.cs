using Entidades.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IPedido
    {
        ETipoDePedido TipoDePedido { get; set;}
        int Id { get; set; }
        void AgregarConsumible(IConsumible consumible);
        void EditarConsumible(IConsumible consumibleConLaCantidadCorregida);
        void EliminarConsumible(IConsumible consumible);
        decimal CalcularPrecio();
        bool VerificarSiEsEntregable();


    }
}
