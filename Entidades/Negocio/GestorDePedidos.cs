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
        private Queue<IPedido> _pedidosParaLocal; // Los pedidos deben salir en orden en que ingresan
        private Queue<IPedido> _pedidosParaDelivery;
        private IGestorProductos _gestorProductosStock;

        private List<IConsumible> _historialDeConsumibles;


        public GestorDePedidos(IGestorProductos gestorProductosStock)
        {
            _pedidosParaLocal = new Queue<IPedido>();
            _pedidosParaDelivery = new Queue<IPedido>();
            _gestorProductosStock = gestorProductosStock;
            _historialDeConsumibles = new List<IConsumible>();
        }
    



        /// <summary>
        /// Crea un pedido y lo agrega a la lista de pedidos (en Gestor Pedidos) - Crean pedidos Encargado (Para delivery) o con Mesero para (Local)
        /// </summary>
        /// <param name="menu"></param>
        public bool CrearPedido(ICreadorDePedidos creadorDePedidos, ETipoDePedido tipoDePedido, List<IConsumible> consumiblesPedidos, int IdDeLaMesaOCliente)
        {
            bool seCreo = false;
            IPedido pedido = creadorDePedidos.CrearPedido(tipoDePedido, consumiblesPedidos, IdDeLaMesaOCliente);
            if(pedido != null && pedido.TipoDePedido == ETipoDePedido.Para_Local)
            {
                seCreo = true;
                _pedidosParaLocal.Enqueue(pedido);
                AgregarConsumiblesAlHistorial(consumiblesPedidos);
                SuscribirEventoListoParaEntregar(pedido); // se suscribe al evento 
            }
            if (pedido != null && pedido.TipoDePedido == ETipoDePedido.Para_Delivery)
            {
                seCreo = true;
                _pedidosParaDelivery.Enqueue(pedido);
                AgregarConsumiblesAlHistorial(consumiblesPedidos);
                SuscribirEventoListoParaEntregar(pedido); // se suscribe al evento 
            }

            return seCreo;
        }





        public bool EditarPedido(IEditorDePedidos editorDePedidos, int id, List<IConsumible> listaActualizadaDeConsumiblesParaElPedido)
        {
            bool seEdito = false;

            
            foreach (IPedido pedido in _pedidosParaLocal)// intentamos editar el pedido en la cola de pedidos para local
            {
                if (pedido.Id == id)
                {
                    editorDePedidos.EditarPedido(id, _pedidosParaLocal, listaActualizadaDeConsumiblesParaElPedido);
                    seEdito = true;
                    break;
                }
            }

            
            if (!seEdito)
            {
                foreach (IPedido pedido in _pedidosParaDelivery)// si no se encontró en la cola de pedidos para local, intenta en la cola de pedidos para delivery
                {
                    if (pedido.Id == id)
                    {
                        editorDePedidos.EditarPedido(id, _pedidosParaDelivery, listaActualizadaDeConsumiblesParaElPedido);
                        seEdito = true;
                        break;
                    }
                }
            }

            return seEdito;
        }







        public bool EliminarPedido(IEliminadorDePedidos eliminadorDePedidos, int id)
        {
            bool seElimino = false;

            
            foreach (IPedido pedido in _pedidosParaLocal)// Intenta eliminar el pedido de la cola de pedidos para local
            {
                if (pedido.Id == id)
                {
                    eliminadorDePedidos.EliminarPedido(id, _pedidosParaLocal);
                    EliminarConsumiblesDelHistorial(pedido.ObtenerTodosLosConsumiblesDelPedido());
                    seElimino = true;
                    break;
                }
            }

            
            if (!seElimino)// Si no se encontró en la cola de pedidos para local, intenta en la cola de pedidos para delivery
            {
                foreach (IPedido pedido in _pedidosParaDelivery)
                {
                    if (pedido.Id == id)
                    {
                        eliminadorDePedidos.EliminarPedido(id, _pedidosParaDelivery);
                        EliminarConsumiblesDelHistorial(pedido.ObtenerTodosLosConsumiblesDelPedido());
                        seElimino = true;
                        break;
                    }
                }
            }

            return seElimino;
        }







        private void AgregarConsumiblesAlHistorial(List<IConsumible> consumibles)
        {
            if(consumibles != null)
            {
                _historialDeConsumibles.AddRange(consumibles);
            }

        }




        private void EliminarConsumiblesDelHistorial(List<IConsumible> consumibles)
        {
            foreach (var consumible in consumibles)
            {
                _historialDeConsumibles.Remove(consumible);
            }
        }





        public List<(IConsumible consumible, int cantidad)> ObtenerRankingDeConsumiblesMasPedido()         // Método para obtener ranking de consumibles (Platos o Bebidas más vendidos)
        {
            var rankingDict = new Dictionary<IConsumible, int>();

            foreach (var consumible in _historialDeConsumibles)
            {
                if (rankingDict.ContainsKey(consumible))
                {
                    rankingDict[consumible]++;
                }
                else
                {
                    rankingDict[consumible] = 1;
                }
            }

            var rankingList = new List<(IConsumible consumible, int cantidad)>();

            foreach (var entry in rankingDict)
            {
                rankingList.Add((entry.Key, entry.Value));
            }

            rankingList.Sort((x, y) => y.cantidad.CompareTo(x.cantidad));

            return rankingList;
        }

        
        public List<(IConsumible consumible, int cantidad)> ObtenerRankingDeConsumiblesMenosPedido() // Método para obtener ranking de consumibles Platos o Beidas menos vendidos)
        {
            var rankingDict = new Dictionary<IConsumible, int>();

            foreach (var consumible in _historialDeConsumibles)
            {
                if (rankingDict.ContainsKey(consumible))
                {
                    rankingDict[consumible]++;
                }
                else
                {
                    rankingDict[consumible] = 1;
                }
            }

            var rankingList = new List<(IConsumible consumible, int cantidad)>();

            foreach (var entry in rankingDict)
            {
                rankingList.Add((entry.Key, entry.Value));
            }

            rankingList.Sort((x, y) => x.cantidad.CompareTo(y.cantidad));

            return rankingList;
        }

        public List<(IConsumible consumible, int cantidad)> ObtenerTopNConsumiblesMasPedidos(int topN)
        {
            var ranking = ObtenerRankingDeConsumiblesMasPedido();
            var topNList = new List<(IConsumible consumible, int cantidad)>();

            for (int i = 0; i < topN && i < ranking.Count; i++)
            {
                topNList.Add(ranking[i]);
            }

            return topNList;
        }

        public List<(IConsumible consumible, int cantidad)> ObtenerTopNConsumiblesMenosPedido(int topN)
        {
            var ranking = ObtenerRankingDeConsumiblesMenosPedido();
            var topNList = new List<(IConsumible consumible, int cantidad)>();

            for (int i = 0; i < topN && i < ranking.Count; i++)
            {
                topNList.Add(ranking[i]);
            }

            return topNList;
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





        /// <summary>
        /// Toma el primer pedido de la cola (queue) REPRESENTA A UN PEDIDO SIN PREPARAR AUN
        /// </summary>
        /// <param name="cocinero"></param>
        public IPedido TomarPedidoSinPrepararAunParaElLocal()
        {
            if(_pedidosParaLocal.Count > 0)
            {
                IPedido pedido = _pedidosParaLocal.Peek();// Obtiene el primer pedido de la cola sin eliminarlo                
                return pedido;
            }
            throw new NoHayPedidosEnColaException("No hay Pedidos Para el Local, en Cola en el Gestor Pedidos");
        }

        /// <summary>
        /// Toma el primer pedido de la cola (queue) REPRESENTA A UN PEDIDO SIN PREPARAR AUN
        /// </summary>
        /// <param name="cocinero"></param>

        public IPedido TomarPedidoSinPrepararAunParaDelivery()
        {
            if (_pedidosParaDelivery.Count > 0)
            {
                IPedido pedido = _pedidosParaDelivery.Peek();// Obtiene el primer pedido de la cola sin eliminarlo                
                return pedido;
            }
            throw new NoHayPedidosEnColaException("No hay Pedidos Para Delivery, en Cola en el Gestor Pedidos");
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



        public IPedido ObtenerPedidoListoParaLaEntregaEnLocal()
        {
            foreach (IPedido pedido in _pedidosParaLocal)
            {
                if (pedido.ListoParaEntregar && pedido.Entregado == false)
                {
                    return pedido;
                }
            }
            throw new NoHayPedidosParaEntregarException("No hay pedidos Para entregar en la cola de pedidos para local");
        }
        public IPedido ObtenerPedidoListoParaLaEntregaDelivery()
        {
            foreach (IPedido pedido in _pedidosParaDelivery)
            {
                if (pedido.ListoParaEntregar && pedido.Entregado == false)
                {
                    return pedido;
                }
            }
            throw new NoHayPedidosParaEntregarException("No hay pedidos Para Entregar en la cola de pedidos para delivery");
        }

        public bool EntregarPedido(IEntregadorPedidos entregadorPedidos, int idDelPedido, int idDeMesaOCliente)
        {
            bool seEntregoCorrectamente = false;
            bool seDescontoCorrectamente = false;
            List<IConsumible> consumiblesDelPedido = new List<IConsumible>();
            if(entregadorPedidos is Mesero)
            {
                foreach (Pedido pedido in _pedidosParaLocal)
                {
                    if (pedido.Id == idDelPedido && pedido.ListoParaEntregar == true)
                    {
                        entregadorPedidos.EntregarPedido(idDeMesaOCliente, pedido); // se entrega por ej. a la mesa y se agrega  a una lista de pedidos para luego cobrar
                        pedido.Entregado = true;
                        consumiblesDelPedido = pedido.ObtenerTodosLosConsumiblesDelPedido();
                        seDescontoCorrectamente = _gestorProductosStock.DescontarProductosDeStock(consumiblesDelPedido);
                        break;
                    }
                }
            }
            if(entregadorPedidos is Delivery)
            {
                foreach (Pedido pedido in _pedidosParaDelivery)
                {
                    if (pedido.Id == idDelPedido && pedido.ListoParaEntregar == true)
                    {
                        entregadorPedidos.EntregarPedido(idDeMesaOCliente, pedido); // se entrega por ej. a la mesa y se agrega  a una lista de pedidos para luego cobrar
                        pedido.Entregado = true;
                        consumiblesDelPedido = pedido.ObtenerTodosLosConsumiblesDelPedido();
                        seDescontoCorrectamente = _gestorProductosStock.DescontarProductosDeStock(consumiblesDelPedido);
                        break;
                    }
                }
            }   

            if (seDescontoCorrectamente)
            {
                seEntregoCorrectamente = true;
            }
            return seEntregoCorrectamente;
        }
    }
}
