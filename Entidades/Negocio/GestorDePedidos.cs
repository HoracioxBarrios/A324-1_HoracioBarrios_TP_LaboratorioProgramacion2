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
        private List<IPedido> _pedidos;
        private ICreadorDePedidos _creadorDePedidos;
        private IGestorMenu _gestorMenu ;//La instancia con los productos



        public GestorDePedidos(ICreadorDePedidos creadorDePedidos, IGestorMenu gestorMenu)
        {
            _pedidos = new List<IPedido>();
            _creadorDePedidos = creadorDePedidos;
            _gestorMenu = gestorMenu;
        }



        /// <summary>
        /// Crea un pedido en base a un menu
        /// </summary>
        /// <param name="menu"></param>
        public void Crearpedido(IMenu menu)
        {

        }


        /// <summary>
        /// Crea un pedidoen ase a varios menues
        /// </summary>
        /// <param name="menues"></param>
        public void CrearPedido(List<IMenu> menues) 
        {
        
        
        }

        /// <summary>
        /// Crea un pedido en base a productos Consumibles
        /// </summary>
        /// <param name="consumible"></param>
        public void CrearPedido(IConsumible consumible)
        {

        }
    }
}
