using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Data;

namespace ContosoUniversity.Pages.OfficeAssignmentPages;

public class DeleteModel : PageModel
{
    private readonly ContosoUniversityContext _context;

    public DeleteModel(ContosoUniversityContext context)
    {
        _context = context;
    }

    [BindProperty]
    public OfficeAssignment OfficeAssignment { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? instructorid)
    {
        if (instructorid is null)
        {
            return NotFound();
        }

        var officeassignment = await _context.OfficeAssignments.FirstOrDefaultAsync(m => m.InstructorId == instructorid);
        if (officeassignment is null)
        {
            return NotFound();
        }
        else
        {
            OfficeAssignment = officeassignment;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? instructorid)
    {
        if (instructorid is null)
        {
            return NotFound();
        }

        var officeassignment = await _context.OfficeAssignments.FindAsync(instructorid);
        if (officeassignment != null)
        {
            OfficeAssignment = officeassignment;
            _context.OfficeAssignments.Remove(OfficeAssignment);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
