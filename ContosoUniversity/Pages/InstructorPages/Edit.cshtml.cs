using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Data;

namespace ContosoUniversity.Pages.InstructorPages;

public class EditModel : PageModel
{
    private readonly ContosoUniversityContext _context;

    public EditModel(ContosoUniversityContext context)
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
        Instructor = instructor;
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

        _context.Attach(Instructor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!InstructorExists(Instructor.Id))
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

    private bool InstructorExists(int id)
    {
        return _context.Instructors.Any(e => e.Id == id);
    }
}
