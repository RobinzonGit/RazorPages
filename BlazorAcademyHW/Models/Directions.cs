using System.ComponentModel.DataAnnotations;

namespace BlazorAcademyHW.Models
{
    public class Directions
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; } = string.Empty;

        // Navigation property
        public virtual ICollection<Groups> Groups { get; set; } = new List<Groups>();
    }
}
