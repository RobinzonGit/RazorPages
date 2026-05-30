using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.DisciplinePages;

public class DeleteModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;

    public DeleteModel(BlazorAcademyHWContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Discipline Discipline { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var discipline = await _context.Disciplines.FirstOrDefaultAsync(m => m.Id == id);
        if (discipline is null)
        {
            return NotFound();
        }
        else
        {
            Discipline = discipline;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var discipline = await _context.Disciplines.FindAsync(id);
        if (discipline != null)
        {
            Discipline = discipline;
            _context.Disciplines.Remove(Discipline);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
