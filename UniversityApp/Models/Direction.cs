using System.ComponentModel.DataAnnotations;

namespace UniversityApp.Models
{
    public class Direction
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Code { get; set; }

        public int? DurationYears { get; set; }
    }
}