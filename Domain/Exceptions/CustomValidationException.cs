using System.ComponentModel.DataAnnotations;

namespace Domain.Exceptions;

[Serializable]
public class CustomValidationException: Exception
{

    public List<ValidationResult> Errors { get; }

    public CustomValidationException(List<ValidationResult> errors)
        : base("Error occurred in validation")
    {
        Errors = errors;
    }
    
}