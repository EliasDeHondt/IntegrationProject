using Domain.FacilitatorFunctionality;
using Domain.ProjectLogics.Steps.Information;
using Domain.ProjectLogics.Steps.Questions;

namespace Domain.ProjectLogics.Steps;

public interface IStep
{
    int StepNumber { get; set; }

    long Id { get; set; }
    
    IInformation? Information { get; set; }
    
    IQuestion? Question { get; set; }
    
    Note Note { get; set; }

    Flow Flow { get; set; }
    
}