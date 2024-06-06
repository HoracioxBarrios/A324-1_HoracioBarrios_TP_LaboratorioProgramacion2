using System;
using System.Collections.Generic;
using Entidades.Enumerables;
using Entidades.Interfaces;

namespace Entidades
{
    public class Cocinero : Empleado, ICocinero
    {
       
        public Cocinero(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) :base(
            rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Contacto = contacto;
            this.Direccion = direccion;
            this.Salario = salario;
            this.Rol = rol;
        }


        /// <summary>
        /// Verifica si el plato esta en la lista de Menu
        /// </summary>
        /// <param name="nombrePlato"></param>
        /// <returns>Devuelve si existe en la Lista de Platos </returns>
        private bool ExitePlatoEnLista(string nombrePlato, List<Plato> listPlatosEnMenu)
        {
            bool seEncontro = false;
            foreach (Plato plato in listPlatosEnMenu)
            {
                if (plato.Nombre == nombrePlato)
                {
                    seEncontro = true;
                    break;
                }
            }
            return seEncontro;
        }


        /// <summary>
        /// Crea un Plato (Comida) si no existe aun y lo agrega a listaMenu
        /// </summary>
        /// <param name="nombrePlato"></param>
        /// <param name="listPlatosEnMenu"></param>
        public IConsumible CrearPlato()
        {
            throw new NotImplementedException();

        }
        public IConsumible EditarPlato()
        {
            throw new NotImplementedException();
        }

        public IConsumible EliminarPlato()
        {
            throw new NotImplementedException();
        }
         

    }
}
