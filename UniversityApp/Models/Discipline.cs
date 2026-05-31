using System.ComponentModel.DataAnnotations;

namespace UniversityApp.Models
{
    public class Discipline
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        public int? Credits { get; set; }
    }
}