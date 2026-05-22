using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Enrollments
{
    public class EditModel : PageModel
    {
        private readonly ContosoUniversityContext _context;

        public EditModel(ContosoUniversityContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Enrollment Enrollment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (enrollment == null) return NotFound();

            Enrollment = enrollment;

            // Исправлено: CourseID вместо Id
            ViewData["CourseID"] = new SelectList(await _context.Courses.ToListAsync(), "CourseID", "Title", Enrollment.CourseID);
            ViewData["StudentID"] = new SelectList(await _context.Students.ToListAsync(), "Id", "FullName", Enrollment.StudentID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CourseID"] = new SelectList(await _context.Courses.ToListAsync(), "CourseID", "Title", Enrollment.CourseID);
                ViewData["StudentID"] = new SelectList(await _context.Students.ToListAsync(), "Id", "FullName", Enrollment.StudentID);
                return Page();
            }

            _context.Attach(Enrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(Enrollment.EnrollmentID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentID == id);
        }
    }
}