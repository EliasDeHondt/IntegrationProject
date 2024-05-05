/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

using System.Collections;
using Data_Access_Layer;
using Domain.ProjectLogics;
using Domain.ProjectLogics.Steps;

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

    public Flow GetFlowByIdWithTheme(long id)
    {
        return _repository.ReadFlowByIdIncludingTheme(id);
    }
    
    public IEnumerable<Flow> GetAllFlows()
    {
        return _repository.ReadAllFlows();
    }

    public IEnumerable<Flow> GetAllFlowsWithTheme()
    {
        return _repository.ReadAllFlowsWithTheme();
    }
    
    public void SetParticipationByFlow(long flowId,string email)
    {
        _repository.AddParticipationByFlow(flowId,email);
    }

    public void ChangeFlowState(Flow flow)
    {
        _repository.UpdateFlowState(flow);
    }

    public IEnumerable<Flow> GetFlowsByIds(IEnumerable<long> flowIds)
    {
        return _repository.ReadFlowsByIds(flowIds);
    }
}