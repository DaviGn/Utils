using System;
using System.ComponentModel.DataAnnotations;

namespace Utils.CustomAnnotations
{
    public class CustomDateRequiredAttribute : RequiredAttribute
    {
        public string Display { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || (value.GetType() == typeof(DateTime) && (DateTime)value == DateTime.MinValue))
            {
                return new ValidationResult("O campo " +
                                            (string.IsNullOrEmpty(Display) ? validationContext.MemberName : validationContext.DisplayName) +
                                            " é obrigatório");
            }

            return ValidationResult.Success;
        }
    }
}