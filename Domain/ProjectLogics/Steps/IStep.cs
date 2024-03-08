using Domain.FacilitatorFunctionality;

namespace Domain.ProjectLogics.Steps;

public interface IStep
{
    int StepNumber { get; set; }

    Note Note { get; set; }

    Flow Flow { get; set; }
    
}