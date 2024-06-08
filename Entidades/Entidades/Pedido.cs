using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{//ver los pedidos para local, y para delivery
    public class Pedido : IPedido
    {
        private List<IConsumible> _listaConLoPedido;

        private ETipoDePedido _tipoDePedido;    
        public ETipoDePedido TipoDePedido { get ; set ; }
        public Pedido() { }


        public void Agregar() { }

        //debe haber un editar pedido()
        public void Quitar() { }

    }
}
