using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.TeacherPages;

public class IndexModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;

    public IndexModel(BlazorAcademyHWContext context)
    {
        _context = context;
    }

    public IList<Teacher> Teacher { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Teacher = await _context.Teachers.ToListAsync();
    }
}
