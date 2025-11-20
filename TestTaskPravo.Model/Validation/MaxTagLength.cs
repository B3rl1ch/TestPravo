using System.ComponentModel.DataAnnotations;

namespace TestTaskPravo.Model.Validation;

public class MaxTagLengthAttribute : ValidationAttribute
{
    private readonly int _maxLength;

    public MaxTagLengthAttribute(int maxLength)
    {
        _maxLength = maxLength;
        ErrorMessage = $"Каждый элемент списка не должен превышать {_maxLength} символов.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IEnumerable<string> list)
            return ValidationResult.Success;

        foreach (var item in list)
        {
            if (item != null && item.Length > _maxLength)
            {
                return new ValidationResult(
                    $"{validationContext.MemberName}: элемент '{item}' превышает допустимую длину {_maxLength}.");
            }
        }

        return ValidationResult.Success;
    }
}