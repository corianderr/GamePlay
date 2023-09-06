using System.ComponentModel.DataAnnotations;

namespace GamePlay.Domain.CustomAttributes;

public class NotLessAttribute : ValidationAttribute {
    private readonly string _comparisonProperty;

    public NotLessAttribute(string comparisonProperty) {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult? IsValid(object value, ValidationContext validationContext) {
        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

        if (property == null)
            throw new ArgumentException($"Property {_comparisonProperty} not found");

        var comparisonValue = Convert.ToInt32(property.GetValue(validationContext.ObjectInstance));
        var currentValue = (int)value;

        return currentValue < comparisonValue ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
    }
}