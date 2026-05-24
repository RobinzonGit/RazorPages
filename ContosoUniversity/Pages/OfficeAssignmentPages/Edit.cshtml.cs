using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Data;

namespace ContosoUniversity.Pages.OfficeAssignmentPages;

public class EditModel : PageModel
{
    private readonly ContosoUniversityContext _context;

    public EditModel(ContosoUniversityContext context)
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
        OfficeAssignment = officeassignment;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(OfficeAssignment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OfficeAssignmentExists(OfficeAssignment.InstructorId))
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

    private bool OfficeAssignmentExists(int instructorid)
    {
        return _context.OfficeAssignments.Any(e => e.InstructorId == instructorid);
    }
}
