using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Enrollments
{
    public class CreateModel : PageModel
    {
        private readonly ContosoUniversityContext _context;

        public CreateModel(ContosoUniversityContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Enrollment Enrollment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            // Используем правильное имя свойства CourseID и FullName (должно быть в Student)
            ViewData["CourseID"] = new SelectList(await _context.Courses.ToListAsync(), "CourseID", "Title");
            ViewData["StudentID"] = new SelectList(await _context.Students.ToListAsync(), "Id", "FullName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Повторно заполняем ViewBag при ошибке
                ViewData["CourseID"] = new SelectList(await _context.Courses.ToListAsync(), "CourseID", "Title", Enrollment.CourseID);
                ViewData["StudentID"] = new SelectList(await _context.Students.ToListAsync(), "Id", "FullName", Enrollment.StudentID);
                return Page();
            }

            _context.Enrollments.Add(Enrollment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}