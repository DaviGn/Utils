using System;
using System.ComponentModel.DataAnnotations;

namespace Utils.CustomAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MustBeTrueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && (bool)value == true)
                return ValidationResult.Success;

            return new ValidationResult(!string.IsNullOrEmpty(ErrorMessage) ? ErrorMessage :
                                        "O campo " +
                                        (string.IsNullOrEmpty(validationContext.DisplayName) ?
                                        validationContext.MemberName : validationContext.DisplayName) +
                                        " deve ser verdadeiro");
        }
    }
}
