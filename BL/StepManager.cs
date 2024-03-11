using Data_Access_Layer;
using Domain.ProjectLogics.Steps;

namespace Business_Layer;

public class StepManager
{

    private readonly StepRepository _repo;
    public StepManager(StepRepository repo)
    {
        _repo = repo;
    }
    
    public StepBase GetStepForFlowByNumber(long flowId, int stepNumber)
    {
        return _repo.ReadStepForFlowByNumber(flowId, stepNumber);
    }
    
}