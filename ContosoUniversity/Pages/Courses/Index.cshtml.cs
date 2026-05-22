using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.Extensions.Configuration;

namespace ContosoUniversity.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.ContosoUniversityContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(ContosoUniversity.Data.ContosoUniversityContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Свойства сортировки
        public string TitleSort { get; set; }
        public string CreditsSort { get; set; }
        public string CurrentSort { get; set; }

        // Поиск
        public string CurrentFilter { get; set; }

        // Размер страницы
        public int PageSize { get; set; }

        // Пагинированный список курсов
        public PaginatedList<Course> Courses { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString, int? pageIndex, int? pageSize)
        {
            CurrentSort = sortOrder;

            // Определение параметров сортировки
            TitleSort = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            CreditsSort = sortOrder == "Credits" ? "credits_desc" : "Credits";

            // Фильтр поиска
            if (searchString != null)
                pageIndex = 1;
            else
                searchString = CurrentFilter;
            CurrentFilter = searchString;

            // Размер страницы (из параметра или конфигурации, по умолчанию 5)
            PageSize = pageSize ?? _configuration.GetValue("PageSize", 5);

            // Базовый запрос
            IQueryable<Course> coursesQuery = _context.Courses;

            // Поиск по названию курса
            if (!string.IsNullOrEmpty(searchString))
            {
                coursesQuery = coursesQuery.Where(c => c.Title.Contains(searchString));
            }

            // Сортировка
            switch (sortOrder)
            {
                case "title_desc":
                    coursesQuery = coursesQuery.OrderByDescending(c => c.Title);
                    break;
                case "Credits":
                    coursesQuery = coursesQuery.OrderBy(c => c.Credits);
                    break;
                case "credits_desc":
                    coursesQuery = coursesQuery.OrderByDescending(c => c.Credits);
                    break;
                default:
                    coursesQuery = coursesQuery.OrderBy(c => c.Title);
                    break;
            }

            // Пагинация
            Courses = await PaginatedList<Course>.CreateAsync(
                coursesQuery.AsNoTracking(),
                pageIndex ?? 1,
                PageSize);
        }
    }
}