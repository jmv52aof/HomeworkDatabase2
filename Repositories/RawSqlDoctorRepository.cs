using System.Data;
using System.Data.SqlClient;
using HomeworkDatabase2.Models;

namespace HomeworkDatabase2.Repositories
{
    public class RawSqlDoctorRepository : IDoctorRepository
    {
        private readonly string _connectionString;

        public RawSqlDoctorRepository( string connectionString )
        {
            _connectionString = connectionString;
        }

        public IReadOnlyList<Doctor> GetAll()
        {
            var result = new List<Doctor>();

            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Name], [Specialisation], [YearsOfExperience] from [Doctor]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while ( reader.Read() )
            {
                result.Add( new Doctor(
                    Convert.ToInt32( reader[ "Id" ] ),
                    Convert.ToString( reader[ "Name" ] ),
                    Convert.ToString( reader[ "Specialisation" ] ),
                    Convert.ToInt32( reader[ "YearsOfExperience" ] )
                ) );
            }

            return result;
        }

        public IReadOnlyList<Doctor> GetAllBySpecialisation( string specialisation )
        {
            var result = new List<Doctor>();

            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = $"select [Id], [Name], [Specialisation], [YearsOfExperience] from [Doctor] where [Specialisation] = @specialisation";
            sqlCommand.Parameters.Add( "@specialisation", SqlDbType.NVarChar, 255 ).Value = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(specialisation);

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while ( reader.Read() )
            {
                result.Add( new Doctor(
                    Convert.ToInt32( reader[ "Id" ] ),
                    Convert.ToString( reader[ "Name" ] ),
                    Convert.ToString( reader[ "Specialisation" ] ),
                    Convert.ToInt32( reader[ "YearsOfExperience" ] )
                ) );
            }

            return result;
        }

        public Doctor GetByName( string name )
        {
            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Name], [Specialisation], [YearsOfExperience] from [Doctor] where [Name] = @name";
            sqlCommand.Parameters.Add( "@name", SqlDbType.NVarChar, 255 ).Value = name;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if ( reader.Read() )
            {
                return new Doctor(
                    Convert.ToInt32( reader[ "Id" ] ),
                    Convert.ToString( reader[ "Name" ] ),
                    Convert.ToString( reader[ "Specialisation" ] ),
                    Convert.ToInt32( reader[ "YearsOfExperience" ] )
                );
            }
            else
            {
                return null;
            }
        }

        public Doctor GetById( int id )
        {
            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [Id], [Name], [Specialisation], [YearsOfExperience] from [Doctor] where [Id] = @id";
            sqlCommand.Parameters.Add( "@id", SqlDbType.Int ).Value = id;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if ( reader.Read() )
            {
                return new Doctor(
                    Convert.ToInt32( reader[ "Id" ] ),
                    Convert.ToString( reader[ "Name" ] ),
                    Convert.ToString( reader[ "Specialisation" ] ),
                    Convert.ToInt32( reader[ "YearsOfExperience" ] )
                );
            }
            else
            {
                return null;
            }
        }

        public void Delete( Doctor doctor )
        {
            if ( doctor == null )
            {
                throw new ArgumentNullException( nameof( doctor ) );
            }

            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "delete [Doctor] where [Id] = @id";
            sqlCommand.Parameters.Add( "@id", SqlDbType.Int ).Value = doctor.Id;
            sqlCommand.ExecuteNonQuery();
        }

        public void Update( Doctor doctor )
        {
            if ( doctor == null )
            {
                throw new ArgumentNullException( nameof( doctor ) );
            }

            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "update [Doctor] set [Name] = @name, [Specialisation] = @specialisation, [YearsOfExperience] = @yearsOfExperience where [Id] = @id";
            sqlCommand.Parameters.Add( "@id", SqlDbType.Int ).Value = doctor.Id;
            sqlCommand.Parameters.Add( "@name", SqlDbType.NVarChar, 255 ).Value = doctor.Name;
            sqlCommand.Parameters.Add( "@specialisation", SqlDbType.NVarChar, 255 ).Value = doctor.Specialisation;
            sqlCommand.Parameters.Add( "@yearsOfExperience", SqlDbType.Int ).Value = doctor.YearsOfExperiene;
            sqlCommand.ExecuteNonQuery();
        }

        public void PrintLoad()
        {
            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [DoctorId], count(*) as Count from [Voucher] group by [DoctorId]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while ( reader.Read() )
            {
                Console.WriteLine($"ID врача: {reader[ "DoctorId" ]}, количество приёмов: {reader[ "Count" ]}");
            }
        }
    }
}
