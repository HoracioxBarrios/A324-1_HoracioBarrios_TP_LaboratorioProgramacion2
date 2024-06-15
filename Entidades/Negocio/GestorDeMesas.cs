using Entidades.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Negocio
{
    public class GestorDeMesas
    {
        private List<IMesa> _listaDeMesas;
        private List<IMesero> _listaDeMeseros;// SEGUIR HAY QUE METER A LOS MESEROS ACA



        //public GestorDeMesas() { }//debe recibir MesaDB
        public GestorDeMesas(int cantidadDeMesas, List<IMesero> listaDeMeseros)
        {
            _listaDeMesas = new List<IMesa>();
            for (int i = 0; i < cantidadDeMesas;  i++)
            {
                IMesa mesa = new Mesa();
                mesa.Id = i +1;//asigno aca una Id arranca en 1
                _listaDeMesas.Add(mesa);
            }

            _listaDeMeseros = listaDeMeseros;
        }
        public void AsignarMesaAMesero() 
        { 
            
        }

        //public List<IMesero> GetMeserosConMesaAsignada() 
        //{ 
        //    if(_listaDeMesasConMeseroAsignado.Count > 0)
        //    {
        //        return _listaDeMesasConMeseroAsignado;
        //    }
        //    throw new Exception("La lista de mesas con mesero asignado esta vacia. Hay que asignar mesas a los ");
        //}
    }
}
