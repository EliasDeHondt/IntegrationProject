/***************************************
 *                                     *
 * Created by CodeForge                *
 * Visit https://codeforge.eliasdh.com *
 *                                     *
 ***************************************/

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
    public Flow Add(string type)
    {

        FlowType flowType;
        
        switch (type)
        {
            case "Linear": flowType = FlowType.LINEAR;
                break;
            
            case "Circular": flowType = FlowType.CIRCULAR;
                break;
            
            default: flowType = FlowType.UNKNOWN;
                break;
        }

        Flow newFlow = new Flow();
        
        newFlow.FlowType = flowType;
        
        _repository.AddFlow(newFlow);

        return newFlow;
    }

    public Flow Update(int flowId)
    {
        throw new NotImplementedException();
    }

    public Flow Remove(Flow item)
    {
        throw new NotImplementedException();
    }

    public Flow GetFlowById(long id)
    {
        return _repository.ReadFlowById(id);
    }

    public Flow GetFlowByIdWithTheme(long id)
    {
        return _repository.ReadFlowByIdIncludingTheme(id);
    }
    
    public List<Flow> GetAllFlows()
    {
        return _repository.ReadAllFlows().ToList();
    }
}