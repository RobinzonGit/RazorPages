using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAcademyHW.Models
{
    public class Groups
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; } = string.Empty;

        // Foreign key to Directions
        public int DirectionId { get; set; }
        [ForeignKey(nameof(DirectionId))]
        public virtual Directions? Direction { get; set; }

        // Study days stored as comma-separated numbers (0=Monday ... 6=Sunday)
        public string? StudyDays { get; set; }

        // Navigation property for students
        public virtual ICollection<Students> Students { get; set; } = new List<Students>();
    }
}