using Entidades.Excepciones;
using Entidades.Interfaces;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;


//https://www.connectionstrings.com
//https://www.connectionstrings.com/sql-server-2019/
namespace Datos
{
    
    public static class EmpleadoDB
    {
        private static string _connectionString;
        private static string _tablaEmpleado;

        static  EmpleadoDB()
        {
            _connectionString = @"Server=DESKTOP-RF5OK6R\RESTAURANT;Database=RestaurantDB;User Id=sa;Password=123456;TrustServerCertificate=true;";

            _tablaEmpleado = "Empleado";
            
        }























        /// <summary>
        /// Verifica la existencia de la Tabla Empleado
        /// </summary>
        /// <returns>Devuelve True si existe y false si No existe</returns>
        /// <exception cref="ConectarADbException"></exception>
        private static bool VerificarExistenciaDeTabla()
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
        public static bool CrearTablaEmpleado()
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
