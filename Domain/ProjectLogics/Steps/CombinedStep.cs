using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public class CombinedStep : IStep
{
    public int StepNumber { get; set; }
    public long Id { get; set; }
    public Note Note { get; set; } = new();
    public Flow Flow { get; set; }

    public IInformation Information { get; set; }
    
    public IQuestion<object> Question { get; set; }
    
    public CombinedStep(int stepNumber, IInformation information, IQuestion<object> question)
    {
        StepNumber = stepNumber;
        Information = information;
        Question = question;
    }
    
}