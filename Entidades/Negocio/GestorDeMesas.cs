using Entidades.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class GestorDeMesas
    {
        private List<IMesa> _listaDeMesas;
        private List<IMesero> _meseros;// SEGUIR HAY QUE METER A LOS MESEROS ACA


        //public GestorDeMesas() { }//debe recibir MesaDB
        public GestorDeMesas(int cantidadDeMesas)
        {
            _listaDeMesas = new List<IMesa>();
            for (int i = 0; i < cantidadDeMesas;  i++)
            {
                IMesa mesa = new Mesa();
                _listaDeMesas.Add(mesa);
            }
        }
        public void AsignarMesaAMesero(IMesa Mesa, IMesero mesero) 
        { 
            
        }
        public void CobrarMesa()
        {

        }
    }
}
