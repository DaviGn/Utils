using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Utils.CustomAnnotations
{
    public class AlfaNumericAttribute : ValidationAttribute
    {
        bool isNull = false;
        public AlfaNumericAttribute(bool IsNull)
        {
            isNull = IsNull;
        }

        public string pattern = @"^.[a-zA-Z0-9_ ]*$";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (isNull)
            {
                if(value is null)
                    return ValidationResult.Success;
                else
                {
                    if (Regex.IsMatch(value.ToString(), pattern, RegexOptions.IgnoreCase))
                    {
                        return ValidationResult.Success;
                    }

                    return new ValidationResult("O campo " +
                                                    (string.IsNullOrEmpty(validationContext.DisplayName) ?
                                                    validationContext.MemberName : validationContext.DisplayName) +
                                                    " deve ser alfanumérico");
                }
            }
            else
            {
                if(value is null)
                {
                    return new ValidationResult("O campo " +
                                            (string.IsNullOrEmpty(validationContext.DisplayName) ?
                                            validationContext.MemberName : validationContext.DisplayName) +
                                            " é obrigatório");
                }
                else
                {
                    if (Regex.IsMatch(value.ToString(), pattern, RegexOptions.IgnoreCase))
                    {
                        return ValidationResult.Success;
                    }

                    return new ValidationResult("O campo " +
                                                    (string.IsNullOrEmpty(validationContext.DisplayName) ?
                                                    validationContext.MemberName : validationContext.DisplayName) +
                                                    " deve ser alfanumérico");
                }
                
            }
        }
    }
}
