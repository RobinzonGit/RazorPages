using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Data;
using BlazorAcademyHWApp.Models;

namespace BlazorAcademyHWApp.Pages.TeacherPages
{
    public class IndexModel : PageModel
    {
        private readonly BlazorAcademyHWContext _context;
        private const int PageSize = 5;

        public IndexModel(BlazorAcademyHWContext context)
        {
            _context = context;
        }

        public List<Teacher> Teachers { get; set; } = new();
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }
        public string CurrentSort { get; set; } = "id_asc";

        public async Task OnGetAsync(int? pageNumber, string sortOrder)
        {
            PageNumber = pageNumber ?? 1;
            CurrentSort = sortOrder ?? CurrentSort;

            IQueryable<Teacher> query = _context.Teachers;

            query = CurrentSort switch
            {
                "last_asc" => query.OrderBy(t => t.LastName),
                "last_desc" => query.OrderByDescending(t => t.LastName),
                "first_asc" => query.OrderBy(t => t.FirstName),
                "first_desc" => query.OrderByDescending(t => t.FirstName),
                "email_asc" => query.OrderBy(t => t.Email),
                "email_desc" => query.OrderByDescending(t => t.Email),
                "id_desc" => query.OrderByDescending(t => t.Id),
                _ => query.OrderBy(t => t.Id)
            };

            int totalCount = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            Teachers = await query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}