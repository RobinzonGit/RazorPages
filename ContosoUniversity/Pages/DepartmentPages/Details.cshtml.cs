using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Data;

namespace ContosoUniversity.Pages.DepartmentPages;

public class DetailsModel : PageModel
{
    private readonly ContosoUniversityContext _context;
    public DetailsModel(ContosoUniversityContext context)
    {
        _context = context;
    }

    public Department Department { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? departmentid)
    {
        if (departmentid is null)
        {
            return NotFound();
        }

        var department = await _context.Departments.FirstOrDefaultAsync(m => m.DepartmentID == departmentid);
        if (department is null)
        {
            return NotFound();
        }
        else
        {
            Department = department;
        }

        return Page();
    }
}
