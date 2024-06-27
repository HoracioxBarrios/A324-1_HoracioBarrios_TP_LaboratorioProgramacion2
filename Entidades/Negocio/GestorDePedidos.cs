using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
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
        private IGestorProductos _gestorProductosStock;




        public GestorDePedidos(IGestorProductos gestorProductosStock)
        {
            _pedidos = new Queue<IPedido>();
            _gestorProductosStock = gestorProductosStock;
        }
    



        /// <summary>
        /// Crea un pedido y lo agrega a la lista de pedidos (en Gestor Pedidos) - Strategy con Encargado o con Mesero
        /// </summary>
        /// <param name="menu"></param>
        public bool CrearPedido(ICreadorDePedidos creadorDePedidos, ETipoDePedido tipoDePedido, List<IConsumible> consumiblesPedidos, int IdDeLaMesaOCliente)
        {
            bool seCreo = false;
            IPedido pedido = creadorDePedidos.CrearPedido(tipoDePedido, consumiblesPedidos, IdDeLaMesaOCliente);
            if(pedido != null)
            {
                seCreo = true;
                _pedidos.Enqueue(pedido);
                SuscribirEventoListoParaEntregar(pedido); // se suscribe al evento 
            }
            return seCreo;
        }

        private void SuscribirEventoListoParaEntregar(IPedido pedido)
        {
            pedido.PedidoListoParaEntregar += PedidoListoParaEntregarHandler;
        }





        // Manejador del evento PedidoListoParaEntregar
        private void PedidoListoParaEntregarHandler(IPedido pedido)
        {
            
            Console.WriteLine($"El pedido {pedido.Id} está listo para ser entregado."); // SE PUEDE MOSTRAR EN PANTALLA EL PEDIDO QUE ESTA LISTO
            
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
        /// Toma el primer pedido de la cola (queue)
        /// </summary>
        /// <param name="cocinero"></param>
        public IPedido TomarPedidoPrioritario()
        {
            if(_pedidos.Count > 0)
            {
                IPedido pedido = _pedidos.Peek();// Obtiene el primer pedido de la cola sin eliminarlo                
                return pedido;
            }
            throw new NoHayPedidosEnColaException("No hay Pedidos en Cola en el Gestor Pedidos");
        }


        /// <summary>
        /// El cocinero toma el pedido (lo añade a su queue interna de platos a cocinar) y comienza a cocinarl los Platos del PEDIDO
        /// </summary>
        /// <param name="cocineroPreparadorPedido"></param>
        public async Task<bool> PrepararPedido(IPreparadorDePedidos cocineroPreparadorPedido, IPedido pedido)
        {
            cocineroPreparadorPedido.TomarPedido(pedido);
            return await cocineroPreparadorPedido.PrepararPedido();
        }



        public bool EntregarPedido(IEntregadorPedidos entregadorPedidos, int idDelPedido, int idDeMesaOCliente)
        {
            bool seEntregoCorrectamente = false;
            bool seDescontoCorrectamente = false;
            List<IConsumible> consumiblesDelPedido;
            foreach(Pedido pedido in _pedidos)
            {
                if (pedido.Id == idDelPedido && pedido.ListoParaEntregar == true)
                {
                    entregadorPedidos.EntregarPedido(idDeMesaOCliente, pedido); // se entrega por ej. a la mesa y se agrega  a una lista de pedidos para luego cobrar
                    pedido.Entregado = true;
                    consumiblesDelPedido = pedido.GetConsumibles();
                    seDescontoCorrectamente = _gestorProductosStock.DescontarProductosDeStock(consumiblesDelPedido);
                    break;
                }
            }

            if (seDescontoCorrectamente)
            {
                seEntregoCorrectamente = true;
            }
            return seEntregoCorrectamente;
        }

        public IPedido ObtenerPedidoListoParaLaEntrega()
        {
            foreach(IPedido pedido in _pedidos)
            {
                if(pedido.ListoParaEntregar && pedido.Entregado == false)
                {
                    return pedido;
                }
            }
            throw new NoHayPedidosParaEntregarException("No hay pedidos Para EntregarException en la cola");
        }
    }
}
