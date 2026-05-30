using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Data;
using BlazorAcademyHWApp.Models;

namespace BlazorAcademyHWApp.Pages.GroupPages
{
    public class IndexModel : PageModel
    {
        private readonly BlazorAcademyHWContext _context;
        private const int PageSize = 5;

        public IndexModel(BlazorAcademyHWContext context)
        {
            _context = context;
        }

        public List<Group> Groups { get; set; } = new();
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }
        public string CurrentSort { get; set; } = "id_asc";

        public async Task OnGetAsync(int? pageNumber, string sortOrder)
        {
            PageNumber = pageNumber ?? 1;
            CurrentSort = sortOrder ?? CurrentSort;

            IQueryable<Group> query = _context.Groups
                .Include(g => g.Direction); // Подгружаем связанное направление

            // Применяем сортировку
            query = CurrentSort switch
            {
                "name_asc" => query.OrderBy(g => g.Name),
                "name_desc" => query.OrderByDescending(g => g.Name),
                "dir_asc" => query.OrderBy(g => g.Direction != null ? g.Direction.Name : ""),
                "dir_desc" => query.OrderByDescending(g => g.Direction != null ? g.Direction.Name : ""),
                "year_asc" => query.OrderBy(g => g.YearOfStudy),
                "year_desc" => query.OrderByDescending(g => g.YearOfStudy),
                "id_desc" => query.OrderByDescending(g => g.Id),
                _ => query.OrderBy(g => g.Id) // id_asc по умолчанию
            };

            int totalCount = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            Groups = await query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}