using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Entidades.Enumerables;
using Entidades.Interfaces;

namespace Entidades
{
    public class Encargado : Empleado
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
            Id = _contadorId;
           
        }

        /// <summary>
        /// Se encarga de ejecutar una accion pasada por parametro.-
        /// Basado en el Patron de Diseño Command
        /// </summary>
        /// <param name="command"></param>
        public void EjecutarCommando(ICommand<string> command)
        {
            var resultado = command.EjecutarAccion();
            
        }

    }
}
