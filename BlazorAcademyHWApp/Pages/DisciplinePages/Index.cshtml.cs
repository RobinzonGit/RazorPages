using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Data;
using BlazorAcademyHWApp.Models;

namespace BlazorAcademyHWApp.Pages.DisciplinePages
{
    public class IndexModel : PageModel
    {
        private readonly BlazorAcademyHWContext _context;
        private const int PageSize = 5;

        public IndexModel(BlazorAcademyHWContext context)
        {
            _context = context;
        }

        public List<Discipline> Disciplines { get; set; } = new();
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }
        public string CurrentSort { get; set; } = "id_asc";

        public async Task OnGetAsync(int? pageNumber, string sortOrder)
        {
            PageNumber = pageNumber ?? 1;
            CurrentSort = sortOrder ?? CurrentSort;

            IQueryable<Discipline> query = _context.Disciplines
                .Include(d => d.Teacher); // Подгружаем преподавателя

            // Сортировка
            query = CurrentSort switch
            {
                "name_asc" => query.OrderBy(d => d.Name),
                "name_desc" => query.OrderByDescending(d => d.Name),
                "teacher_asc" => query.OrderBy(d => d.Teacher != null ? d.Teacher.LastName : ""),
                "teacher_desc" => query.OrderByDescending(d => d.Teacher != null ? d.Teacher.LastName : ""),
                "credits_asc" => query.OrderBy(d => d.Credits),
                "credits_desc" => query.OrderByDescending(d => d.Credits),
                "id_desc" => query.OrderByDescending(d => d.Id),
                _ => query.OrderBy(d => d.Id)
            };

            int totalCount = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            Disciplines = await query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}