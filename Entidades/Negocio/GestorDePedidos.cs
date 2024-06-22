using Entidades;
using Entidades.Enumerables;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class GestorDePedidos
    {
        private Queue<IPedido> _pedidos; // Los pedidos deben salir en orden en que ingresan





        public GestorDePedidos()
        {
            _pedidos = new Queue<IPedido>();
        }
    



        /// <summary>
        /// Crea un pedido y lo agrega a la lista de pedidos (en Gestor Pedidos) - Strategy con Encargado o con Mesero
        /// </summary>
        /// <param name="menu"></param>
        public bool CrearPedido(ICreadorDePedidos creadorDePedidos, ETipoDePedido tipoDePedido, List<IConsumible> consumiblesPedidos)
        {
            bool seCreo = false;
            IPedido pedido = creadorDePedidos.CrearPedido(tipoDePedido, consumiblesPedidos);
            if(pedido != null)
            {
                seCreo = true;
                _pedidos.Enqueue(pedido);
                SuscribirEventoListoParaEntregar(pedido);
            }
            return seCreo;
        }

        private void SuscribirEventoListoParaEntregar(IPedido pedido)
        {
            pedido.PedidoListoParaEntregar += PedidoParaEntregarHandler;
        }



        private void PedidoParaEntregarHandler(IPedido pedido)
        {
            // Aquí realizas las acciones que corresponden cuando un pedido está listo para entregar
            Console.WriteLine($"El pedido {pedido.Id} está listo para ser entregado.");
            // Puedes agregar lógica adicional según las necesidades de tu aplicación
        }

        public bool EditarPedido(IEditorDePedidos editorDePedidos, int id , List<IConsumible> listaActulizadaDeConsumiblesParaElPedido)
        {
            return editorDePedidos.EditarPedido(id, _pedidos, listaActulizadaDeConsumiblesParaElPedido);
            
        }

        public bool EliminarPedido(IEliminadorDePedidos ediminadorDePedidos, int id)
        {
            
            return ediminadorDePedidos.EliminarPedido(id, _pedidos);
        }

        

        /// <summary>
        /// Toma Para el Cocinero el primer pedido de la cola (queue) y el cocinero lo añade a su lista interna de platos a cocinar
        /// </summary>
        /// <param name="cocinero"></param>
        public void TomarPedido(ICocinero cocinero)
        {
            if(_pedidos.Count > 0)
            {
                IPedido pedido = _pedidos.Peek();// Obtiene el primer pedido de la cola sin eliminarlo
                cocinero.TomarPedido(pedido);
            }            
            
        }

        
    }
}
