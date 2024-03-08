using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public class QuestionStep<T> : IStep
{
    public int StepNumber { get; set; }
    public Note Note { get; set; } = new();
    public Flow Flow { get; set; }

    public IQuestion<T> Question { get; set; }
    
    public QuestionStep(int stepNumber, IQuestion<T> question)
    {
        StepNumber = stepNumber;
        Question = question;
    }
    
    
}