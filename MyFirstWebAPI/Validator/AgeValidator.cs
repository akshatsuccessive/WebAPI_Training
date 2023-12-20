using System.ComponentModel.DataAnnotations;

namespace WebAPITraining.Validator
{
    public class AgeValidator : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var age = Convert.ToInt32(value);
            return age > 18;
        }
    }
}
