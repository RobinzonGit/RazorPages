namespace BlazorAcademyHW.Models
{
    public class TeacherDiscipline
    {
        public int TeacherId { get; set; }
        public Teachers Teacher { get; set; } = null!;
        public int DisciplineId { get; set; }
        public Disciplines Discipline { get; set; } = null!;
    }
}