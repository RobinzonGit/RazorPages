using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Models;
using UniversityApp.Data;

namespace UniversityApp.Pages.GroupPages;

public class DetailsModel : PageModel
{
    private readonly UniversityContext _context;
    public DetailsModel(UniversityContext context)
    {
        _context = context;
    }

    public Group Group { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var group = await _context.Groups.FirstOrDefaultAsync(m => m.Id == id);
        if (group is null)
        {
            return NotFound();
        }
        else
        {
            Group = group;
        }

        return Page();
    }
}
