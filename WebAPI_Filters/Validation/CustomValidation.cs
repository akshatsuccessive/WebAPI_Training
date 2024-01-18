using Employees_API2.Models.DomainModels;
using Employees_API2.Models.DTO;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Employees_API2.Validation
{
    public class CustomValidation : AbstractValidator<AddEmployeeDTORequest>
    {
        private bool BeValidAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            // Adjust age if birthday hasn't occurred yet this year
            if (dateOfBirth > today.AddYears(-age))
            {
                age--;
            }

            return age >= 18 && age <= 60;
        }

        public CustomValidation() 
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name Should not be Empty")
                .Length(1, 15).WithMessage("Name length should be between 1 and 15 characters");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email should not be empty")
                .EmailAddress().WithMessage("Email address should be in Valid Format");

            RuleFor(e => e.DateOfBirth)
                .NotEmpty().WithMessage("Date Of Birth should not be empty")
                .Must(BeValidAge).WithMessage("Employee age must be between 18 and 60.");

            RuleFor(e => e.Salary)
                .NotEmpty().WithMessage("Salary cannot be empty")
                .InclusiveBetween(10000, 100000).WithMessage("Salary must be between 10,000 and 100,000");

            RuleFor(e => e.Department)
                .NotEmpty().WithMessage("Department Name cannot be empty")
                .MinimumLength(5).WithMessage("Department Name should be Minimum of 5 characters");
        }
    }
    public class CustomValidationUpdate : AbstractValidator<UpdateEmployeeDTORequest>
    {
        private bool BeValidAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            // Adjust age if birthday hasn't occurred yet this year
            {
                age--;
            }

            return age >= 18 && age <= 60;
        }

        public CustomValidationUpdate()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name Should not be Empty")
                .Length(1, 15).WithMessage("Name length should be between 1 and 15 characters");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email should not be empty")
                .EmailAddress().WithMessage("Email address should be in Valid Format");

            RuleFor(e => e.DateOfBirth)
                .NotEmpty().WithMessage("Date Of Birth should not be empty")
                .Must(BeValidAge).WithMessage("Employee age must be between 18 and 60.");

            RuleFor(e => e.Salary)
                .NotEmpty().WithMessage("Salary cannot be empty")
                .InclusiveBetween(10000, 100000).WithMessage("Salary must be between 10,000 and 100,000");

            RuleFor(e => e.Department)
                .NotEmpty().WithMessage("Department Name cannot be empty")
                .MinimumLength(5).WithMessage("Department Name should be Minimum of 5 characters");
        }
    }
}
