using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Data;
using BlazorAcademyHWApp.Models;

namespace BlazorAcademyHWApp.Pages.DirectionPages
{
    public class IndexModel : PageModel
    {
        private readonly BlazorAcademyHWContext _context;
        private const int PageSize = 5;

        public IndexModel(BlazorAcademyHWContext context)
        {
            _context = context;
        }

        public List<Direction> Directions { get; set; } = new();
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }
        public string CurrentSort { get; set; } = "id_asc";

        public async Task OnGetAsync(int? pageNumber, string sortOrder)
        {
            PageNumber = pageNumber ?? 1;
            CurrentSort = sortOrder ?? CurrentSort;

            IQueryable<Direction> query = _context.Directions;

            query = CurrentSort switch
            {
                "name_asc" => query.OrderBy(d => d.Name),
                "name_desc" => query.OrderByDescending(d => d.Name),
                "code_asc" => query.OrderBy(d => d.Code),
                "code_desc" => query.OrderByDescending(d => d.Code),
                "duration_asc" => query.OrderBy(d => d.DurationYears),
                "duration_desc" => query.OrderByDescending(d => d.DurationYears),
                "id_desc" => query.OrderByDescending(d => d.Id),
                _ => query.OrderBy(d => d.Id)
            };

            int totalCount = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            Directions = await query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}