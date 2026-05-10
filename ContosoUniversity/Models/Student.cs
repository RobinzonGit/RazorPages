using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        // Calculated propetes
        public string FullName { get => $"{LastName} {FirstName}"; }    

        public ICollection<Enrollment> Enrollment { get; set; }


    }
}
