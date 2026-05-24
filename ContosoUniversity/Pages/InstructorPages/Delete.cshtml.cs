using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Data;

namespace ContosoUniversity.Pages.InstructorPages;

public class DeleteModel : PageModel
{
    private readonly ContosoUniversityContext _context;

    public DeleteModel(ContosoUniversityContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Instructor Instructor { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.Id == id);
        if (instructor is null)
        {
            return NotFound();
        }
        else
        {
            Instructor = instructor;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var instructor = await _context.Instructors.FindAsync(id);
        if (instructor != null)
        {
            Instructor = instructor;
            _context.Instructors.Remove(Instructor);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
