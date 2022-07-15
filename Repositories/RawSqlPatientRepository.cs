using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using HomeworkDatabase2.Models;

namespace HomeworkDatabase2.Repositories
{
    public class RawSqlPatientRepository : IPatientRepository
    {
        private readonly string _connectionString;

        public RawSqlPatientRepository( string connectionString )
        {
            _connectionString = connectionString;
        }

        public IReadOnlyList<Patient> GetAll()
        {
            var result = new List<Patient>();

            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Name], [Birthdate], [Phonenumber] from [Patient]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while ( reader.Read() )
            {
                result.Add( new Patient(
                    Convert.ToInt32( reader[ "Id" ] ),
                    Convert.ToString( reader[ "Name" ] ),
                    Convert.ToDateTime( reader[ "Birthdate" ] ),
                    Convert.ToString( reader[ "Phonenumber" ] )
                ) );
            }

            return result;
        }

        public Patient GetByName( string name )
        {
            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Name], [Birthdate], [Phonenumber] from [Patient] where [Name] = @name";
            sqlCommand.Parameters.Add( "@name", SqlDbType.NVarChar, 255 ).Value = name;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if ( reader.Read() )
            {
                return new Patient(
                    Convert.ToInt32( reader[ "Id" ] ),
                    Convert.ToString( reader[ "Name" ] ),
                    Convert.ToDateTime( reader[ "Birthdate" ] ),
                    Convert.ToString( reader[ "Phonenumber" ] )
                );
            }
            else
            {
                return null;
            }
        }

        public Patient GetById( int id )
        {
            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Name], [Birthdate], [Phonenumber] from [Patient] where [Id] = @id";
            sqlCommand.Parameters.Add( "@id", SqlDbType.Int ).Value = id;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if ( reader.Read() )
            {
                return new Patient(
                    Convert.ToInt32( reader[ "Id" ] ),
                    Convert.ToString( reader[ "Name" ] ),
                    Convert.ToDateTime( reader[ "Birthdate" ] ),
                    Convert.ToString( reader[ "Phonenumber" ] )
                );
            }
            else
            {
                return null;
            }
        }

        public void Delete( Patient patient )
        {
            if ( patient == null )
            {
                throw new ArgumentNullException( nameof( patient ) );
            }

            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "delete [Patient] where [Id] = @id";
            sqlCommand.Parameters.Add( "@id", SqlDbType.Int ).Value = patient.Id;
            sqlCommand.ExecuteNonQuery();
        }

        public void Update( Patient patient )
        {
            if ( patient == null )
            {
                throw new ArgumentNullException( nameof( patient ) );
            }

            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "update [Patient] set [Name] = @name, [Birthdate] = @birthdate, [Phonenumber] = @phonenumber where [Id] = @id";
            sqlCommand.Parameters.Add( "@id", SqlDbType.Int ).Value = patient.Id;
            sqlCommand.Parameters.Add( "@name", SqlDbType.NVarChar, 255 ).Value = patient.Name;
            sqlCommand.Parameters.Add( "@birthdate", SqlDbType.DateTime ).Value = patient.Birthdate;
            sqlCommand.Parameters.Add( "@phonenumber", SqlDbType.NVarChar, 255).Value = patient.PhoneNumber;
            sqlCommand.ExecuteNonQuery();
        }
    }
}
