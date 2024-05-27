using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Entidades.Enumerables;
using Entidades.Interfaces;

namespace Entidades
{
    public class Encargado : Empleado
    {
        
        private Encargado(){}
        public Encargado(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Contacto = contacto;
            this.Direccion = direccion;
            this.Salario = salario;
            this.Rol = rol;

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
