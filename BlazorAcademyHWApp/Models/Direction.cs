using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAcademyHWApp.Models
{
    [Table("Directions")]
    public class Direction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Code { get; set; }

        public int? DurationYears { get; set; }
    }
}