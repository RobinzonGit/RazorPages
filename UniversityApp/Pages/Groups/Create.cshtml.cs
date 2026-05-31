using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Models;
using UniversityApp.Data;

namespace UniversityApp.Pages.GroupPages;

public class CreateModel : PageModel
{
    private readonly UniversityContext _context;

    public CreateModel(UniversityContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Group Group { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Groups.Add(Group);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
