using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics.Steps.Information;

namespace Domain.ProjectLogics.Steps;

public class InformationStep : IStep
{
    public int StepNumber { get; set; }
    public long Id { get; set; }
    public Note Note { get; set; } = new();
    public Flow Flow { get; set; }
    

    public IInformation Information { get; set; }
    
    public InformationStep(int stepNumber, IInformation information)
    {
        StepNumber = stepNumber;
        Information = information;
    }
    
}