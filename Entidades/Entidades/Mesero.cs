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
    public class Mesero : Empleado, ICobrador, IMesero
    {
        private decimal _montoAcumulado = 0;
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
        public void AgregarMesa(IMesa mesa)
        {
            _mesasAsignada.Add(mesa);
        }


        public void CobrarMesa(int idMesa)
        {
            foreach(IMesa mesa in _mesasAsignada) 
            { 
                if(mesa.Id == idMesa)
                {
                    List<IPedido> pedidosDeLaMesa = mesa.ObtenerPedidosDeLaMesa();
                    foreach(IPedido pedido in pedidosDeLaMesa)
                    {
                        _montoAcumulado = pedido.CalcularPrecio();
                    }
                }
            
            }
        }

        public void CerrarMesa(int idMesa)
        {
            throw new NotImplementedException();
        }




        public List<IMesa> MesasAsignada 
        { 
            get { return _mesasAsignada; }
            set { _mesasAsignada = value;}
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
