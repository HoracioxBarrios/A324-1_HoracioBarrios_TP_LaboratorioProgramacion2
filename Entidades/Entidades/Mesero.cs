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

            _montoAcumulado = 0;

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
            foreach (Mesa mesa in _mesasAsignada)
            {
                if (mesa.Id == idMesa)
                {
                    mesa.AgregarPedidoAMesa(pedido);
                    pedido.Entregado = true;
                    //Descontar del stock
                    break;
                }
            }
            throw new AlEntregarPedidoException("Error al entregar Pedido a la mesa");
        }

        public void Cobrar(int idMesaOCliente)
        {
            decimal montoMesaActual = 0;
            foreach(IMesa mesa in _mesasAsignada) 
            { 
                if(mesa.Id == idMesaOCliente)
                {
                    List<IPedido> pedidosDeLaMesa = mesa.ObtenerPedidosDeLaMesa();
                    foreach(IPedido pedido in pedidosDeLaMesa)
                    {
                        if(pedido.Entregado == true)
                        {
                            _montoAcumulado += pedido.CalcularPrecio();
                        }                                        

                    }
                    CerrarMesa(idMesaOCliente);
                    break;
                }
                
            }
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
        }

        public List<IMesa> MesasAsignada { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
