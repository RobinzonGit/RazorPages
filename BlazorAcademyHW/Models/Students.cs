using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAcademyHW.Models
{
    public class Students
    {
        public int Id { get; set; }
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public byte[]? Photo { get; set; }
        public int? GroupId { get; set; }
        [ForeignKey(nameof(GroupId))]
        public virtual Groups? Group { get; set; }

        [NotMapped]
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age))
                    age--;
                return age;
            }
        }

        [NotMapped]
        public string? DirectionName => Group?.Direction?.Name;

        [NotMapped]
        public string? GroupName => Group?.Name;
    }
}