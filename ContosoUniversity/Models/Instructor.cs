using System.ComponentModel.DataAnnotations;
namespace ContosoUniversity.Models

{
    public class Instructor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Фамилия - это обязательное поле")]
        [StringLength(50, ErrorMessage = " Превышенно максимальное количество символов")]
        [RegularExpression(@"^[A-ZА-Я][a-zа-я]*$", ErrorMessage = "Фамилия может включать в себя только символы Русского и Латинского алфавита, а так же должна быть написана с большой буквы")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Имя - это обязательное поле")]
        [StringLength(50, ErrorMessage = " Превышенно максимальное количество символов")]
        [RegularExpression(@"^[A-ZА-Я][a-zа-я]*$", ErrorMessage = "Имя может включать в себя только символы Русского и Латинского алфавита, а так же должна быть написана с большой буквы")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата трудоустройства")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Преподаватель")]

        public string FullName { get => $"{LastName}{FirstName}";}

        //Novigation properties
        public OfficeAssignment OfficeAssignment { get; set; }

        public ICollection<Course> Course { get; set; }


        


    }
}
