using System;
using System.Collections.Generic;
using Entidades.Enumerables;
using Entidades.Interfaces;

namespace Entidades
{
    public class Cocinero : Empleado, ICrearPlato, IModificarPlato, IEliminarPlato
    {
        private List<MenuPlatos> menuPlatos;
        private Cocinero() 
        {
            this.Rol = ERol.Cocinero;
        }
        public Cocinero(string nombre, string apellido, string contacto, string direccion, decimal salario) : this() 
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Contacto = contacto;
            this.Direccion = direccion;
            this.Salario = salario;            
        }

        public void CrearPlato()
        {
            throw new NotImplementedException();
        }

        public void ModificarPlato()
        {
            throw new NotImplementedException();
        }

        public void EliminarPlato()
        {
            throw new NotImplementedException();
        }

        public Plato GetPlato()
        {
            //Recorre la lista, busca el plato y lo devuelve
            return new Plato();
        }
    }
}
