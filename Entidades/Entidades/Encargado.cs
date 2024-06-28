using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Entidades.Enumerables;
using Entidades.Interfaces;

namespace Entidades
{
    public class Encargado : Empleado, IEncargado, ICreadorDePedidos, IEditorDePedidos, IEliminadorDePedidos, IEstablecedorDePrecios
    {
        public Encargado(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : base(
            rol, nombre, apellido, contacto, direccion, salario)
        {
            Nombre = nombre;
            Apellido = apellido;
            Contacto = contacto;
            Direccion = direccion;
            Salario = salario;
            Rol = rol;


        }
        public Encargado(int id, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : this(rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Id = id;
        }

        public Encargado(int id, string password, EStatus status, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : this(
            id, rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Password = password;
            this.Status = status;
        }


        /// <summary>
        /// Asigna Mesero a Mesa
        /// </summary>
        /// <param name="mesa"></param>
        /// <param name="mesero"></param>
        public void AsignarMesaAMesero(IMesa mesa, IMesero mesero)
        {
            mesa.IdDelMesero = mesero.Id;
            mesero.RecibirMesa(mesa);
        }



        public void AsignarClienteADelivery(ICliente cliente, IDelivery delivery)
        {
            cliente.IdDelDelivery = delivery.Id;
            delivery.RecibirCliente(cliente);
        }








        public IPedido CrearPedido(ETipoDePedido tipoDePedido, List<IConsumible> ConsumiblesParaElPEdido, int idDelCliente)
        {
            return new Pedido(tipoDePedido, ConsumiblesParaElPEdido, idDelCliente);
        }


        /// <summary>
        /// Edita el pedido en la cola (queue)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pedidos"></param>
        /// <param name="consumiblesCorregidos"></param>
        public bool EditarPedido(int id, Queue<IPedido> pedidos, List<IConsumible> consumiblesCorregidos)
        {
            bool seCorrigio = false;
            int count = pedidos.Count;

            while (count > 0)
            {
                
                IPedido pedido = pedidos.Dequeue(); // Desencolar el pedido actual

                if (pedido.Id == id)
                {
                    pedido.EditarConsumibles(consumiblesCorregidos);
                    seCorrigio = true;
                }

                
                pedidos.Enqueue(pedido);// Volver a encolar el pedido (modificado o no)

                count--;
            }

            return seCorrigio;
        }

        /// <summary>
        /// Elimina el Pedido de la cola (Queue)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pedidos"></param>
        /// <returns></returns>
        public bool EliminarPedido(int id, Queue<IPedido> pedidos)
        {
            bool seElimino = false;
            List<IPedido> temporalList = new List<IPedido>();

            while(pedidos.Count > 0)
            {
                // Desencolamos elementos
                IPedido pedido = pedidos.Dequeue();
                if(pedido.Id != id)
                {
                    temporalList.Add(pedido);
                }
                else
                {
                    seElimino = true;
                }           
            }
            //Encolamos nuevamente
            foreach (IPedido ped in temporalList)
            {
                pedidos.Enqueue(ped);
            }

            return seElimino;
        }


        /// <summary>
        /// Establece el precio de venta
        /// </summary>
        /// <param name="producto"></param>
        /// <param name="precioDeVenta"></param>
        public void EstablecerPrecioAProducto(IConsumible producto, decimal precioDeVenta)
        {
            producto.Precio = precioDeVenta;
        }


    }
    
}
