using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.ContosoUniversityContext _context;

        public IndexModel(ContosoUniversity.Data.ContosoUniversityContext context)
        {
            _context = context;
        }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CorentFilter { get; set; }
        public string CorentSort { get; set; }

        public IList<Models.Student> Students { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Models.Student> students = from s in _context.Students select s;
            switch (sortOrder)
            {
                case "name_desc": students = students.OrderByDescending(s => s.LastName); break;
                case "date_desc": students = students.OrderByDescending(s => s.EnrollmentDate); break;
                case "Date": students = students.OrderBy(s => s.EnrollmentDate); break;
                default: students = students.OrderBy(_ => _.LastName); break;
            }
            this.Students = await students. AsNoTracking().ToListAsync();
        }
    }
}
