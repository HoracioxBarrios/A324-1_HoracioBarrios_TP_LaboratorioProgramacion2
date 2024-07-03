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

        private decimal _montoDelPedidoActualTemporal;//Hasta que se realiza el pago
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
            _montoDelPedidoActualTemporal = 0;
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
        /// Cobra el pedido en base al Precio de Venta de los Productos (IConsumibles o IVendibles --- Bebidas y Platos ----)
        /// </summary>
        /// <param name="pedido"></param>
        public IPago Cobrar(int idDelCliente, ETipoDePago tipoDePago)
        {
            bool seCobro = false;
            IPago pago = null;
            foreach (Cliente cliente in _clientes)
            {
                if( cliente.Id == idDelCliente)
                {
                    List<IPedido> pedidosDelCliente = cliente.ObtenerPedidosDeLCliente();
                    foreach(Pedido pedido in pedidosDelCliente)
                    {
                        _montoDelPedidoActualTemporal += pedido.CalcularPrecio();
                    }
                    seCobro = true;
                    pago = RegistrarPago(cliente.Id, _montoDelPedidoActualTemporal, tipoDePago);

                    _montoAcumulado = _montoDelPedidoActualTemporal; // se guarda en el acumulador General
                    _montoDelPedidoActualTemporal = 0; //con el pago hecho, se limpia el acumulador para el proximo pedido.
                    break;
                }
            }

            if (!seCobro)
            {
                throw new AlCobrarException("Error al cobrar mesa");
            }
            return pago;
        }

        private IPago RegistrarPago(int idMesaOCliente, decimal monto, ETipoDePago tipoPago)
        {
            return new Pago(idMesaOCliente, this.Id, this.Rol, monto, tipoPago);
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


        /// <summary>
        /// Va a tener temporalmente el precio del pedido actual, hasta que se Pague el pedido
        /// </summary>
        public decimal MontoDeLPedidoActualTemporal
        {
            get { return _montoDelPedidoActualTemporal; }
            set { _montoDelPedidoActualTemporal = value; }
        }
    }
}
