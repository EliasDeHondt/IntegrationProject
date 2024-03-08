using System.ComponentModel.DataAnnotations;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public class InformationStep : IStep
{
    public int StepNumber { get; set; }
    public long Id { get; set; }
    
    [Required]
    public IInformation Information { get; set; }
    public IQuestion<object>? Question { get; set; }
    public Note Note { get; set; } = new();
    public Flow Flow { get; set; }
    
    
    
    public InformationStep(int stepNumber, IInformation information)
    {
        StepNumber = stepNumber;
        Information = information;
    }
    
}