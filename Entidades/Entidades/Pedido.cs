using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{


    public delegate void PedidoListoParaEntregarEventHandler(Pedido pedido);
    public class Pedido : IPedido
    {

        private List<IConsumible> _consumiblesPedidos; // Bebidas o Platos
        private decimal _precioDeloPedido;
        private ETipoDePedido _tipoDePedido;//Para Local o Para Delivery     
        private bool _listoParaEntregar;
        private static int _contadorId = 0;
        private int _id;

        public event PedidoListoParaEntregarEventHandler PedidoListoParaEntregar;



        private Pedido() 
        {
            _listoParaEntregar = false;
            _id = ++_contadorId;
        }
        public Pedido(ETipoDePedido tipoDePedido, List<IConsumible> consumiblesPedidos) :this()
        {
            _tipoDePedido = tipoDePedido;
            _consumiblesPedidos = consumiblesPedidos ?? new List<IConsumible>(); // (el operador de coalescencia nula). Este operador se usa para devolver el valor de su operando izquierdo si no es null; de lo contrario, devuelve el operando derecho.
            SuscribirseVariosPlatosAEventoPlatoListoParaEntregar(_consumiblesPedidos);//Los consumibles pedidos son BEBIDAS O EN ESTE CASO PLATOS
        }






        /// <summary>
        /// Suscribe al evento de los platos de la lista para avisar mediante evento cuando estan listos (cocinados) para entregar en el pedido
        /// </summary>
        /// <param name="consumibles"></param>
        private void SuscribirseVariosPlatosAEventoPlatoListoParaEntregar(List<IConsumible> consumibles)
        {
            foreach (IConsumible consumible in consumibles)
            {
                if (consumible is Plato plato)
                {
                    plato.EventPlatoListo += PlatoListoHandler;
                }
            }
        }
        private void DeSuscribirseVariosPlatosAEventoPlatoListoParaEntregar(List<IConsumible> consumibles)
        {
            foreach (IConsumible consumible in consumibles)
            {
                if (consumible is Plato plato)
                {
                    plato.EventPlatoListo -= PlatoListoHandler;
                }
            }
        }

        private void SuscribirseUnPlatoAEventoPlatoListoParaEntregar(IConsumible consumible)
        {
            if (consumible is Plato plato)
            {
                plato.EventPlatoListo += PlatoListoHandler;
            }
        }

        private void DeSuscribirseUnPlatoAEventoPlatoListoParaEntregar(IConsumible consumible)
        {
            if (consumible is Plato plato)
            {
                plato.EventPlatoListo -= PlatoListoHandler;
            }
        }



        private void PlatoListoHandler(Plato plato)
        {            
            VerificarSiEsEntregable();// Actualizar el estado del pedido cuando un plato está listo
        }


        /// <summary>
        /// Comprueba si los platos del pedido tienen el estado entregable
        /// </summary>
        /// <returns></returns>
        public bool VerificarSiEsEntregable()
        {
            _listoParaEntregar = true;

            foreach (IConsumible consumible in _consumiblesPedidos)
            {
                if (consumible is IVendible vendible && !vendible.ListoParaEntregar)
                {
                    _listoParaEntregar = false;
                    break;
                }
            }

            if (_listoParaEntregar)
            {
                OnPedidoListoParaEntregar(); // Si todos los platos están listos, disparar el evento
            }

            return _listoParaEntregar;
        }

        protected virtual void OnPedidoListoParaEntregar()
        {
            PedidoListoParaEntregar?.Invoke(this); // Invoca el evento, pasando la instancia actual del pedido como argumento
        }


        public decimal CalcularPrecio()
        {
            _precioDeloPedido = 0;
            foreach(IConsumible consumible in _consumiblesPedidos)
            {
                _precioDeloPedido += consumible.CalcularPrecioDeCosto();
            }
            return _precioDeloPedido;
        }

        public void AgregarConsumible(IConsumible consumible) 
        {
            _consumiblesPedidos.Add(consumible);
            SuscribirseUnPlatoAEventoPlatoListoParaEntregar(consumible);
        }


        public void EditarConsumibles(List<IConsumible> nuevaListaDeConsumiblesCorregidos)
        {
            _consumiblesPedidos.Clear();
            _consumiblesPedidos.AddRange(nuevaListaDeConsumiblesCorregidos);
            SuscribirseVariosPlatosAEventoPlatoListoParaEntregar(_consumiblesPedidos);
        }
        public void EditarConsumible(IConsumible consumibleConLaCantidadCorregida)
        {
            for (int i = 0; i < _consumiblesPedidos.Count; i++)
            {
                if (_consumiblesPedidos[i].Nombre == consumibleConLaCantidadCorregida.Nombre)
                {
                    _consumiblesPedidos[i] = consumibleConLaCantidadCorregida;
                    SuscribirseUnPlatoAEventoPlatoListoParaEntregar(_consumiblesPedidos[i]);
                    break;
                }
            }
        }

        public void EliminarConsumible(IConsumible consumible)
        {
            for (int i = 0; i < _consumiblesPedidos.Count; i++)
            {
                if (_consumiblesPedidos[i].Nombre == consumible.Nombre)
                {
                    DeSuscribirseUnPlatoAEventoPlatoListoParaEntregar(_consumiblesPedidos[i]);
                    _consumiblesPedidos.RemoveAt(i);
                    break; 
                }
            }
        }


        public List<IConsumible> GetPlatos()
        {
            List<IConsumible> platos = new List<IConsumible>();
            if (_consumiblesPedidos.Count > 0)
            {
                foreach (IConsumible consumible in _consumiblesPedidos)
                {
                    if (consumible is Plato)
                    {
                        platos.Add(consumible);
                    }
                }
                return platos;
            }
            throw new ListaVaciaException("La Lista de los Platos del pedido está Vacia");
        }


        public List<IConsumible> GetBebidas()
        {
            List<IConsumible> bebidas = new List<IConsumible>();
            if (_consumiblesPedidos.Count > 0)
            {
                foreach (IConsumible consumible in _consumiblesPedidos)
                {
                    if (consumible is Bebida)
                    {
                        bebidas.Add(consumible);
                    }
                }
                return bebidas;
            }
            throw new ListaVaciaException("La lista de bebidas del pedido esta Vacia");
        }



        public ETipoDePedido TipoDePedido 
        {          
            get { return _tipoDePedido;}
            set {  _tipoDePedido = value;}
        }
        public int Id
        {
            get { return _id;}
            set { _id = value;}
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Pedido Numero: {Id}");
            sb.Append($"Tipo de Pedido: {TipoDePedido}");

            string estadoDelPedido = "No está Listo";
            if (VerificarSiEsEntregable())
            {
                estadoDelPedido = "Listo para la Entrega";
            }

            sb.Append($"Estado del Pedido: {estadoDelPedido}");

            sb.Append($" --------- Listado de Productos del Pedido --------");
            sb.Append($"- Platos: ");
            foreach (Plato plato in GetPlatos())
            {
                sb.AppendLine($"Nombre del Plato {plato.Nombre} ----- Precio: {plato.Precio}");
            }

            sb.Append($"- Bebidas: ");
            foreach(Bebida bebida in GetBebidas())
            {
                sb.AppendLine($"Nombre de la Bebida: {bebida.Nombre} ----- Precio: {bebida.Precio}");
            }
            sb.Append("----------------------------------------------");

            return sb.ToString();
        }
    }
}
