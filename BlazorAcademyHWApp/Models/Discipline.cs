using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAcademyHWApp.Models
{
    [Table("Disciplines")]
    public class Discipline
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public int? TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher? Teacher { get; set; }

        public int? Credits { get; set; }
    }
}