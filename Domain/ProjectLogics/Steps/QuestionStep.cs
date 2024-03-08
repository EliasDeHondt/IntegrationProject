using System.ComponentModel.DataAnnotations;
using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public class QuestionStep : IStep
{
    public int StepNumber { get; set; }
    public long Id { get; set; }
    public IInformation? Information { get; set; } = null;
    public Note Note { get; set; } = new();
    public Flow Flow { get; set; }

    [Required]
    public IQuestion<object> Question { get; set; }
    
    public QuestionStep(int stepNumber, IQuestion<object> question)
    {
        StepNumber = stepNumber;
        Question = question;
    }
    
    
}