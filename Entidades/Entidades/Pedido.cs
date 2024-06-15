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
        private decimal _precioDeloPedido;
        private int _idDePedido;
        private ETipoDePedido _tipoDePedido;//Para Local o Para Delivery     
        


        public Pedido(ETipoDePedido tipoDePedido, List<IConsumible> consumibles) 
        {
            _tipoDePedido = tipoDePedido;
            _listaConLoPedido = consumibles;
        }

        public decimal CalcularPrecio()
        {
            _precioDeloPedido = 0;
            foreach(IConsumible consumible in _listaConLoPedido)
            {
                _precioDeloPedido += consumible.CalcularPrecio();
            }
            return _precioDeloPedido;
        }

        public void Agregar(IConsumible consumible) 
        { 
            _listaConLoPedido.Add(consumible);
        }

        //debe haber un editar pedido()
        public void Quitar(IConsumible consumible) 
        { 
            foreach(IConsumible consumibleEnPedido in _listaConLoPedido)
            {
                if(consumible == consumibleEnPedido)
                {
                    _listaConLoPedido.Remove(consumibleEnPedido);
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
            get { return _idDePedido;}
            set { _idDePedido = value;}
        }

    }
}
