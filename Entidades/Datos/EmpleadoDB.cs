using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace Datos
{
    public class EmpleadoDB
    {
        private string _connectionString;

        public EmpleadoDB(string connectionString)
        {
            _connectionString = connectionString;
        }


        public void CrearTablaEnDb(string nombreDeLaTabla) //la tabla puede ser Empleado
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(_connectionString);
                connection.Open();

                string checkTableQuery = $@"
                    SELECT COUNT(*)
                    FROM INFORMATION_SCHEMA.TABLES
                    WHERE TABLE_SCHEMA = 'dbo'
                    AND TABLE_NAME = '{nombreDeLaTabla}'
                ";


                using(var command  = new SqlCommand(checkTableQuery,connection))
                {
                    int contadorTable = (int)command.ExecuteScalar();
                    if (contadorTable > 0)
                    {
                        throw new Exception("La Tabla ya existe");
                    }
                    else
                    {
                        string createTableQuery = $@"
                            CREATE TABLE {nombreDeLaTabla} (
                                Id INT PRIMARY KEY IDENTITY(1,1),
                                Nombre NVARCHAR(100) NOT NULL,
                                Apellido NVARCHAR(100) NOT NULL,
                                Contacto NVARCHAR(100),
                                Rol NVARCHAR(50) NOT NULL,
                                Direccion NVARCHAR(200),
                                Salario DECIMAL(10, 2) NOT NULL,
                                Password NVARCHAR(100)
                            )
                        ";


                        using (var createCommand = new SqlCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }
                }


            }
            catch (Exception e)
            {

            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }


        
        }

    }
}
