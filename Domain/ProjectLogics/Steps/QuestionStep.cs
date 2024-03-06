using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public class QuestionStep<T> : ISteps
{
    public int StepNumber { get; set; }
    public Note note { get; set; } = new();

    public IQuestion<T> Question { get; set; }
    
    public QuestionStep(int stepNumber, IQuestion<T> question)
    {
        StepNumber = stepNumber;
        Question = question;
    }
    
    
}