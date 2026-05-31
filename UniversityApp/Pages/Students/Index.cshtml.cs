using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Data;
using UniversityApp.Models;

namespace UniversityApp.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly UniversityContext _context;
        private const int PageSize = 5;

        public IndexModel(UniversityContext context) => _context = context;

        public List<Student> Students { get; set; } = new();
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }
        public string CurrentSort { get; set; } = "id_asc";

        public async Task OnGetAsync(int? pageNumber, string sortOrder)
        {
            PageNumber = pageNumber ?? 1;
            CurrentSort = sortOrder ?? CurrentSort;

            IQueryable<Student> query = _context.Students.Include(s => s.Group);

            query = CurrentSort switch
            {
                "last_asc" => query.OrderBy(s => s.LastName),
                "last_desc" => query.OrderByDescending(s => s.LastName),
                "first_asc" => query.OrderBy(s => s.FirstName),
                "first_desc" => query.OrderByDescending(s => s.FirstName),
                "group_asc" => query.OrderBy(s => s.Group != null ? s.Group.Name : ""),
                "group_desc" => query.OrderByDescending(s => s.Group != null ? s.Group.Name : ""),
                "id_desc" => query.OrderByDescending(s => s.Id),
                _ => query.OrderBy(s => s.Id)
            };

            int total = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(total / (double)PageSize);

            Students = await query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}