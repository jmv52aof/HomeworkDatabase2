using HomeworkDatabase2.Models;

namespace HomeworkDatabase2.Repositories
{
    public interface IPatientRepository
    {
        IReadOnlyList<Patient> GetAll();
        Patient GetByName( string name );
        Patient GetById( int id );
        void Update( Patient patient );
        void Delete( Patient patient );
    }
}
