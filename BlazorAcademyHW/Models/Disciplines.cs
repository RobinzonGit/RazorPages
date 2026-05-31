using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlazorAcademyHW.Models
{
    public class Disciplines
    {
        public int Id { get; set; }
        [Required] public string? Name { get; set; } = string.Empty;
        public int Lessons { get; set; }

        [JsonIgnore]
        public virtual ICollection<TeacherDiscipline> TeacherDisciplines { get; set; } = new List<TeacherDiscipline>();
    }
}