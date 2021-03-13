using System;
using System.ComponentModel.DataAnnotations;

namespace Filed_Coding.Helpers.Validations
{

    public class CardExpiryValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (DateTime.TryParse(value.ToString(), out DateTime ccnDate) && DateTime.UtcNow.Date < ccnDate)
                return null;
            return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
        }
    }
}
