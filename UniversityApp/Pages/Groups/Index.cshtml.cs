using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Models;
using UniversityApp.Data;

namespace UniversityApp.Pages.GroupPages;

public class IndexModel : PageModel
{
    private readonly UniversityContext _context;

    public IndexModel(UniversityContext context)
    {
        _context = context;
    }

    public IList<Group> Group { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Group = await _context.Groups.ToListAsync();
    }
}
