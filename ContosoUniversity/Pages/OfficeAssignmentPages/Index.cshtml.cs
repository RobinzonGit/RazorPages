using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Data;

namespace ContosoUniversity.Pages.OfficeAssignmentPages;

public class IndexModel : PageModel
{
    private readonly ContosoUniversityContext _context;

    public IndexModel(ContosoUniversityContext context)
    {
        _context = context;
    }

    public IList<OfficeAssignment> OfficeAssignment { get; set; } = default!;

    public async Task OnGetAsync()
    {
        OfficeAssignment = await _context.OfficeAssignments.ToListAsync();
    }
}
