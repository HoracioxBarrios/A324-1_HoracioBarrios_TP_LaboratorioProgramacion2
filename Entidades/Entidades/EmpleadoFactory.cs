using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;

namespace Entidades
{
    public static class EmpleadoFactory
    {

        public static IEmpleado CrearEmpleado(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario)
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
    }

}
