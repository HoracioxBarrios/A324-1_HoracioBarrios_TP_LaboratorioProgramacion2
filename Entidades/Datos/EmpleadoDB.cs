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
    
    public class EmpleadoDB : IOperacionesDeBaseDeDatos<IEmpleado>
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
        public bool Create(IEmpleado empleado)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(_connectionString)) 
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
                        if (filas > 0)
                        {
                            return true;
                        }
                        return false;
                    }
                
                }
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
            throw new NotImplementedException();
        }

        public IEmpleado ReadOne(string nombre, string apellido0)
        {
            throw new NotImplementedException();
        }
        public bool Update(int id, IEmpleado entidad)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string nombre, string apellido)
        {
            throw new NotImplementedException();
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
