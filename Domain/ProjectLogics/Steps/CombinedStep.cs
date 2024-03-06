using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public class CombinedStep<T> : ISteps
{
    public int StepNumber { get; set; }
    public Note note { get; set; } = new();

    public IInformation Information { get; set; }
    
    public IQuestion<T> Question { get; set; }
    
    public CombinedStep(int stepNumber, IInformation information, IQuestion<T> question)
    {
        StepNumber = stepNumber;
        Information = information;
        Question = question;
    }
    
}