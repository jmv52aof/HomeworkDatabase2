namespace HomeworkDatabase2.Models
{
    public class Voucher
    {
        public int Id { get; private set; }
        public DateTime ReceptionTime { get; private set; }
        public int DoctorId { get; private set; }
        public int PatientId { get; private set; }

        public Voucher( int id, DateTime receptionTime, int doctorId, int patientId )
        {
            Id = id;
            ReceptionTime = receptionTime;
            DoctorId = doctorId;
            PatientId = patientId;
        }
    }
}
