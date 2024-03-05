using Domain.Facilitator;
using Domain.ProjectLogics.Steps.Information;

namespace Domain.ProjectLogics.Steps;

public class InformationStep : ISteps
{
    public int StepNumber { get; set; }
    public Note note { get; set; } = new();

    public IInformation Information { get; set; }
    
    public InformationStep(int stepNumber, IInformation information)
    {
        StepNumber = stepNumber;
        Information = information;
    }
    
}