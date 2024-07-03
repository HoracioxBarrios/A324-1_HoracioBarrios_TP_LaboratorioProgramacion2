using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;

namespace Entidades
{//meseri debe poder ser pasado a la entidad gestor pedidos y crear los pedidos que luego vera en la cocina para crear los platos(debe tener tiempo de preparacion)
    public class Mesero : Empleado, ICobrador, IMesero, ICreadorDePedidos, IEntregadorPedidos
    {
        private decimal _montoDelPedidoActualTemporal;//Hasta que se realiza el pago
        private decimal _montoAcumulado;
        private List<IMesa> _mesasAsignada;



        public Mesero(ERol rol, string nombre, string apellido, string contacto,string direccion, decimal salario): base(
            rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Contacto = contacto;            
            this.Direccion = direccion;
            this.Salario = salario;
            this.Rol = rol;

            _mesasAsignada = new List<IMesa>();
            _montoAcumulado = 0;
            _montoDelPedidoActualTemporal = 0;
        }
        public Mesero(int id, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : this(
            rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Id = id;
        }
        public Mesero(int id, string password, EStatus status, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : this(
            id, rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Password = password;
            this.Status = status;
        }






        /// <summary>
        /// agrega la mesa que va a atender
        /// </summary>
        /// <param name="mesa"></param>
        public void RecibirMesa(IMesa mesa)
        {
            _mesasAsignada.Add(mesa);
        }

     

        public void EntregarPedido(int idMesa, IPedido pedido)
        {
            bool mesaEncontrada = false;

            foreach (Mesa mesa in _mesasAsignada)
            {
                if (mesa.Id == idMesa)
                {
                    
                    mesa.AgregarPedidoAMesa(pedido);
                    pedido.Entregado = true;
                    mesaEncontrada = true;
                    break;
                }
            }

            if (!mesaEncontrada)
            {
                throw new AlEntregarPedidoException("Error Mesa no encontrada (al entregar Pedido a la mesa)");
            }
        }




        public IPago Cobrar(int idMesaOCliente, ETipoDePago tipoPago)
        {
            bool seCobro = false;
            IPago pago = null;

            foreach (IMesa mesa in _mesasAsignada)
            {
                if (mesa.Id == idMesaOCliente)
                {
                    List<IPedido> pedidosDeLaMesa = mesa.ObtenerPedidosDeLaMesa();
                    foreach (IPedido pedido in pedidosDeLaMesa)
                    {
                        if (pedido.Entregado)
                        {
                            _montoDelPedidoActualTemporal += pedido.CalcularPrecio();
                        }
                    }

                    pago = RegistrarPago(idMesaOCliente, _montoDelPedidoActualTemporal, tipoPago);
                    seCobro = true;
                    CerrarMesa(idMesaOCliente);

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
            return new Pago(idMesaOCliente, this.Id,this.Rol, monto, tipoPago);
        }

        private void CerrarMesa(int idMesa)
        {
            for (int i = 0; i < _mesasAsignada.Count; i++)
            {
                if (_mesasAsignada[i].Id == idMesa)
                {
                    _mesasAsignada[i].Cerrar();
                    break;
                }            
            }

        }


        public IPedido CrearPedido(ETipoDePedido tipoDePedido, List<IConsumible> ConsumiblesParaElPedido, int IdDeLaMesaOCliente)
        {
            return new Pedido(tipoDePedido, ConsumiblesParaElPedido, IdDeLaMesaOCliente);
        }





        public decimal MontoAcumulado
        {
            get
            {
                return _montoAcumulado;
            }
            set
            {
                if(value > 0)
                {

                    _montoAcumulado = value;
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

        public List<IMesa> MesasAsignada
        {
            get { return _mesasAsignada; }
            set
            {
                _mesasAsignada = value;
            }
        }
    }
}
