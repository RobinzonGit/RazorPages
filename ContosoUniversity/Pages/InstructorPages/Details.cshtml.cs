using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Data;

namespace ContosoUniversity.Pages.InstructorPages;

public class DetailsModel : PageModel
{
    private readonly ContosoUniversityContext _context;
    public DetailsModel(ContosoUniversityContext context)
    {
        _context = context;
    }

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
}
