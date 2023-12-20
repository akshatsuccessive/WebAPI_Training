using FluentValidation;
using WebAPITraining.Models;

namespace WebAPITraining.Validator
{
    public class ValidatorClass : AbstractValidator<Student>
    {
        public ValidatorClass() 
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Please enter a name")
                .Length(0, 10).WithMessage("Name length should be between 1 and 10 characters");
        }
    }
}
