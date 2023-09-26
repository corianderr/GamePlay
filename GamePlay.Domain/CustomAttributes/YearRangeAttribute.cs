using System.ComponentModel.DataAnnotations;

namespace GamePlay.Domain.CustomAttributes;

public class YearRangeAttribute : ValidationAttribute {
    private readonly int _minYear;

    public YearRangeAttribute(int minYear) {
        _minYear = minYear;
    }

    protected override ValidationResult? IsValid(object value, ValidationContext validationContext) {
        if (value is not int year) return ValidationResult.Success;
        var currentYear = DateTime.Now.Year;

        if (year < _minYear || year > currentYear + 10) return new ValidationResult(ErrorMessage);
        return ValidationResult.Success;
    }
}