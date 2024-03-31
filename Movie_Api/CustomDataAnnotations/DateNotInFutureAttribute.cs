using System.ComponentModel.DataAnnotations;

namespace Movie_Api.CustomDataAnnotations
{
    public class DateNotInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if ((DateTime)value > DateTime.Now)
            {
                return new ValidationResult("Date cannot be in the future.");
            }
            return ValidationResult.Success;

        }
    }
}
