/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using Data_Access_Layer;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;
using Domain.ProjectLogics.Steps.Information;

namespace Business_Layer;

public class FlowManager
{
    private readonly FlowRepository _repository;

    public FlowManager(FlowRepository repository)
    {
        _repository = repository;
    }

    //This function makes a GLOBAL Flow, not project specific!
    public Flow Add(FlowType type)
    {

        Flow newFlow = new Flow
        {
            FlowType = type
        };

        _repository.AddFlow(newFlow);

        return newFlow;
    }
    
    public Flow GetFlowById(long id)
    {
        return _repository.ReadFlowById(id);
    }

    /** Returns all steps in the given flow.
     * 
     * parameter: flowId -> The ID of the flow from which we are getting the steps.
     */ 
    public IEnumerable<StepBase> GetAllStepsInFlow(long flowId)
    {
        return _repository.GetAllSteps(flowId);
    }

    public Flow GetFlowByIdWithTheme(long id)
    {
        return _repository.ReadFlowByIdIncludingTheme(id);
    }
    
    public IEnumerable<Flow> GetAllFlows()
    {
        return _repository.ReadAllFlows();
    }
    public void SetParticipationByFlow(long flowId,string email)
    {
        _repository.AddParticipationByFlow(flowId,email);
    }

    public void ChangeFlowState(Flow flow)
    {
        _repository.UpdateFlowState(flow);
    }

    //TODO: this method needs to be updated for all step types & sub types
    public StepBase AddStep(long flowId, int stepNumber, string stepType)
    {
        Flow flow = _repository.ReadFlowById(flowId);

        StepBase step = new InformationStep();
        
        switch (stepType)
        {
            case "Information":
                step = new InformationStep(stepNumber, new Text(),flow);
                break;
        }
        
        _repository.AddStepToFlow(flowId, step);

        return step;

    }
}