using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IssuesClient.Utility;

public class PasswordAttribute : ValidationAttribute
{

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
        value is string str && !string.IsNullOrWhiteSpace(str) &&
        str.Length > 8 &&
        str.Any(char.IsNumber) &&
        str.Any(char.IsUpper) &&
        str.Any(char.IsLower) &&
        str.Any(char.IsDigit) &&
        str.Any(c => !char.IsLetterOrDigit(c))
        ? ValidationResult.Success
        : new("");

}
