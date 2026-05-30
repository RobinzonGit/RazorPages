using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.DisciplinePages;

public class CreateModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;

    public CreateModel(BlazorAcademyHWContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Discipline Discipline { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Disciplines.Add(Discipline);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
