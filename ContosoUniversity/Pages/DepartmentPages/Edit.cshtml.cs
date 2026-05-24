using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Data;

namespace ContosoUniversity.Pages.DepartmentPages;

public class EditModel : PageModel
{
    private readonly ContosoUniversityContext _context;

    public EditModel(ContosoUniversityContext context)
    {
        _context = context;
    }

    [BindProperty]
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
        Department = department;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Department).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DepartmentExists(Department.DepartmentID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool DepartmentExists(int departmentid)
    {
        return _context.Departments.Any(e => e.DepartmentID == departmentid);
    }
}
