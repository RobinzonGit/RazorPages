using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public System.DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        // Добавьте это свойство
        public string FullName => $"{LastName} {FirstName}";
    }
}
