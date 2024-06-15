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
        private List<IConsumible> _listaDeConsumiblesConLoPedido;
        private decimal _precioDeloPedido;
        private int _id;
        private ETipoDePedido _tipoDePedido;//Para Local o Para Delivery     
        


        public Pedido(ETipoDePedido tipoDePedido, List<IConsumible> consumiblesParaElPedido) 
        {
            _tipoDePedido = tipoDePedido;
            _listaDeConsumiblesConLoPedido = consumiblesParaElPedido;
        }

        public decimal CalcularPrecio()
        {
            _precioDeloPedido = 0;
            foreach(IConsumible consumible in _listaDeConsumiblesConLoPedido)
            {
                _precioDeloPedido += consumible.CalcularPrecio();
            }
            return _precioDeloPedido;
        }

        public void Agregar(IConsumible consumibleParaPedido) 
        { 
            _listaDeConsumiblesConLoPedido.Add(consumibleParaPedido);
        }

        public void Editar(IConsumible consumibleConLaCantidadCorregida)
        {
            for (int i = 0; i < _listaDeConsumiblesConLoPedido.Count; i++)
            {
                if (_listaDeConsumiblesConLoPedido[i].Nombre == consumibleConLaCantidadCorregida.Nombre)
                {
                    _listaDeConsumiblesConLoPedido[i] = consumibleConLaCantidadCorregida;
                    break;
                }
            }
        }


        public void Quitar(IConsumible consumible)
        {
            for (int i = 0; i < _listaDeConsumiblesConLoPedido.Count; i++)
            {
                if (_listaDeConsumiblesConLoPedido[i].Nombre == consumible.Nombre)
                {
                    _listaDeConsumiblesConLoPedido.RemoveAt(i);
                    break; 
                }
            }
        }


        public ETipoDePedido TipoDePedido 
        {          
            get { return _tipoDePedido;}
            set {  _tipoDePedido = value;}
        }
        public int ID
        {
            get { return _id;}
            set { _id = value;}
        }

    }
}
