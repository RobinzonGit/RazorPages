using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Models;
using BlazorAcademyHWApp.Data;

namespace BlazorAcademyHWApp.Pages.DirectionPages;

public class EditModel : PageModel
{
    private readonly BlazorAcademyHWContext _context;

    public EditModel(BlazorAcademyHWContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Direction Direction { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var direction = await _context.Directions.FirstOrDefaultAsync(m => m.Id == id);
        if (direction is null)
        {
            return NotFound();
        }
        Direction = direction;
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

        _context.Attach(Direction).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DirectionExists(Direction.Id))
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

    private bool DirectionExists(int id)
    {
        return _context.Directions.Any(e => e.Id == id);
    }
}
