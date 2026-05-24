using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Data;

namespace ContosoUniversity.Pages.OfficeAssignmentPages;

public class DetailsModel : PageModel
{
    private readonly ContosoUniversityContext _context;
    public DetailsModel(ContosoUniversityContext context)
    {
        _context = context;
    }

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
}
