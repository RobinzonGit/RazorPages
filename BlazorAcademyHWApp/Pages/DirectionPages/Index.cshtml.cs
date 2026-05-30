using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.DirectionPages;

public class IndexModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;

    public IndexModel(BlazorAcademyHWContext context)
    {
        _context = context;
    }

    public IList<Direction> Direction { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Direction = await _context.Directions.ToListAsync();
    }
}
