using Org.BouncyCastle.Tls;

namespace StudentRegistration.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; }

    }
}
