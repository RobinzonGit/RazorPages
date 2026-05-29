using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAcademyHWApp.Models
{
    [Table("Groups")]
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public int DirectionId { get; set; }

        [ForeignKey("DirectionId")]
        public Direction? Direction { get; set; }

        public int? YearOfStudy { get; set; }
    }
}