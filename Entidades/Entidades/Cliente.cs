using Entidades.Excepciones;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cliente :ICliente
    {
        private int _id;
        private string _nombre;
        private string _direccion;
        private string _telefono;
        private List<IPedido> _pedidos;
        private int _idDelDelivery;
        public Cliente(int id, string nombre, string direccion, string telefono) 
        {
            _id = id;
            _nombre = nombre;
            _direccion = direccion;
            _telefono = telefono;
            _pedidos = new List<IPedido>();
        }

        public void AgregarPedidoACliente(IPedido pedido)
        {
            _pedidos.Add(pedido);
        }


        public List<IPedido> ObtenerPedidosDeLCliente()
        {
            if (_pedidos.Count > 0)
            {
                return _pedidos;
            }
            throw new AlObtenerPedidosDeLaMesaException("Error la lista de pedidos esta vacia");
        }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Direccion { get => _direccion; set => _direccion = value; }
        public string Telefono { get => _telefono; set => _telefono = value; }
        public int Id { get => _id; set => _id = value; }

        public int IdDelDelivery
        {
            get { return _idDelDelivery; }
            set { _idDelDelivery = value; }
        }
    }
}
