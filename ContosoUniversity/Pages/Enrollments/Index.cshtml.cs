using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.Extensions.Configuration; // для configuration (если нужно значение по умолчанию)

namespace ContosoUniversity.Pages.Enrollments
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

        // Свойства для сортировки
        public string GradeSort { get; set; }
        public string CourseSort { get; set; }
        public string StudentSort { get; set; }
        public string CurrentSort { get; set; }

        // Фильтр поиска
        public string CurrentFilter { get; set; }

        // Размер страницы
        public int PageSize { get; set; }

        // Данные с пагинацией
        public PaginatedList<Enrollment> Enrollments { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString, int? pageIndex, int? pageSize)
        {
            CurrentSort = sortOrder;

            // Определяем порядок сортировки
            GradeSort = string.IsNullOrEmpty(sortOrder) ? "grade_desc" : "";
            CourseSort = sortOrder == "Course" ? "course_desc" : "Course";
            StudentSort = sortOrder == "Student" ? "student_desc" : "Student";

            // Поиск
            if (searchString != null)
                pageIndex = 1;
            else
                searchString = CurrentFilter;

            CurrentFilter = searchString;

            // Размер страницы (из параметра или из конфигурации, по умолчанию 5)
            PageSize = pageSize ?? _configuration.GetValue("PageSize", 5);

            // Базовый запрос с Include (нужны для отображения названий)
            IQueryable<Enrollment> enrollmentsQuery = _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student);

            // Поиск по студенту (содержит подстроку в полном имени)
            if (!string.IsNullOrEmpty(searchString))
            {
                enrollmentsQuery = enrollmentsQuery.Where(e =>
                    e.Student.LastName.Contains(searchString) ||
                    e.Student.FirstName.Contains(searchString));
            }

            // Сортировка
            switch (sortOrder)
            {
                case "grade_desc":
                    enrollmentsQuery = enrollmentsQuery.OrderByDescending(e => e.Grade);
                    break;
                case "Course":
                    enrollmentsQuery = enrollmentsQuery.OrderBy(e => e.Course.Title);
                    break;
                case "course_desc":
                    enrollmentsQuery = enrollmentsQuery.OrderByDescending(e => e.Course.Title);
                    break;
                case "Student":
                    enrollmentsQuery = enrollmentsQuery.OrderBy(e => e.Student.LastName).ThenBy(e => e.Student.FirstName);
                    break;
                case "student_desc":
                    enrollmentsQuery = enrollmentsQuery.OrderByDescending(e => e.Student.LastName).ThenByDescending(e => e.Student.FirstName);
                    break;
                default: // сортировка по Grade (по возрастанию)
                    enrollmentsQuery = enrollmentsQuery.OrderBy(e => e.Grade);
                    break;
            }

            // Пагинация
            Enrollments = await PaginatedList<Enrollment>.CreateAsync(
                enrollmentsQuery.AsNoTracking(),
                pageIndex ?? 1,
                PageSize);
        }
    }
}