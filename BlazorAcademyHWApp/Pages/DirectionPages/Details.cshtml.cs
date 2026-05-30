using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.DirectionPages;

public class DetailsModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;
    public DetailsModel(BlazorAcademyHWContext context)
    {
        _context = context;
    }

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
}
