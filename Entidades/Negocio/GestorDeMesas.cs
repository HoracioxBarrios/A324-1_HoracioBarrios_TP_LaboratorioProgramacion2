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



        public GestorDeMesas(IEncargado encargado, int cantidadMesas)
        {
            _listaDeMeseros = new List<IMesero>();
            _listaDeMesas = new List<IMesa>();
            _encargado = encargado;
            // Inicializar las mesas según la cantidad especificada
            for (int i = 1; i <= cantidadMesas; i++)
            {
                _listaDeMesas.Add(new Mesa(i, 4)); // Supongamos que cada mesa tiene 4 comensales
            }
        }

        public void RegistrarMesero(IMesero mesero)
        {
            _listaDeMeseros.Add(mesero);
        }


        public void AsignarMesaAMesero(string nombreDelMesero, string apellido, int idMesa)
        {
            // Buscar el mesero por nombre y apellido
            IMesero mesero = _listaDeMeseros.FirstOrDefault(m => m.Nombre == nombreDelMesero && m.Apellido == apellido);

            if (mesero == null)
            {
                throw new InvalidOperationException("Mesero no encontrado en AsignarMesaAMesero");
            }

            // Buscar la mesa por id
            IMesa mesa = _listaDeMesas.FirstOrDefault(m => m.Id == idMesa);
            if (mesa == null)
            {
                throw new InvalidOperationException("Mesa no encontrada en AsignarMesaAMesero");
            }

            // Asignar la mesa al mesero
            mesa.IdDelMesero = mesero.Id;
            mesero.RecibirMesa(mesa);
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


        public IMesa GetMesa(int id)
        {
            foreach(IMesa mesa in _listaDeMesas)
            {
                if(mesa.Id == id)
                {
                    return mesa;
                }
            }
            throw new ErrorAlBuscarMesaEnListaEnGestorDeMesas("No esta la mesa que estas buscando por ID");
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
