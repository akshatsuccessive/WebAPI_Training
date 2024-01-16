using EmployeesAPI.Models;
using FluentValidation;

namespace EmployeesAPI.Validator
{
    public class CustomValidator : AbstractValidator<Employee>
    {
        public CustomValidator() 
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Please Enter a Name")
                .Length(1, 10).WithMessage("Name length should be between 1 and 10 characters");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(e => e.Salary)
                .NotEmpty().WithMessage("Salary cannot be empty")
                .InclusiveBetween(10000, 100000).WithMessage("Salary must be between 10,000 and 100,000");

            RuleFor(e => e.Department)
                .NotEmpty().WithMessage("Department Name cannot be empty")
                .MinimumLength(5).WithMessage("Department Name should be Minimum of 5 characters");
        }
    }
}
