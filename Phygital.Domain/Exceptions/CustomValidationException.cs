using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

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
    
    protected CustomValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        
    }
    
}