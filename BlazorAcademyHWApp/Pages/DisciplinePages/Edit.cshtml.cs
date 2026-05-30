using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.DisciplinePages;

public class EditModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;

    public EditModel(BlazorAcademyHWContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Discipline Discipline { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var discipline = await _context.Disciplines.FirstOrDefaultAsync(m => m.Id == id);
        if (discipline is null)
        {
            return NotFound();
        }
        Discipline = discipline;
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

        _context.Attach(Discipline).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DisciplineExists(Discipline.Id))
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

    private bool DisciplineExists(int id)
    {
        return _context.Disciplines.Any(e => e.Id == id);
    }
}
