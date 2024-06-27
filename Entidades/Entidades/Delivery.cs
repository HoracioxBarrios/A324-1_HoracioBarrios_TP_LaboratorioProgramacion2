using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using Entidades.Enumerables;
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
            throw new NotImplementedException();
        }

        public void Cobrar(int idMesaOCliente)
        {
            throw new NotImplementedException();
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
