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
        public Cocinero(int id, string password, EStatus status, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario) : this(
            id, rol, nombre, apellido, contacto, direccion, salario)
        {
            this.Password = password;
            this.Status = status;
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


        public IConsumible EditarPlato(IConsumible plato, List<IConsumible> ingredientesActualizacion)
        {
            if (ingredientesActualizacion == null || ingredientesActualizacion.Count < 2)
            {
                throw new AlObtenerListaDeIngredientesException("El plato debe tener al menos 2 ingredientes.");
            }

            Plato platoActualizado = plato as Plato;
            if (platoActualizado == null)
            {
                throw new InvalidCastException("El objeto plato no puede ser convertido a Plato.");
            }

            platoActualizado.SetIngredientesDelPlato(ingredientesActualizacion);

            return platoActualizado as IConsumible;
        }


        public void EliminarPlato(string nombre, List<IConsumible> listaDePlatos)
        {
            for (int i = 0; i < listaDePlatos.Count; i++)
            {
                if (listaDePlatos[i].Nombre == nombre)
                {
                    listaDePlatos.RemoveAt(i);
                    break; 
                }
            }
        }


    }
}
