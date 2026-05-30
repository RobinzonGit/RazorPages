using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Data;
using BlazorAcademyHWApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAcademyHWApp.Pages.StudentPages
{
    public class IndexModel : PageModel
    {
        private readonly BlazorAcademyHWContext _context;

        public IndexModel(BlazorAcademyHWContext context)
        {
            _context = context;
        }

        public List<Student> Students { get; set; } = new();
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }
        public string CurrentSort { get; set; } = "id_asc";
        private const int PageSize = 5;   // Количество записей на странице

        public async Task OnGetAsync(int? pageNumber, string sortOrder)
        {
            PageNumber = pageNumber ?? 1;
            CurrentSort = sortOrder ?? CurrentSort;

            IQueryable<Student> query = _context.Students
                .Include(s => s.Group);   // Подгружаем группу для отображения её названия

            // Применяем сортировку
            query = CurrentSort switch
            {
                "name_asc" => query.OrderBy(s => s.FirstName),
                "name_desc" => query.OrderByDescending(s => s.FirstName),
                "last_asc" => query.OrderBy(s => s.LastName),
                "last_desc" => query.OrderByDescending(s => s.LastName),
                "group_asc" => query.OrderBy(s => s.Group != null ? s.Group.Name : ""),
                "group_desc" => query.OrderByDescending(s => s.Group != null ? s.Group.Name : ""),
                "id_desc" => query.OrderByDescending(s => s.Id),
                _ => query.OrderBy(s => s.Id),   // id_asc по умолчанию
            };

            // Получаем общее количество записей
            int totalCount = await query.CountAsync();

            // Вычисляем общее количество страниц
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            // Применяем пагинацию
            Students = await query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}