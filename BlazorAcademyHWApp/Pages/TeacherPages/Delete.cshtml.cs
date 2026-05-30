using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.TeacherPages;

public class DeleteModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;

    public DeleteModel(BlazorAcademyHWContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Teacher Teacher { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var teacher = await _context.Teachers.FirstOrDefaultAsync(m => m.Id == id);
        if (teacher is null)
        {
            return NotFound();
        }
        else
        {
            Teacher = teacher;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher != null)
        {
            Teacher = teacher;
            _context.Teachers.Remove(Teacher);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
