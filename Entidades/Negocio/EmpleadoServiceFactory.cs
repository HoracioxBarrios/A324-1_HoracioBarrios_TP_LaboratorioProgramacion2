using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;

namespace Negocio
{
    /// <summary>
    /// Class Empleado Service - (Usa el Patron de Diseño Factory Method para crear IEmpleados
    /// </summary>
    public static class EmpleadoServiceFactory
    {
        /// <summary>
        /// Methodo que Crea un Empleado
        /// </summary>
        /// <param name="rol">Representa el Rol que va a tener el empleado</param>
        /// <param name="nombre">Nombre del empleado</param>
        /// <param name="apellido">Apellido del empleado</param>
        /// <param name="contacto">Contato como Numero de Telefono, Correo, Email </param>
        /// <param name="direccion">Direccion representa el Lugar de residencia</param>
        /// <param name="salario"> Salario representa cuanto cobra el empleado</param>
        /// <returns>Devuelve un IEmpleado</returns>
        /// <exception cref="EmpleadoDatosException">Lanza una excecion si los datos no son Validos</exception>
        /// <exception cref="EmpleadoRolNoExistenteException">Lanza una excepcion si el rol es incorrecto</exception>
        public static IEmpleado CrearEmpleado(ERol rol, string nombre, string apellido, string contacto
            , string direccion, decimal salario)
        {
            if ((string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido)
                || string.IsNullOrEmpty(contacto) || string.IsNullOrEmpty(direccion) ||salario <= 0))
            {
                throw new EmpleadoDatosException("Datos de empleado Invalido");

            }
            switch (rol)
            {
                case ERol.Encargado:
                    return new Encargado(rol, nombre, apellido, contacto, direccion, salario);
                case ERol.Mesero:
                    return new Mesero(rol, nombre, apellido, contacto, direccion, salario);
                case ERol.Cocinero:
                    return new Cocinero(rol, nombre, apellido, contacto, direccion, salario);
                case ERol.Delivery:
                    return new Delivery(rol, nombre, apellido, contacto, direccion, salario);
                default:
                    throw new EmpleadoRolNoExistenteException("El rol de empleado es Incorrecto");
            }
        }





        /// <summary>
        /// Sobrecarga para crear el empleado en la consula a DB - (Dentro de EmpleadoDB)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rol"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="contacto"></param>
        /// <param name="direccion"></param>
        /// <param name="salario"></param>
        /// <param name="password"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="EmpleadoDatosException"></exception>
        /// <exception cref="EmpleadoRolNoExistenteException"></exception>
        public static IEmpleado CrearEmpleado(int id, string password, EStatus status, ERol rol, string nombre, string apellido, string contacto
           , string direccion, decimal salario)
        {
            if ((string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido)
                || string.IsNullOrEmpty(contacto) || string.IsNullOrEmpty(direccion) || salario <= 0 || id < 0 || string.IsNullOrEmpty(password)))
            {
                throw new EmpleadoDatosException("Datos de empleado Invalido");

            }
            switch (rol)
            {
                case ERol.Encargado:
                    return new Encargado(id, password, status, rol, nombre, apellido, contacto, direccion, salario);
                case ERol.Mesero:
                    return new Mesero(id, password, status, rol, nombre, apellido, contacto, direccion, salario);
                case ERol.Cocinero:
                    return new Cocinero(id, password, status, rol, nombre, apellido, contacto, direccion, salario);
                case ERol.Delivery:
                    return new Delivery(id, password, status, rol, nombre, apellido, contacto, direccion, salario);
                default:
                    throw new EmpleadoRolNoExistenteException("El rol de empleado es Incorrecto");
            }
        }

    }

}
