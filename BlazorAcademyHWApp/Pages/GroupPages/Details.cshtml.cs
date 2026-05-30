using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.GroupPages;

public class DetailsModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;
    public DetailsModel(BlazorAcademyHWContext context)
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
