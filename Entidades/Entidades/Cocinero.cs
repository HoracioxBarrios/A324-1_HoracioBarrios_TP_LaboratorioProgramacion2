using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Entidades.Enumerables;
using Entidades.Excepciones;
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
        public Cocinero(int id, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : this(
            rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Id = id;
        }

        /// <summary>
        /// Verifica si el plato esta en la lista de Menu
        /// </summary>
        /// <param name="nombrePlato"></param>
        /// <returns>Devuelve si existe en la Lista de Platos </returns>
        private bool ExitePlatoEnLista(string nombrePlato, List<IConsumible> listaPlatosEnMenu)
        {
            bool seEncontro = false;
            foreach (IConsumible plato in listaPlatosEnMenu)
            {
                if (plato.Nombre == nombrePlato)
                {
                    seEncontro = true;
                    break;
                }
            }
            return seEncontro;
        }

        public IConsumible CrearPlato(string nombre, List<IConsumible> listaDeIngredientes)
        {
            if (listaDeIngredientes == null || listaDeIngredientes.Count < 2)
            {
                throw new AlObtenerListaDeIngredientesException("El plato debe tener al menos 2 ingredientes.");
            }

            return new Plato(nombre, listaDeIngredientes);
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
