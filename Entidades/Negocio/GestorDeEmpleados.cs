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
                IEmpleado empleado = EmpleadoServiceFactory.CrearEmpleado(rol, nombre, apellido, contacto, direccion, salario);
                _listaDeEmpleados.Add(empleado);
            }
            catch (EmpleadoDatosException ex)
            {
                throw new EmpleadoErrorAlCrearException("No se Pudo crear el Empleado: ", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error desconocido : {ex.Message}", ex);
            }
            
        }

        public List<IEmpleado> GetEmpleados()
        {
            if(_listaDeEmpleados.Count > 0)
            {
                return _listaDeEmpleados;
            }
            throw new ListaVaciaException("La lista esta vacia");
        }

        private int VerificarExistenciaDeEmpleado(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new DatoIncorrectoException("El Dato es invalido");
            }
            foreach(IEmpleado empleado in GetEmpleados())
            {
                if(string.Equals(empleado.Nombre, nombre, StringComparison.OrdinalIgnoreCase))
                {
                    Empleado? emp = empleado as Empleado;
                    {
                        return emp.Id;
                    }
                }
            }
            throw new AlObtenerIDException("Error al obtener la Id");
        }

    }
}
