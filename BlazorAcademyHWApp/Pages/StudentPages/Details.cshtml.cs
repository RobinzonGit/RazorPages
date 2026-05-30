using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.StudentPages;

public class DetailsModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;
    public DetailsModel(BlazorAcademyHWContext context)
    {
        _context = context;
    }

    public Student Student { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
        if (student is null)
        {
            return NotFound();
        }
        else
        {
            Student = student;
        }

        return Page();
    }
}
