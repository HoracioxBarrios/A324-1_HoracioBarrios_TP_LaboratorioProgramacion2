using Entidades.Excepciones;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class GestorDeDelivery
    {
        private List<ICliente> _listaDeClientes;
        private List<IDelivery> _listaDeDeliverys;// SEGUIR HAY QUE METER A LOS MESEROS ACA
        private IEncargado _encargado;

        public GestorDeDelivery(IEncargado encargado)
        {
            _encargado = encargado;
            _listaDeClientes = new List<ICliente>();
            _listaDeDeliverys = new List<IDelivery>();
        }

        public void RegistrarDelivery(IDelivery delivery)
        {
            _listaDeDeliverys.Add(delivery);
        }

        //rEGISTRAR LOS CLIENTES
        public void RegistrarCliente(ICliente cliente)
        {
            _listaDeClientes.Add(cliente);
        }

        //gestor delivery emula >>>> gestor mesas
        public void AsignarClienteADelivery(int idDelDelivery, int idDelCliente)
        {
            IDelivery delivery = _listaDeDeliverys.FirstOrDefault(d => d.Id == idDelDelivery);
            ICliente cliente = _listaDeClientes.FirstOrDefault(c => c.Id == idDelCliente);

            if (delivery != null && cliente != null)
            {
                _encargado.AsignarClienteADelivery(cliente, delivery);
            }
            else
            {
                // Manejar el caso cuando no se encuentra el mesero o la mesa /
                if (delivery == null)
                {
                    throw new IdDelDeliveryBuscadoAlAsignarClienteADeliveryException("Error al buscar delivery por Id al querer asignar cliente a delivery");
                }
                if (cliente == null)
                {
                    throw new IdDelClienteBuscadoAlAsignarClienteADeliveryException("Error al buscar Cliente por Id al querer asignar cliente al delivery");
                }
            }
        }


        public ICliente ObtenerCliente(int id)
        {
            foreach (ICliente cliente in _listaDeClientes)
            {
                if (cliente.Id == id)
                {
                    return cliente;
                }
            }
            throw new ErrorAlBuscarClienteEnListaEnGestorDelivery("No esta el Cliente que estas buscando por ID");
        }
    }
}
