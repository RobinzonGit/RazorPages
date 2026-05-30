using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.TeacherPages;

public class DetailsModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;
    public DetailsModel(BlazorAcademyHWContext context)
    {
        _context = context;
    }

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
}
