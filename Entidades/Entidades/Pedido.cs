﻿using Entidades.Enumerables;
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
        private int _id;
        private List<IConsumible> _consumiblesPedidos; // Bebidas o Platos
        private decimal _precioDeloPedido;

        private ETipoDePedido _tipoDePedido;//Para Local o Para Delivery     
        private bool _isEntregable;


        private Pedido() 
        {
            _isEntregable = false;
        }
        public Pedido(ETipoDePedido tipoDePedido, List<IConsumible> consumiblesPedidos) :this()
        {
            _tipoDePedido = tipoDePedido;
            _consumiblesPedidos = consumiblesPedidos;
        }




        /// <summary>
        /// Verifica SiEl Pedido esta Listo para la entrega, si hay uno de los que integran el pedido que no esta  disponible, el pedido NO esta disponible (Todos deben ser disponibles Bebidas y Comidas)
        /// </summary>
        /// <returns> True si ya se puede entregar</returns>
        public bool VerificarSiEsEntregable() // estaria bueno que sea evaluable en base a un evento
        {
            _isEntregable = true;
            foreach(IConsumible consumible in _consumiblesPedidos)
            {
                if(consumible.Disponibilidad == false)
                {
                    _isEntregable = false;
                    break;
                }
            }
            return _isEntregable;
        }

        public decimal CalcularPrecio()
        {
            _precioDeloPedido = 0;
            foreach(IConsumible consumible in _consumiblesPedidos)
            {
                _precioDeloPedido += consumible.CalcularPrecio();
            }
            return _precioDeloPedido;
        }

        public void AgregarConsumible(IConsumible consumible) 
        {
            _consumiblesPedidos.Add(consumible);
        }

        public void EditarConsumible(IConsumible consumibleConLaCantidadCorregida)
        {
            for (int i = 0; i < _consumiblesPedidos.Count; i++)
            {
                if (_consumiblesPedidos[i].Nombre == consumibleConLaCantidadCorregida.Nombre)
                {
                    _consumiblesPedidos[i] = consumibleConLaCantidadCorregida;
                    break;
                }
            }
        }

        public void EliminarConsumible(IConsumible consumible)
        {
            for (int i = 0; i < _consumiblesPedidos.Count; i++)
            {
                if (_consumiblesPedidos[i].Nombre == consumible.Nombre)
                {
                    _consumiblesPedidos.RemoveAt(i);
                    break; 
                }
            }
        }


        public ETipoDePedido TipoDePedido 
        {          
            get { return _tipoDePedido;}
            set {  _tipoDePedido = value;}
        }
        public int Id
        {
            get { return _id;}
            set { _id = value;}
        }

    }
}
