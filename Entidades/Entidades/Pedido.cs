using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pedido : IPedido
    {
        private List<IConsumible> _listaConLoPedido;

        private ETipoDePedido _petoDePedido;    
        public ETipoDePedido TipoDePedido { get ; set ; }
        public Pedido() { }


        public void Agregar() { }

        public void Quitar() { }

    }
}
