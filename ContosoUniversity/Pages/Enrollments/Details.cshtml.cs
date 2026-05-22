using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Enrollments
{
    public class DetailsModel : PageModel
    {
        private readonly ContosoUniversityContext _context;

        public DetailsModel(ContosoUniversityContext context)
        {
            _context = context;
        }

        public Enrollment Enrollment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)   // загружаем связанный курс
                .Include(e => e.Student)  // загружаем связанного студента
                .FirstOrDefaultAsync(m => m.EnrollmentID == id);

            if (enrollment == null) return NotFound();

            Enrollment = enrollment;
            return Page();
        }
    }
}