namespace SoManyBooksSoLittleTime.Web.ViewModels.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CheckDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;

            if (dt <= DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(this.ErrorMessage ?? "Make sure your date is <= than today");
        }
    }
}
