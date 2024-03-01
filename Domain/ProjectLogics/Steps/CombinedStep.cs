using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public class CombinedStep<T> : ISteps
{
    public int StepNumber { get; set; }
    
    public IInformation Information { get; set; }
    
    public IQuestion<T> Question { get; set; }
    
}