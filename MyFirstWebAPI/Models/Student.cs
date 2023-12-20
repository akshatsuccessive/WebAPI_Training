using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using WebAPITraining.Validator;

namespace WebAPITraining.Models
{
    public class Student
    {
        [ValidateNever]
        public int Id { get; set; }

        //[StringLength(25, ErrorMessage = "Name should be less than 25 characters")]
        public string Name { get; set; }

        [Range(18, 60)]
        [AgeValidator(ErrorMessage = "Invalid Age")]
        public int Age { get; set; }

        [Required]
        public string Department { get; set; }
    }
}
