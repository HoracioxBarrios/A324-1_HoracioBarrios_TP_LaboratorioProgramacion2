using System;
using System.Collections.Generic;
using Entidades.Enumerables;
using Entidades.Interfaces;

namespace Entidades
{
    public class Encargado : Empleado
    {
        private Encargado()
        {
            this.Rol = ERol.Encargado;
        }
        public Encargado(string nombre, string apellido, string contacto, string direccion, decimal salario) :this()
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Contacto = contacto;
            this.Direccion = direccion;
            this.Salario = salario;
            
        }
        public void EjecutarCommando(ICommand<string> command)
        {
            var resultado = command.EjecutarAccion();
            
        }

    }
}
