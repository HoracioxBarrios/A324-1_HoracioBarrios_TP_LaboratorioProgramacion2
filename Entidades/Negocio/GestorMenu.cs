using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class GestorMenu
    {
        private List<IConsumible> _listaDeMenus;
        private List<IConsumible> _listaPlatos;
        private ICocinero _cocinero;
        public GestorMenu(ICocinero cocinero) 
        { 
            _cocinero = cocinero;
        }

        public void CrearMenu()
        {
            if(_listaPlatos.Count > 0)
            {

            }
        }

        

    }
}
