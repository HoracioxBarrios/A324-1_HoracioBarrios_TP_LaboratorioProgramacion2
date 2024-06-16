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
        private bool ExistePlatoEnLista(string nombrePlato, List<IConsumible> listaPlatosEnMenu)
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


        /// <summary>
        /// Al momento de Crear un plato debe contar antes con ingredientes en la lista para el plato, y debe tener al menos 2 ingredientes.
        /// Al Crear el Plato, limpia la lista de ingredientes del cocinero para que este proceda a elejir nueva conbinacion de ingredientes para un nuevo plato.
        /// </summary>
        /// <param name="nombreDelPlato"></param>
        /// <returns></returns>
        /// <exception cref="AlObtenerListaDeIngredientesException"></exception>
        public IConsumible CrearPlato(string nombreDelPlato, List<IConsumible> ingredientes)
        {
            
            if ( ingredientes.Count < 2)
            {
                throw new AlObtenerListaDeIngredientesException("El plato debe tener al menos 2 ingredientes.");
            }

            IConsumible plato =  new Plato(nombreDelPlato, ingredientes);

            return plato;
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

            platoActualizado.ReemplazarIngredientes(ingredientesActualizacion);

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
