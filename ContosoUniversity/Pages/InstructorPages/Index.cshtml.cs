using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Data;

namespace ContosoUniversity.Pages.InstructorPages;

public class IndexModel : PageModel
{
    private readonly ContosoUniversityContext _context;

    public IndexModel(ContosoUniversityContext context)
    {
        _context = context;
    }

    public IList<Instructor> Instructor { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Instructor = await _context.Instructors.ToListAsync();
    }
}
