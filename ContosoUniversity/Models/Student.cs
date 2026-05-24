using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Фамилия - это обязательное поле")]
        [StringLength(50, ErrorMessage = " Превышенно максимальное количество символов")]
        [RegularExpression(@"^[A-ZА-Я][a-zа-я]*$", ErrorMessage = "Фамилия может включать в себя только символы Русского и Латинского алфавита, а так же должна быть написана с большой буквы")]
        [Display(Name = "Имя")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Фамилия - это обязательное поле")]
        [StringLength(50, ErrorMessage = "Превышенно максимальное количество символов")]
        [RegularExpression(@"^[A-ZА-Я][a-zа-я]*$", ErrorMessage = "Имя может включать в себя только символы Русского и Латинского алфавита, а так же должна быть написана с большой буквы")]
        [Display(Name = "Фамилия")]

        public string FirstName { get; set; }

        [Display(Name = "Студент")]
        // Добавьте это свойство
        public string FullName => $"{LastName} {FirstName}";

        [DataType(DataType.Date)]
        [Display(Name = "Дата поступления")]
        public System.DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
