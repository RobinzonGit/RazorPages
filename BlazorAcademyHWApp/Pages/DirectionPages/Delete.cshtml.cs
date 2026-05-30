using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.DirectionPages;

public class DeleteModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;

    public DeleteModel(BlazorAcademyHWContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Direction Direction { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var direction = await _context.Directions.FirstOrDefaultAsync(m => m.Id == id);
        if (direction is null)
        {
            return NotFound();
        }
        else
        {
            Direction = direction;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var direction = await _context.Directions.FindAsync(id);
        if (direction != null)
        {
            Direction = direction;
            _context.Directions.Remove(Direction);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
