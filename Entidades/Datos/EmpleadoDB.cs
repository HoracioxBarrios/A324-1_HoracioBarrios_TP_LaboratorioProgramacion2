using Entidades;
using Entidades.Enumerables;
using Entidades.Excepciones;
using Entidades.Interfaces;
using Negocio;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;


//https://www.connectionstrings.com
//https://www.connectionstrings.com/sql-server-2019/
namespace Datos
{
    
    public class EmpleadoDB :IOperacionesEmpleadoDB
    {
        private string _connectionString;
        private string _tablaEmpleado;
        private string _baseDeDatosPcEscritorio;
        private string _baseDeDatosPcEscritorioOfi;

        public EmpleadoDB()
        {
            _baseDeDatosPcEscritorio = @"";
            _baseDeDatosPcEscritorioOfi = @"DESKTOP-RF5OK6R\RESTAURANT";

            _connectionString = $"Server={_baseDeDatosPcEscritorioOfi};Database=RestaurantDB;User Id=sa;Password=123456;TrustServerCertificate=true;";

            _tablaEmpleado = "Empleado";
            
        }


        /// <summary>
        /// Crea el empleado en la Base de datos
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns>Devuelve true si fue exitoso, sino False</returns>
        /// <exception cref="AlCrearEmpleadoEnDBException"></exception>
        public bool Create(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario)
        {
            try
            {
                IEmpleado empleado = EmpleadoServiceFactory.CrearEmpleado(rol, nombre, apellido, contacto, direccion, salario);
                bool seCreo = false;
                if(empleado != null )
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();
                        string queryInsertEmpleado = @"
                    INSERT INTO Empleado (Nombre, Apellido, Contacto, Rol, Direccion, Salario, Password, Status)
                    VALUES (@Nombre, @Apellido, @Contacto, @Rol, @Direccion, @Salario, @Password, @Status)";

                        using (SqlCommand command = new SqlCommand(queryInsertEmpleado, conn))
                        {
                            command.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                            command.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                            command.Parameters.AddWithValue("@Contacto", empleado.Contacto);
                            command.Parameters.AddWithValue("@Rol", (int)empleado.Rol);
                            command.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                            command.Parameters.AddWithValue("@Salario", empleado.Salario);
                            command.Parameters.AddWithValue("@Password", empleado.Password);
                            command.Parameters.AddWithValue("@Status", (int)empleado.Status);

                            int filas = command.ExecuteNonQuery();
                            return seCreo;
                        }
                    }                    
                }
                return seCreo;
            }
            catch (ArgumentException e)
            {
                throw new AlCrearEmpleadoEnDBException($"Error al crear el empleado en la Base de Datos: {e.Message}", e);
            }
            
            catch(Exception e)
            {
                throw new AlCrearEmpleadoEnDBException($"Error desconocido al crear el empleado en la Base de datos: {e.Message}", e);
            }
        }

        public List<IEmpleado> ReadAll()
        {
            List<IEmpleado> empleados = new List<IEmpleado>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string querySelectAll = @"
                    SELECT Id, Nombre, Apellido, Contacto, Rol, Direccion, Salario, Password, Status
                    FROM Empleado";

                    using(SqlCommand command = new SqlCommand(querySelectAll, conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                
                                int id = Convert.ToInt32(reader["Id"]);
                                string nombre = Convert.ToString(reader["Nombre"]);
                                string apellido = Convert.ToString(reader["Apellido"]);                                
                                string contacto = Convert.ToString(reader["Contacto"]);
                                ERol rol = (ERol)Convert.ToInt32(reader["Rol"]);
                                string direccion = Convert.ToString(reader["Direccion"]);
                                decimal salario = Convert.ToDecimal(reader["Salario"]);
                                string password = Convert.ToString(reader["Password"]);
                                EStatus status = (EStatus)Convert.ToInt32(reader["Status"]);

                                IEmpleado empleado = EmpleadoServiceFactory.CrearEmpleado(id, password,status,rol, nombre, apellido, contacto, direccion, salario);
                                if(empleado != null) 
                                {
                                    empleados.Add(empleado);
                                }
                            }                            
                        }
                    }                    
                }
                return empleados;
            }
            catch(Exception e)
            {
                throw new ReadAllEnDbException($"Error al Leer los Empleados de la Base de Datos {e.Message}", e);
            }
        }
        public IEmpleado ReadOne(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string querySelectOneById = @"
                        SELECT Id, Nombre, Apellido, Contacto, Rol, Direccion, Salario, Password, Status
                        FROM Empleado
                        WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(querySelectOneById, conn))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int empleadoId = Convert.ToInt32(reader["Id"]);
                                string nombre = Convert.ToString(reader["Nombre"]);
                                string apellido = Convert.ToString(reader["Apellido"]);
                                string contacto = Convert.ToString(reader["Contacto"]);
                                ERol rol = (ERol)Convert.ToInt32(reader["Rol"]);
                                string direccion = Convert.ToString(reader["Direccion"]);
                                decimal salario = Convert.ToDecimal(reader["Salario"]);
                                string password = Convert.ToString(reader["Password"]);
                                EStatus status = (EStatus)Convert.ToInt32(reader["Status"]);

                                IEmpleado empleado = EmpleadoServiceFactory.CrearEmpleado(
                                    empleadoId, password, status, rol, nombre, apellido, contacto, direccion, salario);

                                return empleado;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                throw new ReadOneEnDbException($"Error al Leer el Empleado de la Base de Datos: {e.Message}", e);
            }
        }


        public IEmpleado ReadOne(string nombre, string apellido)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string querySelectOne = @"
                        SELECT Id, Nombre, Apellido, Contacto, Rol, Direccion, Salario, Password, Status
                        FROM Empleado
                        WHERE Nombre = @Nombre AND Apellido = @Apellido";

                    using (SqlCommand command = new SqlCommand(querySelectOne, conn))
                    {
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Apellido", apellido);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                string nombreEmpleado = Convert.ToString(reader["Nombre"]);
                                string apellidoEmpleado = Convert.ToString(reader["Apellido"]);
                                string contacto = Convert.ToString(reader["Contacto"]);
                                ERol rol = (ERol)Convert.ToInt32(reader["Rol"]);
                                string direccion = Convert.ToString(reader["Direccion"]);
                                decimal salario = Convert.ToDecimal(reader["Salario"]);
                                string password = Convert.ToString(reader["Password"]);
                                EStatus status = (EStatus)Convert.ToInt32(reader["Status"]);

                                IEmpleado empleado = EmpleadoServiceFactory.CrearEmpleado(
                                    id, password, status, rol, nombreEmpleado, apellidoEmpleado, contacto, direccion, salario);

                                return empleado;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                throw new ReadOneEnDbException($"Error al Leer el Empleado de la Base de Datos: {e.Message}", e);
            }
        }

        public bool Update(int id, string password, EStatus status, ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario)
        {
            bool seActualizo = false;
            try
            {
                // Crear el objeto empleado
                IEmpleado empleado = EmpleadoServiceFactory.CrearEmpleado(id, password, status, rol, nombre, apellido, contacto, direccion, salario);

                if (empleado != null)
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();

                        // Definir la consulta SQL para actualizar
                        string queryUpdateEmpleado = @"
                                UPDATE Empleado
                                SET Nombre = @Nombre,
                                    Apellido = @Apellido,
                                    Contacto = @Contacto,
                                    Rol = @Rol,
                                    Direccion = @Direccion,
                                    Salario = @Salario,
                                    Password = @Password,
                                    Status = @Status
                                WHERE Id = @Id";

                        using (SqlCommand command = new SqlCommand(queryUpdateEmpleado, conn))
                        {
                            command.Parameters.AddWithValue("@Id", empleado.Id);
                            command.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                            command.Parameters.AddWithValue("@Apellido", empleado.Apellido);
                            command.Parameters.AddWithValue("@Contacto", empleado.Contacto);
                            command.Parameters.AddWithValue("@Rol", (int)empleado.Rol);
                            command.Parameters.AddWithValue("@Direccion", empleado.Direccion);
                            command.Parameters.AddWithValue("@Salario", empleado.Salario);
                            command.Parameters.AddWithValue("@Password", empleado.Password);
                            command.Parameters.AddWithValue("@Status", (int)empleado.Status);

                            int filas = command.ExecuteNonQuery();
                            if (filas > 0)
                            {
                                return seActualizo;
                            }
                        }
                    }
                    return seActualizo;
                }
                else
                {
                    throw new AlCrearEmpleadoEsNullException("Error el empleado no se pudo crear por ende es null, y por eso no se guardo en la Base de Datos");
                }
            }
            catch (AlCrearEmpleadoEsNullException e)
            {
                throw new AlCrearEmpleadoEnDBException($"Error al actualizar el empleado en la Base de Datos: {e.Message}", e);
            }
            catch (ArgumentException e)
            {
                throw new AlCrearEmpleadoEnDBException($"Error al actualizar el empleado en la Base de Datos: {e.Message}", e);
            }
            catch (Exception e)
            {
                throw new AlCrearEmpleadoEnDBException($"Error desconocido al actualizar el empleado en la Base de datos: {e.Message}", e);
            }
        }
        public bool Delete(int id)
        {
            bool seElimino = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // Definir la consulta SQL para actualizar el estado a Inactivo
                    string queryUpdateStatus = @"
                    UPDATE Empleado
                    SET Status = @Status
                    WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(queryUpdateStatus, conn))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Status", (int)EStatus.Inactivo);

                        int filas = command.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            return seElimino;
                        }
                    }
                }
                return seElimino;
            }
            catch (Exception e)
            {
                throw new AlEliminarDeFormaLogicaEnDBException($"Error al Eliminar de Forma Logica al empleado en la Base de Datos: {e.Message}", e);
            }
        }


        public bool Delete(string nombre, string apellido)
        {
            bool seElimino = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // Definir la consulta SQL para actualizar el estado a Inactivo basado en nombre y apellido
                    string queryUpdateStatus = @"
                        UPDATE Empleado
                        SET Status = @Status
                        WHERE Nombre = @Nombre AND Apellido = @Apellido";

                    using (SqlCommand command = new SqlCommand(queryUpdateStatus, conn))
                    {
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Apellido", apellido);
                        command.Parameters.AddWithValue("@Status", (int)EStatus.Inactivo);

                        int filas = command.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            return seElimino;
                        }
                    }
                }
                return seElimino;
            }
            
            catch (Exception e)
            {
                throw new AlEliminarDeFormaLogicaEnDBException($"Error al Eliminar de Forma Logica al empleado en la Base de Datos: {e.Message}", e);
            }
        }

























        /// <summary>
        /// Verifica la existencia de la Tabla Empleado
        /// </summary>
        /// <returns>Devuelve True si existe y false si No existe</returns>
        /// <exception cref="ConectarADbException"></exception>
        private bool VerificarExistenciaDeTabla()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string QueryChequearTablaEmpleado = @" 
                    SELECT COUNT(*) 
                    FROM INFORMATION_SCHEMA.TABLES
                    WHERE TABLE_SCHEMA = 'dbo'
                    AND TABLE_NAME = @tablaEmpleado"
                    ;

                    using(SqlCommand command = new SqlCommand(QueryChequearTablaEmpleado, conn))
                    {
                        command.Parameters.AddWithValue("@tablaEmpleado", _tablaEmpleado);// encapsulo la tabla para seguridad
                        int contadorTabla = (int)command.ExecuteScalar();
                        if (contadorTabla > 0 ) 
                        { 
                            return true; 
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                }
            }
            catch( ArgumentException e)
            {
                throw new ConectarADbException($"Error al Verificar la Existencia De Tabla Empleado en la Db: {e.Message}", e);
            }
            catch ( Exception e )
            {
                throw new ConectarADbException($"Error Desconocido al Verificar la Existencia De Tabla Empleado en la Db:: {e.Message}", e );
            }
        }


        /// <summary>
        /// Crea la Tabla Empleado en la Db (Antes verifica si existe)
        /// </summary>
        /// <returns>Devuelve True: si pudo crear la Tabla Empleado, False: No se creo la Tabla porque se considera que ya existe</returns>
        /// <exception cref="AlCrearTablaEmpleadoException"></exception>
        public bool CrearTabla()
        {
            if (!VerificarExistenciaDeTabla())
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();
                        string QueryCrearTablaEmpleado = @"
                        CREATE TABLE Empleado (
                            Id INT PRIMARY KEY IDENTITY(1,1),
                            Nombre VARCHAR(100) NOT NULL,
                            Apellido VARCHAR(100) NOT NULL,
                            Contacto VARCHAR(100) NOT NULL,
                            Rol INT NOT NULL,
                            Direccion VARCHAR(200) NOT NULL,
                            Salario DECIMAL(10, 2) NOT NULL,
                            Password VARCHAR(100) NOT NULL,
                            Status INT NOT NULL
                        )";

                        using (SqlCommand command = new SqlCommand(QueryCrearTablaEmpleado, conn))
                        {
                            command.ExecuteNonQuery();
                            return true;
                        }

                    }
                }
                catch (ArgumentException e)
                {
                    throw new AlCrearTablaEmpleadoException($"Error al querer Crear tabla de empleado en la DB: {e.Message}", e);
                }
                catch (Exception e)
                {
                    throw new AlCrearTablaEmpleadoException($"Error Desconocido al querer Crear la tabla de empleado en la Db: {e.Message}", e);
                }
            }
            return false;

        }


    }
}
