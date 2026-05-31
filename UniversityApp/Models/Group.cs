using System.ComponentModel.DataAnnotations;

namespace UniversityApp.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public int DirectionId { get; set; }
        public Direction? Direction { get; set; }

        public int? YearOfStudy { get; set; }
    }
}