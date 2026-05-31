using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Models;
using UniversityApp.Data;

namespace UniversityApp.Pages.Students;

public class DeleteModel : PageModel
{
    private readonly UniversityContext _context;

    public DeleteModel(UniversityContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Student Student { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
        if (student is null)
        {
            return NotFound();
        }
        else
        {
            Student = student;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            Student = student;
            _context.Students.Remove(Student);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
