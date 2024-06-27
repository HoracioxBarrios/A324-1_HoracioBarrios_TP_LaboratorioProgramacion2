using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;

namespace Entidades
{
    public class Mesa : IMesa
    {
        private int _cantidadComensales;
        private EStateMesa _estado = EStateMesa.Cerrada;
        private int _idDelMesero;
        private int _id;
        private List<IPedido> _pedidos;



        private Mesa()
        {
            _cantidadComensales = 4;
            _estado = EStateMesa.Cerrada;
            _pedidos = new List<IPedido>();
        }

        public Mesa(int id, int cantidadDeComensales) : this()
        {
            _cantidadComensales = cantidadDeComensales;
            _id = id;
        }

        public Mesa(int id, int cantidadDeComensales, EStateMesa estado) : this(id, cantidadDeComensales)
        {
            _estado = estado;
        }


        public void AgregarPedidoAMesa(IPedido pedido)
        {
            _pedidos.Add(pedido);
        }

        public void AgregarPedidosAMesa(List<IPedido> pedidos)
        {
            _pedidos = pedidos;
        }

        public List<IPedido> ObtenerPedidosDeLaMesa()
        {
            if(_pedidos.Count > 0)
            {
                return _pedidos;
            }
            throw new AlObtenerPedidosDeLaMesaException("Error la lista de pedidos esta vacia");
        }
        public bool EstaMesaEstaAsignadaAMesero()
        {
            bool estaAsignada = false;
            if(_idDelMesero != 0)
            {
                estaAsignada = true;
            }
            return estaAsignada;
        }
        public void Cerrar()
        {
            _estado = EStateMesa.Cerrada;
            _pedidos.Clear();
        }

        public int IdDelMesero
        {
            get { return _idDelMesero; }
            set { _idDelMesero = value; }
        }


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int CantidadComensales
        {
            get { return _cantidadComensales; }
            set { _cantidadComensales = value; }
        }
        public EStateMesa Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
    }
}
        


     


