using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public class QuestionStep<T> : ISteps
{
    public int StepNumber { get; set; }
    
    public IQuestion<T> Question { get; set; }
    
}