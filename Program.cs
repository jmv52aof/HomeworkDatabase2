using HomeworkDatabase2.Repositories;
using HomeworkDatabase2.Models;

const string connectionString = @"Server=debian;Database=clinic;User Id=sa;Password=a12345678A;";

IPatientRepository patientRepository = new RawSqlPatientRepository( connectionString );
IDoctorRepository doctorRepository = new RawSqlDoctorRepository( connectionString );
IVoucherRepository voucherRepository = new RawSqlVoucherRepository( connectionString ); 

while ( true )
{
    Console.Write("Введите команду: ");
    string command = Console.ReadLine().ToLower();
    if ( command == "exit") 
    {
        return;
    }
    if ( command == "print-vouchers" )
    {
        IReadOnlyList<Voucher> vouchers = voucherRepository.GetAll();
        if (vouchers.Count == 0 ) 
        {
            Console.WriteLine("Нет талонов");
        } 
        else 
        {
            foreach (var voucher in vouchers) 
            {
                Console.WriteLine($"ID: {voucher.Id}. Талон на: {voucher.ReceptionTime}, ID врача: {voucher.DoctorId}, ID пациента: {voucher.PatientId}");
            }
        }
    }
    if (command == "get-doctors-by-specialisation")
    {
        Console.Write("Введите специализацию: ");
        string specialisation = Console.ReadLine();
        IReadOnlyList<Doctor> doctors = doctorRepository.GetAllBySpecialisation(specialisation);
        if ( doctors.Count == 0 )
        {
            Console.WriteLine("Нет врачей с такой специализацией: " + specialisation);
        }
        else
        {
            foreach (var doctor in doctors)
            {
                Console.WriteLine($"ID доктора: {doctor.Id}, Имя: {doctor.Name}, Специализация: {doctor.Specialisation}, Лет стажа: {doctor.YearsOfExperiene}");
            }
        }
    }
    if ( command == "update-doctor-info" )
    {
        Console.Write("Введите ID: ");
        int id;
        try
        {
            id = Int32.Parse(Console.ReadLine());
        }
        catch ( FormatException )
        {
            Console.WriteLine( "ОШИБКА: ID должен быть числом" );
            continue;
        }

        Doctor doctor = doctorRepository.GetById(id);
        if ( doctor == null )
        {
            Console.WriteLine( "ОШИБКА: Нет врача с таким ID ");
            continue;
        }
        
        Console.Write("Введите новое имя: ");
        string newName = Console.ReadLine();
        Console.Write("Введите новую специализацию: ");
        string newSpecialisation = Console.ReadLine();
        Console.Write("Введите количество лет опыта: ");
        int newYearsOfExperience;
        try
        {
            newYearsOfExperience = Int32.Parse(Console.ReadLine());
        }
        catch ( FormatException )
        {
            Console.WriteLine( "ОШИБКА: Количество лет стажа должно быть числом" );
            continue;
        }

        doctor.UpdateName( newName );
        doctor.UpdateSpecialisation( newSpecialisation );
        doctor.UpdateYearsOfExperience( newYearsOfExperience );
        doctorRepository.Update( doctor );
    }
    if ( command == "remove-voucher-by-id" )
    {
        Console.Write( "Введите ID: ");
        int id;
        try
        {
            id = Int32.Parse(Console.ReadLine());
        }
        catch ( FormatException )
        {
            Console.WriteLine( "ОШИБКА: ID должен быть числом" );
            continue;
        }
        Voucher voucher = voucherRepository.GetById(id);
        if ( voucher == null )
        {
            Console.WriteLine( "ОШИБКА: Нет талона с таким ID ");
            continue;
        }
        
        voucherRepository.Delete( voucher );
    }
    if ( command == "get-doctors-load" )
    {
        doctorRepository.PrintLoad();
    }
}