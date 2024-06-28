using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;

namespace Entidades
{    //delivery debe de poder entrar y ver los pedidos, seleccionarlo y luego de entregado marcarlo como entregado
    //entregar y cobrar interface.
    public class Delivery : Empleado, IDelivery, IEntregadorPedidos, ICobrador
    {

        private decimal _montoAcumulado;
        private List<ICliente> _clientes;
   
        public Delivery(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : base(
            rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Contacto = contacto;      
            this.Direccion = direccion;
            this.Salario = salario;
            this.Rol = rol;
            _montoAcumulado = 0;
            _clientes = new List<ICliente>();
        }


        public Delivery(int id, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : this(
            rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Id = id;
        }

        public Delivery(int id, string password, EStatus status, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : this(
            id, rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Password = password;
            this.Status = status;
        }

        public void RecibirCliente(ICliente cliente)
        {
            _clientes.Add(cliente);
        }

        public void EntregarPedido(int idCliente, IPedido pedido)
        {
            bool clienteEncontrado = false;

            foreach (Cliente cliente in _clientes)
            {
                if(cliente.Id == idCliente)
                {
                    cliente.AgregarPedidoACliente(pedido);
                    int id = cliente.Id;
                    bool seCobro = Cobrar(id);
                    pedido.Entregado = true;
                    clienteEncontrado = true;
                    break;
                }
            }

            if (!clienteEncontrado)
            {
                throw new AlEntregarPedidoException("Error no se encontro el cliente (al entregar Pedido al cliente del delivery)");
            }
        }


        /// <summary>
        /// Cobra el pedido en base al Precio de Venta de los Productos (IConsumibles o IVendibles)
        /// </summary>
        /// <param name="pedido"></param>
        public bool Cobrar(int idDelCliente)
        {
            bool seCobro = false;
            foreach(Cliente cliente in _clientes)
            {
                if( cliente.Id == idDelCliente)
                {
                    List<IPedido> pedidosDelCliente = cliente.ObtenerPedidosDeLCliente();
                    foreach(Pedido pedido in pedidosDelCliente)
                    {
                        _montoAcumulado += pedido.CalcularPrecio();
                    }
                    seCobro = true;
                    break;
                }
            }
            return seCobro;
        }



        public decimal MontoAcumulado
        {
            get
            {
                return _montoAcumulado;
            }
            set
            {
                if (value > 0)
                {
                    _montoAcumulado += value;
                }
            }
        }
    }
}
