namespace HomeworkDatabase2.Models
{
    public class Patient
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string PhoneNumber { get; private set; }

        public Patient( int id, string name, DateTime birthdate, string phoneNumber)
        {
            Id = id;
            Name = name;
            Birthdate = birthdate;
            PhoneNumber = phoneNumber;
        }

        public void UpdateName( string newName )
        {
            Name = newName;
        }

        public void UpdateBirthdate( DateTime birthDate )
        {
            Birthdate = birthDate;
        }

        public void UpdatePhonenumber( String phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
