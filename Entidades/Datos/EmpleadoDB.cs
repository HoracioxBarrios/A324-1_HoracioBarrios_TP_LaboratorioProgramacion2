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
            _baseDeDatosPcEscritorio = "Hora\\SERVER_PRUEBA";
            _baseDeDatosPcEscritorioOfi = "DESKTOP-RF5OK6R\\RESTAURANT";

            _connectionString = $"Server={_baseDeDatosPcEscritorioOfi};Database=RestaurantDB;User Id=sa;Password=123456;TrustServerCertificate=true;";

            _tablaEmpleado = "Empleado";
            
        }


        /// <summary>
        /// Crea un empleado guardandolo en base de datos
        /// </summary>
        /// <param name="rol"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="contacto"></param>
        /// <param name="direccion"></param>
        /// <param name="salario"></param>
        /// <returns></returns>
        /// <exception cref="AlCrearEmpleadoEnDBException"></exception>
        public bool Create(ERol rol, string nombre, string apellido, string contacto, string direccion, decimal salario)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre) ||  string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(contacto) || string.IsNullOrEmpty(direccion) || salario <= 0)
                {
                    throw new DatoIncorrectoException("Los Datos son incorrectos");
                }

                if (!VerificarExistenciaDelEmpleadoEnTabla(nombre, apellido)) 
                {
                    IEmpleado empleado = EmpleadoServiceFactory.CrearEmpleado(rol, nombre, apellido, contacto, direccion, salario);
                    bool seCreo = false;
                    if (empleado != null)
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
                                seCreo = true;
                            }
                        }
                    }
                    return seCreo;
                }
                else
                {
                    throw new EmpleadoExisteEnDBException("Empleado ya Existe En DB");
                }
            }
            catch(DatoIncorrectoException e)
            {
                throw new AlCrearEmpleadoEnDBException($"Error al crear el empleado en la Base de Datos: {e.Message}", e);
            }
            catch(EmpleadoExisteEnDBException e)
            {
                throw new AlCrearEmpleadoEnDBException($"Error al crear el empleado en Base de datos: {e.Message}", e);
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

                    string querySelectOneforId = @"
                        SELECT Id, Nombre, Apellido, Contacto, Rol, Direccion, Salario, Password, Status
                        FROM Empleado
                        WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(querySelectOneforId, conn))
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

        public bool Update(int id, string password)
        {
            bool seActualizo = false;
            try
            {
                if (password != null)
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();
                        
                        string queryUpdateEmpleado = @"
                                UPDATE Empleado
                                SET Password = @Password
                                WHERE Id = @Id";

                        using (SqlCommand command = new SqlCommand(queryUpdateEmpleado, conn))
                        {
                            command.Parameters.AddWithValue("@Id", id);
                            command.Parameters.AddWithValue("@Password", password);
            

                            int filas = command.ExecuteNonQuery();
                            if (filas > 0)
                            {
                                seActualizo = true;
                            }
                        }
                    }
                    
                }
                return seActualizo;
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
        public bool Update(int id, string nombre, string apellido)
        {
            bool seActualizo = false;
            try
            {

                IEmpleado empleado = ReadOne(id);

                if (empleado != null)
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();

                        string queryUpdateEmpleado = @"
                                UPDATE Empleado
                                SET Nombre = @Nombre,
                                    Apellido = @Apellido
                                WHERE Id = @Id";

                        using (SqlCommand command = new SqlCommand(queryUpdateEmpleado, conn))
                        {
                            command.Parameters.AddWithValue("@Id", empleado.Id);
                            command.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                            command.Parameters.AddWithValue("@Apellido", empleado.Apellido);

                            int filas = command.ExecuteNonQuery();
                            if (filas > 0)
                            {
                                seActualizo = true;
                            }
                        }
                    }
                    
                }
                return seActualizo;
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
        public bool Update(int id, decimal salario)
        {
            bool seActualizo = false;
            try
            {

                IEmpleado empleado = ReadOne(id);

                if (empleado != null)
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();


                        string queryUpdateEmpleado = @"
                                UPDATE Empleado
                                SET Salario = @Salario
                                WHERE Id = @Id";

                        using (SqlCommand command = new SqlCommand(queryUpdateEmpleado, conn))
                        {
                            command.Parameters.AddWithValue("@Id", empleado.Id);
                            command.Parameters.AddWithValue("@Salario", empleado.Salario);
                            int filas = command.ExecuteNonQuery();
                            if (filas > 0)
                            {
                                seActualizo = true;
                            }
                        }
                    }                    
                }
                return seActualizo;
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
                            seElimino = true;
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

                    
                    string queryUpdateStatus = @"
                        UPDATE Empleado
                        SET Status = @Status
                        WHERE Nombre = @Nombre 
                        AND Apellido = @Apellido";

                    using (SqlCommand command = new SqlCommand(queryUpdateStatus, conn))
                    {
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Apellido", apellido);
                        command.Parameters.AddWithValue("@Status", (int)EStatus.Inactivo);

                        int filas = command.ExecuteNonQuery();
                        if (filas > 0)
                        {
                            seElimino = true;
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
        /// Comprueba si existe el empelado en db
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <returns>Devuelve True si existe y false si no existe</returns>
        private bool VerificarExistenciaDelEmpleadoEnTabla(string nombre, string apellido)
        {
            bool existe = true;
            IEmpleado empleado = ReadOne(nombre, apellido);
            if (empleado == null)
            {
                existe = false;
            }
            return existe;
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
