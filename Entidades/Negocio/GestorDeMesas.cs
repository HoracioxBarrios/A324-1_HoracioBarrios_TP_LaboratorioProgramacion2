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
        


        public GestorDeMesas(IEncargado encargado, int cantidadDeMesas)
        {
            _listaDeMesas = new List<IMesa>();
            _listaDeMeseros = new List<IMesero>();
            for (int i = 0; i < cantidadDeMesas;  i++)
            {
                IMesa mesa = new Mesa();
                mesa.Id = i +1;     //asigno aca una Id arranca en 1, la db deveria asignar luego
                _listaDeMesas.Add(mesa);
            }

            
        }

        public void RegistrarMesero(IMesero mesero)
        {
            if (mesero != null)
            {
                _listaDeMeseros.Add(mesero);
            }
        }

        public void AsignarMesaAMesero(string nombreDelMesero, string apellido, int idMesa)
        {
            IMesero mesero = _listaDeMeseros.FirstOrDefault(m => m.Nombre == nombreDelMesero && m.Apellido == apellido);
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




        public IMesero GetMesero(string nombre, string apellido)
        {
            return _listaDeMeseros.FirstOrDefault(m => m.Nombre == nombre && m.Apellido == apellido);
        }


        public IMesero GetMesero(int idMesero)
        {
            return _listaDeMeseros.FirstOrDefault(m => m.Id == idMesero );
        }


    }
}
