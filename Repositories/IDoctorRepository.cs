using HomeworkDatabase2.Models;

namespace HomeworkDatabase2.Repositories
{
    public interface IDoctorRepository
    {
        IReadOnlyList<Doctor> GetAll();
        IReadOnlyList<Doctor> GetAllBySpecialisation( string specialisation );
        Doctor GetByName( string name );
        Doctor GetById( int id );
        void PrintLoad();
        void Update( Doctor doctor );
        void Delete( Doctor doctor );
    }
}
