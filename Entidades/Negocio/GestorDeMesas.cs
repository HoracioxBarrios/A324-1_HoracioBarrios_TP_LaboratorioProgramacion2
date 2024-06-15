using Entidades.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Entidades.Excepciones;

namespace Negocio
{
    public class GestorDeMesas
    {
        private List<IMesa> _listaDeMesas;
        private List<IMesero> _listaDeMeseros;// SEGUIR HAY QUE METER A LOS MESEROS ACA
        private IEncargado _encargado;



        public GestorDeMesas(int cantidadDeMesas, List<IMesero> listaDeMeseros, IEncargado encargado)
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
        public void AsignarMesaAMesero(int idDelMesero, int idMesa)
        {
            IMesero mesero = _listaDeMeseros.FirstOrDefault(m => m.Id == idDelMesero);
            IMesa mesa = _listaDeMesas.FirstOrDefault(m => m.Id == idMesa);

            if (mesero != null && mesa != null)
            {
                _encargado.AsignarMesaAMesero(mesa, mesero);
            }
            else
            {
                // Manejar el caso cuando no se encuentra el mesero o la mesa /
                if (mesero == null)
                {
                    throw new IdDelMeseroBuscadoAlAsignarMesaAMeseroException("Error al buscar mesero por Id al querer asignar mesa a mesero");
                }
                if (mesa == null)
                {
                    throw new IdDeLaMesaBuscadaAlAsignarMesaAMeseroException("Error al buscar mesa por Id al querer asignar mesa al mesero");
                }
            }
        }
            
    }
}
