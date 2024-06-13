using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;


namespace Negocio
{
    public class GestorDeEmpleados : IGestorDeEmpleados
    {
        private List<IEmpleado> _listaDeEmpleados;
        private IOperacionesEmpleadoDB _operacionesDeBaseDeDatos;

        public GestorDeEmpleados(IOperacionesEmpleadoDB operacionesDeBaseDeDatos)
        {
            _listaDeEmpleados = new List<IEmpleado>();
            _operacionesDeBaseDeDatos = operacionesDeBaseDeDatos;
        }






        public bool CrearEmpleado(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario)
        {            
            try
            {                  
                bool seCreo = _operacionesDeBaseDeDatos.Create(rol, nombre, apellido, contacto, direccion, salario);
                ActualizarListaEmpleadosLocal();
                return seCreo;

            }
            catch (EmpleadoDatosException e)
            {
                throw new EmpleadoErrorAlCrearException("No se Pudo crear el Empleado: ", e);
            }
            catch (Exception e)
            {
                throw new Exception($"Error desconocido : {e.Message}", e);
            }
            
        }

        public void EditarEmpleado(int id, string password, EStatus status, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario)
        {
            try
            {
                bool seEdito = _operacionesDeBaseDeDatos.Update(id, password, status, rol, nombre, apellido, contacto, direccion, salario);
                if (seEdito)
                {
                    ActualizarListaEmpleadosLocal();
                }
            }
            catch(Exception e)
            {
                throw new AlEditarEmpleadoEnDBException($"Error al editar el empleado en la base de datos {e.Message}", e);
            }

        }

        public void EliminarEmpleado(int id)
        {
            try
            {
                bool seEliminoDeFormaLogica = _operacionesDeBaseDeDatos.Delete(id);
            }
            catch(Exception e)
            {
                throw new AlEliminarEmpleadoDeLaBaseDeDatosException($"Error al eliminar Empleado de la Base de datos {e.Message}", e);
            }
            
        }
        public void EliminarEmpleado(string nombre, string apellido)
        {
            try
            {
                bool seEliminoDeFormaLogica = _operacionesDeBaseDeDatos.Delete(nombre, apellido);
            }
            catch (Exception e)
            {
                throw new AlEliminarEmpleadoDeLaBaseDeDatosException($"Error al eliminar Empleado de la Base de datos {e.Message}", e);
            }

        }

        public List<IEmpleado> GetEmpleadosEnList()
        {
            ActualizarListaEmpleadosLocal();
            if (_listaDeEmpleados.Count > 0)
            {
                return _listaDeEmpleados;
            }
            throw new ListaVaciaException("La lista esta vacia");
        }
        public IEmpleado GetEmpleadoEnList(string nombreEmpleado)
        {
            ActualizarListaEmpleadosLocal();
            if (_listaDeEmpleados.Count > 0)
            {
                foreach (IEmpleado empleado in _listaDeEmpleados)
                {
                    if (empleado.Nombre == nombreEmpleado)
                    {
                        return empleado;
                    }
                }
            }
            throw new ListaVaciaException("La lista esta vacia");
        }

        private void ActualizarListaEmpleadosLocal()
        {
            try
            {
                _listaDeEmpleados = _operacionesDeBaseDeDatos.ReadAll();
            }
            catch (Exception ex)
            {
                throw new ActualizarListaEmpleadosLocalException($"Error al Actualizar la Lista Local {ex.Message}",ex);
            }
            
        }
    }
}
