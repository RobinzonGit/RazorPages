using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UniversityApp.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        public int GroupId { get; set; }
        public Group? Group { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}