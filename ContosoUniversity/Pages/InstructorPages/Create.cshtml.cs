using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Data;

namespace ContosoUniversity.Pages.InstructorPages;

public class CreateModel : PageModel
{
    private readonly ContosoUniversityContext _context;

    public CreateModel(ContosoUniversityContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Instructor Instructor { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Instructors.Add(Instructor);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
