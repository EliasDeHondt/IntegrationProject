using Domain.FacilitatorFunctionality;

namespace Domain.ProjectLogics.Steps;

public interface ISteps
{
    int StepNumber { get; set; }

    Note note { get; set; }

}