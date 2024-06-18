using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Entidades.Enumerables;
using Entidades.Interfaces;

namespace Entidades
{
    public class Encargado : Empleado, IEncargado, ICreadorDePedidos
    {      
        public Encargado(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario):base(
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
            mesero.AgregarMesa(mesa);
        }

        public void CrearPedido(ETipoDePedido tipoDePedido, List<IConsumible> ConsumiblesParaElPEdido)
        {

        }


    }
}
