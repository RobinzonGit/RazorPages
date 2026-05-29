using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAcademyHWApp.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public Group? Group { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Email { get; set; }
    }
}