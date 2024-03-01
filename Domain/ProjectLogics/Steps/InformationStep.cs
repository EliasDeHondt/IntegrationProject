using Domain.ProjectLogics.Steps.Information;

namespace Domain.ProjectLogics.Steps;

public class InformationStep : ISteps
{
    public int StepNumber { get; set; }
    
    public IInformation Information { get; set; }
    
}