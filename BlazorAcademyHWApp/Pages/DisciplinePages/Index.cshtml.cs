using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.DisciplinePages;

public class IndexModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;

    public IndexModel(BlazorAcademyHWContext context)
    {
        _context = context;
    }

    public IList<Discipline> Discipline { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Discipline = await _context.Disciplines.ToListAsync();
    }
}
