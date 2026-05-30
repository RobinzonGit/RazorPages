using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.GroupPages;

public class DeleteModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;

    public DeleteModel(BlazorAcademyHWContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var group = await _context.Groups.FindAsync(id);
        if (group != null)
        {
            Group = group;
            _context.Groups.Remove(Group);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
