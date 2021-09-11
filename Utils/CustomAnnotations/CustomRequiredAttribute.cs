using System;
using System.ComponentModel.DataAnnotations;

namespace Utils.CustomAnnotations
{
    public class CustomRequiredAttribute : RequiredAttribute
    {
        public string Display { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || (value.GetType() == typeof(String) && string.IsNullOrEmpty((string)value)))
            {
                return new ValidationResult("O campo " +
                                            (string.IsNullOrEmpty(Display) ? validationContext.MemberName : validationContext.DisplayName) +
                                            " é obrigatório");
            }

            return ValidationResult.Success;
        }
    }
}