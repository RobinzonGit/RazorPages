using Microsoft.EntityFrameworkCore;
using BlazorAcademyHW.Models;

namespace BlazorAcademyHW.Data
{
    public class BlazorAcademyHWContext : DbContext
    {
        public BlazorAcademyHWContext(DbContextOptions<BlazorAcademyHWContext> options)
            : base(options)
        {
        }

        public DbSet<Students> Students { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Directions> Directions { get; set; }
        public DbSet<Disciplines> Disciplines { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<TeacherDiscipline> TeacherDisciplines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Составной первичный ключ для таблицы многие-ко-многим
            modelBuilder.Entity<TeacherDiscipline>()
                .HasKey(td => new { td.TeacherId, td.DisciplineId });

            // Настройка связей
            modelBuilder.Entity<TeacherDiscipline>()
                .HasOne(td => td.Teacher)
                .WithMany(t => t.TeacherDisciplines)
                .HasForeignKey(td => td.TeacherId);

            modelBuilder.Entity<TeacherDiscipline>()
                .HasOne(td => td.Discipline)
                .WithMany(d => d.TeacherDisciplines)
                .HasForeignKey(td => td.DisciplineId);

            base.OnModelCreating(modelBuilder);
        }
    }
}