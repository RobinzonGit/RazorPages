using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.DisciplinePages;

public class DetailsModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;
    public DetailsModel(BlazorAcademyHWContext context)
    {
        _context = context;
    }

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
}
