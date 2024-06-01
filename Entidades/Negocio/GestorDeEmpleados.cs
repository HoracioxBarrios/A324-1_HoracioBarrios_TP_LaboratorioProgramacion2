using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;


namespace Negocio
{
    public class GestorDeEmpleados
    {
        private List<IEmpleado> _listaDeEmpleados;

        public GestorDeEmpleados()
        {
            _listaDeEmpleados = new List<IEmpleado>();
        }

        public void CrearEmpleado(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario)
        {
            try
            {
                IEmpleado empleado = EmpleadoFactoryService.CrearEmpleado(rol, nombre, apellido, contacto, direccion, salario);
                _listaDeEmpleados.Add(empleado);
            }
            catch (EmpleadoDatosException ex)
            {
                throw new EmpleadoErrorAlCrearException("No se Pudo crear el Empleado: ", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error desconocido: {ex.Message}", ex);
            }
            
        }

        public List<IEmpleado> GetEmpleados()
        {
            return _listaDeEmpleados;
        }


    }
}
